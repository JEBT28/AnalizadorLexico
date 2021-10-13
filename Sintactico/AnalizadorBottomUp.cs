using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Compilador.Sintactico
{
    class AnalizadorBottomUp
    {
        public AnalizadorBottomUp()
        {
            //Obtenemos las gramaticas del CSV
            this.Gramaticas = new LeerCSV().ObtenerGramaticas();
            //Inicializamos la lista donde se almacenaran las entradas derivadas
            this.DerivacionesEntradas = new List<DerivacionEntrada>();
            this.ErrorSintacticos = new List<ErrorSintactico>();
            //Bloque para imprimir las gramaticas con su pila valida en el debugger
            foreach (var item in Gramaticas)
            {
                Debug.WriteLine("Raiz: {0} | PilaValida: {1}", item.Raiz, item.PilaValida.ToString());
            }
        }

        private int Fila { get; set; }
        public int CorrespondeciaParentesis { get; set; }
        public int CorrespondeciaLlaves { get; set; }
        public List<ErrorSintactico> ErrorSintacticos { get; set; }
        public List<Gramatica> Gramaticas { get; set; }
        public List<DerivacionEntrada> DerivacionesEntradas { get; set; }
        public void Recorrido(string[] tokens)
        {
            //Recibimos los tokens de la cadena de entrada
            List<string> pilaRaices = new List<string>();
            DerivacionesEntradas = new List<DerivacionEntrada>();

            Fila = tokens.Length;
            for (int i = tokens.Length - 1; i > -1; i--)
            {               

                if (!String.IsNullOrWhiteSpace(tokens[i]) )
                {
                    if (tokens[i].Trim().Contains(' '))
                    {
                        //Realizamos el recorrido, pasando lo tokens obtenidos
                        string resultado =  DerivarEntrada(entradaLinea: tokens[i]);

                        if (resultado.Equals("ERROR"))
                        {
                            ErrorSintacticos.Add(new ErrorSintactico { Codigo = "ERROR06", Descripcion = "Se esperaba otra terminacion para la instruccion. ", Linea = Fila });
                        }

                        pilaRaices.Add(resultado);
                    }
                    else
                    {

                        DerivacionesEntradas.Add(new DerivacionEntrada(tokens[i]) { Derivaciones = new List<string>() { tokens[i] } });
                        pilaRaices.Add(tokens[i]);
                    }
                }
               
                Fila--;
            }

            pilaRaices.Reverse();
            string res = DerivarEntrada(String.Join("", pilaRaices));
            if (res.Trim() == "S")
            {
                Debug.WriteLine("Aceptada");
            }
            else
            {
                ErrorSintacticos.Add(new ErrorSintactico { Codigo = "ERROR10", Descripcion = "No se pudo derivar la pila de raices", Linea = 0 });
                throw new Exception();

            }

            //Al finalizar el recorrido de la cadena de entrada verificamos si existe un desbalance
            //con los parentesis y llaves que no fue detectado 
            if (CorrespondeciaLlaves != 0)
            {
                ErrorSintacticos.Add(new ErrorSintactico { Codigo = "ERROR10", Descripcion = "Se esperaba un " + (CorrespondeciaLlaves < 0 ? "{" : "}"), Linea = tokens.Length });
            }
            if (CorrespondeciaParentesis != 0)
            {

                ErrorSintacticos.Add(new ErrorSintactico { Codigo = "ERROR09", Descripcion = "Se esperaba un " + (CorrespondeciaParentesis < 0 ? "(" : ")"), Linea = Fila });
            }           

            if (ErrorSintacticos.Count > 0)
            {
                Debug.WriteLine("Rechazada");
                throw new Exception();
            }
            
        }
        public void RecorrerEntrada(string[] entradaLinea)
        {
            //Inicializamos una lista para trabajar con la pila de entrada
            List<string> PilaEntrada = new List<string>();

            //Cargamos la pila de entrada por primera ves con los tokens de la entrada
            PilaEntrada.AddRange(entradaLinea);

            //Declaramos la pila que recibira los tokens en el desplazamiento
            List<string> Pila;

            //Creamos un Objeto de tipo DerivacionEntrada donde almacenaremos las derivaciones
            DerivacionEntrada entrada = new DerivacionEntrada(String.Join(" ", PilaEntrada));

            do
            {
                Debug.WriteLine("Antes Despl: " + String.Join(" ", PilaEntrada));
                // Desplazamiento

                //Inicializamos la pila para los tokens que se desplacen
                Pila = new List<string>();

                //Mientras existan elemento en la pila de entrada seguiremos recorriendola
                while (PilaEntrada.Count > 0)
                {
                    //Tomamos el ultimo token de la pila, en este caso el token mas a la derecha de la entrada
                    string token = PilaEntrada.Last().Contains("ID") ? "ID" : PilaEntrada.Last().Contains("CN") ? "CN" : PilaEntrada.Last();

                    //Removemos el token de la pila de entrada
                    PilaEntrada.Remove(PilaEntrada.Last());

                    //Comparamos el token para las banderas de correspondencia de parentesis y llaves
                    switch (token)
                    {
                        case "CAES02": CorrespondeciaParentesis++; break;
                        case "CAES13": CorrespondeciaParentesis--; break;
                        case "CAES14": CorrespondeciaLlaves++; break;
                        case "CAES15": CorrespondeciaLlaves--; break;
                        default: break;
                    }
                    
                    //Agregamos el token a la pila de analisis
                    Pila.Add(token);

                    Debug.WriteLine("Pila antes reduccion: " + String.Join(" ", Pila.Reverse<string>()));

                    //Agregamos la pila antes de la reduccion a las derivacions
                    entrada.Derivaciones.Add(String.Join(" ", Pila.Reverse<string>()));

                    //Reducimos y recibimos el resultado de la derivacion
                    var resultado = Derivacion(Pila.ToArray());


                    //Si el resultado es nulo o espacio en blanco continuamos
                    if (!String.IsNullOrWhiteSpace(resultado) || resultado == "S")
                    {
                        //Si tenemos un resultado limpiamos la pila y dejamos solo el resultado
                        Pila.Clear();
                        Pila.Add(resultado);
                    }
                    else
                    {
                        //Si resultado es nulo y la Pila de entrada ya no tiene tokens disparamos un error 
                        //para avisar que no puede seguir reduciendo para esta pila
                        if (PilaEntrada.Count == 0)
                        {
                            if (CorrespondeciaParentesis != 0)
                            {
                                ErrorSintacticos.Add(new ErrorSintactico { Codigo = "ERROR09", Descripcion = "Se esperaba un " + (CorrespondeciaParentesis < 0 ? "(" : ")"), Linea = Fila });
                                if (CorrespondeciaParentesis < 0)
                                    CorrespondeciaParentesis++;
                                else
                                    CorrespondeciaParentesis--;
                                Pila.Clear();
                                Pila.Add("ERROR");
                            }
                            else
                            {
                                ErrorSintacticos.Add(new ErrorSintactico { Codigo = "ERROR06", Descripcion = "Se esperaba otra terminacion para la instruccion. ", Linea = Fila });
                                Pila.Clear();
                                Pila.Add("ERROR");
                            }
                        }
                    }

                }
                //Realizamos la reversa en caso de que se pueda seguir derivando
                // Reversa                
                PilaEntrada.AddRange(Pila.Reverse<String>());

            } while (Pila.First() != "S" && Pila.First() != "ERROR");//Si la Pila de analisis ya nos devolvio una aceptacion para la entrada salimos
            //Agregamos la aceptacion a la lista de derivaciones para esta entrada
            entrada.Derivaciones.Add(Pila.First());
            //Agregamos la entrada derivada a la lista de entradas derivadas
            DerivacionesEntradas.Add(entrada);

        }

        public string DerivarEntrada(string entradaLinea)
        {

            entradaLinea = new Regex("ID[0-9]{2}").Replace(entradaLinea, "ID");
            entradaLinea = new Regex("CNE[0-9]{2}").Replace(entradaLinea, "CNE");
            entradaLinea = new Regex("CNR[0-9]{2}").Replace(entradaLinea, "CNR");

            string entradaDerivada = entradaLinea;
            while (Regex.Matches( entradaDerivada,"  ").Count!=0)
            {
                entradaDerivada = entradaDerivada.Replace("  "," ");
            }

            CorrespondeciaParentesis += Regex.Matches(entradaLinea,"CAES02").Count;
            CorrespondeciaParentesis -= Regex.Matches(entradaLinea, "CAES13").Count;

            Debug.WriteLine("CorresponednciaParentesis: "+CorrespondeciaParentesis);
            if (CorrespondeciaParentesis != 0)
            {
                ErrorSintacticos.Add(new ErrorSintactico { Codigo = "ERROR09", Descripcion = "Se esperaba un " + (CorrespondeciaParentesis < 0 ? "(" : ")"), Linea = Fila });
                CorrespondeciaParentesis = 0;
                DerivacionesEntradas.Add(new DerivacionEntrada(entradaLinea) { Derivaciones = new List<string>() { "ERROR" } });
                return "ERROR"; 
            }

            CorrespondeciaLlaves += new Regex("CAES14").Matches(entradaLinea).Count;
            CorrespondeciaLlaves -= new Regex("CAES15").Matches(entradaLinea).Count;
            Debug.WriteLine("CorresponednciaLlaves: " + CorrespondeciaParentesis);
            if (CorrespondeciaLlaves != 0)
            {
                ErrorSintacticos.Add(new ErrorSintactico { Codigo = "ERROR10", Descripcion = "Se esperaba un " + (CorrespondeciaLlaves < 0 ? "{" : "}"), Linea = 0 });
                CorrespondeciaLlaves = 0;
                DerivacionesEntradas.Add(new DerivacionEntrada(entradaLinea) { Derivaciones = new List<string>() { "ERROR" } });
                return "ERROR";
            }          
            DerivacionEntrada entrada = new DerivacionEntrada(entradaLinea);
            var pilaAD = "";

            bool control = true;
            while ( control )
            {

                if (pilaAD.Equals(entradaDerivada) && !entradaDerivada.Trim().Equals("SENTENCIA"))
                {
                    Debug.WriteLine("Pila antes ERROR: " + entradaDerivada);
                    entradaDerivada = "ERROR";
                    entrada.Derivaciones.Add(entradaDerivada);
                }

                pilaAD = entradaDerivada;
                if (!entradaDerivada.Equals("ERROR"))
                {
                    for (int i = Gramaticas.Count - 1; i > -1; i--)
                    {
                        var g = Gramaticas[i];

                        string pilaResultante = "";
                        while (g.PilaValida.IsMatch(entradaDerivada))
                        {
                            pilaResultante = g.PilaValida.Replace(entradaDerivada, g.Raiz);
                            if (!entradaDerivada.Equals(pilaResultante))
                            {
                                Debug.WriteLine("Pila antes reduccion: " + entradaDerivada);
                                Debug.WriteLine("Pila reducida: " + pilaResultante);
                                entrada.Derivaciones.Add(pilaResultante);
                                entradaDerivada = pilaResultante;
                            }
                        }
                        
                    }
                }

                if (entradaDerivada.Trim().Equals("SENTENCIA"))
                {
                    control = false;
                }
                else if (entradaDerivada.Equals("ERROR"))
                {
                    control = false;
                }
                else if (!entradaDerivada.Trim().Contains(' '))
                {
                    control = false;
                }
                else
                {
                    control = true;
                }
               
               
            }

            DerivacionesEntradas.Add(entrada);
            return entradaDerivada;
        }

        

        private string Derivacion(string[] Pila)
        {
            //Pasamos la pila a una cadena que pueda ser comparada vs un patron
            var auxPila = String.Join(" ", Pila.Reverse<string>()).Trim();

            Debug.Write(auxPila + "->");

            //Buscamos que exista una gramatica que pueda reducir la pila de entrada
            bool gramaticaValida = Gramaticas.Contains(new Gramatica(auxPila, new Regex("")));

            if (gramaticaValida)
            {
                //Si la gramatica es valida la recuperamos
                var gramatica = Gramaticas.ElementAt(Gramaticas.IndexOf(new Gramatica(auxPila, new Regex(""))));
                //Hacemos la reduccion    
                string pilaResultante = gramatica.PilaValida.Replace(auxPila, gramatica.Raiz);

                Debug.WriteLine(pilaResultante);
                //Retornamos la pila resultante
                return pilaResultante;
            }
            else
            {
                //Si no existe gramatica valida se retorna nada para que siga recorriendo la pila
                return "";
            }
        }
    }

}

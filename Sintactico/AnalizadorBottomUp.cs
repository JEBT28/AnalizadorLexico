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

            Fila = tokens.Length;
            for (int i = tokens.Length - 1; i > -1; i--)
            {
                //Recorremos linea por linea los entradas de tokens, descartando las vacias
                if (!String.IsNullOrWhiteSpace(tokens[i]))
                {
                    //Separamos los tokens de la linea en una pila
                    var entrada = tokens[i].Trim().Split(' ');

                    //Realizamos el recorrido, pasando lo tokens obtenidos
                    RecorrerEntrada(entradaLinea: entrada);

                }
                Fila--;
            }

            //Al finalizar el recorrido de la cadena de entrada verificamos si existe un desbalance
            //con los parentesis y llaves que no fue detectado 
            if (CorrespondeciaLlaves != 0)
            {
                ErrorSintacticos.Add(new ErrorSintactico { Codigo = "ERROR10", Descripcion = "Se esperaba un " + (CorrespondeciaLlaves < 0 ? "{" : "}"), Linea = Fila });
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
            else
            {
                Debug.WriteLine("Aceptada");
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

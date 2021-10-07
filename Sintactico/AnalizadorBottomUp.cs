using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Compilador.Sintactico
{
    class AnalizadorBottomUp
    {
        public AnalizadorBottomUp() {        
            this.Gramaticas = new LeerCSV().ObtenerGramaticas();
            this.DerivacionesEntradas = new List<DerivacionEntrada>();
            foreach (var item in Gramaticas)
            {
                Debug.WriteLine("Raiz: {0} | PilaValida: {1}",item.Raiz,item.PilaValida.ToString());
            }
        }


        public int CorrespondeciaParentesis { get; set; }
        public int CorrespondeciaLlaves { get; set; }

        public List<Gramatica> Gramaticas { get; set; }

        public List<DerivacionEntrada>  DerivacionesEntradas{ get; set; }

        public void Recorrido(string[] tokens)
        {
            try
            {
                for (int i = 0; i < tokens.Length; i++)
                {
                    if (!String.IsNullOrWhiteSpace(tokens[i]))
                    {
                        var entrada = tokens[i].Trim().Split(' ');                      

                        RecorrerEntrada(entradaLinea: entrada);

                        
                    }                    
                }

                if (CorrespondeciaLlaves != 0) {

                    throw new Exception(message: "Se esperaba un "+(CorrespondeciaLlaves<0?"{":"}"));
                }
                if (CorrespondeciaParentesis != 0) {

                    throw new Exception(message: "Se esperaba un " + (CorrespondeciaLlaves < 0 ? "(" : ")"));
                }

                Debug.WriteLine("aceptada");
            }
            catch(Exception ex)
            { 
               throw new Exception(ex.Message);
            }
        }

        public void RecorrerEntrada(string[] entradaLinea)
        {
            List<string> PilaEntrada = new List<string>();

            PilaEntrada.AddRange(entradaLinea);

            List<string> Pila;

            DerivacionEntrada entrada = new DerivacionEntrada(String.Join(" ", PilaEntrada));

            do
            {

                Debug.WriteLine("Antes Despl: " + String.Join(" ", PilaEntrada));
                // Desplazamiento
                Pila = new List<string>();
                while(PilaEntrada.Count>0)
                {                   
                    string token = PilaEntrada.Last().Contains("ID") ? "ID" : PilaEntrada.Last().Contains("CN") ? "CN" : PilaEntrada.Last();

                    PilaEntrada.Remove(PilaEntrada.Last());

                    switch (token) {
                        case "CAES02": CorrespondeciaParentesis++;break;
                        case "CAES13": CorrespondeciaParentesis--; break;
                        case "CAES14": CorrespondeciaLlaves++; break;
                        case "CAES15": CorrespondeciaLlaves--; break;
                        default: break;
                    }

                    Pila.Add(token);

                    Debug.WriteLine("Pila antes reduccion: " + String.Join(" ", Pila.Reverse<string>()));

                    entrada.Derivaciones.Add(String.Join(" ", Pila.Reverse<string>()));

                    var resultado = Reduccion(Pila.ToArray());


                    if (!String.IsNullOrWhiteSpace(resultado))
                    {
                        Pila.Clear();
                        Pila.Add(resultado);
                    }
                    else
                    {
                        if (PilaEntrada.Count == 0)
                        {
                            throw new Exception("Se esperaba otra terminacion la instruccion");
                        }
                    }
                  
                }
                // Reversa                
                PilaEntrada.AddRange(Pila.Reverse<String>());
            } while (Pila.First() != "S");
            entrada.Derivaciones.Add("S");
            DerivacionesEntradas.Add(entrada);
        }

        private string Reduccion(string[] Pila) {
            
            var auxPila = String.Join(" ",Pila.Reverse<string>()).Trim();

            Debug.Write(auxPila+"->");

            bool gramaticaValida =  Gramaticas.Contains(new Gramatica(auxPila,new Regex(""))) ;

            //Debug.WriteLine("Raiz: {0} | PilaValida: {1}", gramaticaValida.Raiz, gramaticaValida.PilaValida.ToString());

            if (gramaticaValida)
            {

                var gramatica = Gramaticas.ElementAt(Gramaticas.IndexOf(new Gramatica(auxPila, new Regex(""))));
                string simbolo = gramatica.PilaValida.Replace(auxPila, gramatica.Raiz);
                
                Debug.WriteLine(simbolo);

                return simbolo;
            }
            else
            {
                return "";
            }            
        }
    }

}

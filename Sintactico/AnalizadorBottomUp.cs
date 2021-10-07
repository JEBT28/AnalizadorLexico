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
            foreach (var item in Gramaticas)
            {
                Debug.WriteLine("Raiz: {0} | PilaValida: {1}",item.Raiz,item.PilaValida.ToString());
            }
        }


        public int CorrespondeciaParentesis { get; set; }
        public int CorrespondeciaLlaves { get; set; }

        public List<Gramatica> Gramaticas { get; set; }

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

                        Debug.WriteLine(" aceptada");
                    }                    
                }
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

            do
            {

                Debug.WriteLine("Antes Despl: " + String.Join(" ", PilaEntrada));
                // Desplazamiento
                Pila = new List<string>();
                while(PilaEntrada.Count>0)
                {                   
                    string token = PilaEntrada.Last().Contains("ID") ? "ID" : PilaEntrada.Last().Contains("CN") ? "CN" : PilaEntrada.Last();                    
                    Debug.WriteLine("Token ntes eliminacion pilaEntrada: " + token);
                    PilaEntrada.Remove(PilaEntrada.Last());
                    Debug.WriteLine("Token ntes reduccion: " +token);
                    Pila.Add(token);
                    Debug.WriteLine("Pila antes reduccion: " + String.Join(" ", Pila.Reverse<string>()));
                    var resultado = Reduccion(Pila.ToArray());

                    if (!String.IsNullOrWhiteSpace(resultado))
                    {
                        Pila.Clear();
                        Pila.Add(resultado);
                    }
                }

                // Reversa                
                PilaEntrada.AddRange(Pila.Reverse<String>());
            } while (Pila.First() != "S");
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

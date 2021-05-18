using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    class Automata
    {
        public Automata()
        {
             (MatrizTransiciones,ColumnasMatriz) = new ConexionBD().obtenerMatriz();
            /*for (int i = 0; i < 215; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    Debug.Write(MatrizTransiciones[i, j]);
                }
                Debug.WriteLine("");
            } */

            foreach (var c in ColumnasMatriz)
            {
                Debug.WriteLine(c.NumeroColumna +" - "+c.Simbolo);
            }
        }

        public int EstadoActual { get; set; }

        public int ProximoEstado { get; set; }

        public string[,] MatrizTransiciones{ get; set; }

        public List<Error> ErroresLenguaje { get; set; }

        public List<ColumnaMatriz> ColumnasMatriz { get; set; }

        public string RecorridoMatriz(char caracter)
        {
            Regex saltoLinea = new Regex(@"[\n]");
            EstadoActual = ProximoEstado;
            Debug.Write(EstadoActual + " - ");
            int columnaCategoria = ColumnasMatriz.Count - 1;
            

            if (saltoLinea.IsMatch(caracter.ToString()))
            {
                ProximoEstado = 0;
                return "SL";

               
            }
            else
            {                        
                    int columna = ValidarCaracter(caracter);
                    Debug.WriteLine(columna);
                    ProximoEstado = int.Parse(MatrizTransiciones[EstadoActual, columna]);

                    return  MatrizTransiciones[ProximoEstado, columnaCategoria];  
            }
            
        }

        public int ValidarCaracter(char caracter)
        {
            Debug.Write(caracter+" - ");
            if (new Regex(@"[\s]").IsMatch(caracter.ToString()))
            {
                return ColumnasMatriz.ElementAt(ColumnasMatriz.FindIndex(cm => cm.Simbolo == "FDC")).NumeroColumna;
            }
            else
            {
                return ColumnasMatriz.ElementAt(ColumnasMatriz.FindIndex(cm => cm.Simbolo.Equals(caracter.ToString()))).NumeroColumna;
            }
        }
    }
}

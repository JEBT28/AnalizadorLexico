using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace AnalizadorLexico
{
    class LeerCSV
    {
        public (String[,], List<ColumnaMatriz>) obtenerMatriz()
        {
            List<ColumnaMatriz> colMatriz = new List<ColumnaMatriz>();
            string[,] matriz = new string[0,0];
            try
            {
                string[] filas = Properties.Resources.Matriz.Split('\n');

                string[] encabezadosColumnas = filas[0].Split(',');

                matriz = new string[filas.Length,encabezadosColumnas.Length-1];

                for (int c = 1; c < encabezadosColumnas.Length; c++) {

                    string simbolo = encabezadosColumnas[c];           

                    colMatriz.Add(new ColumnaMatriz { NumeroColumna = c - 1, Simbolo = simbolo == "CM" ? "," : simbolo });        
                }
                for (int x = 1; x < filas.Length; x++)
                {
                    string[] filaActual = filas[x].Split(',');

                    for (int y = 1; y < filaActual.Length; y++)
                    {
                        var valor = String.IsNullOrWhiteSpace(filaActual[y]) ? "" : filaActual[y].Trim();
                        matriz[x-1, y-1] = valor;
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Ocurrio un error");
            }            

            return (matriz, colMatriz);
        }

        public List<Error> obtenerErrores()
        {
            List<Error> errores = new List<Error>();
            try
            {
                string[] filas = Properties.Resources.Errores.Split('\n');
                for (int i = 0; i < filas.Length; i++)
                {
                    string[] filaActual = filas[i].Split(',');
                    errores.Add(new Error {Codigo=filaActual[0], Descripcion=filaActual[1] });
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Ocurrio un error");
            }

            return errores;
        }

    }
}

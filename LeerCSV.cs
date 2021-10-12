using Compilador.Sintactico;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Compilador
{
    class LeerCSV
    {
        public (String[,], List<ColumnaMatriz>) ObtenerMatriz()
        {
            List<ColumnaMatriz> colMatriz = new List<ColumnaMatriz>();
            string[,] matriz = new string[0, 0];
            try
            {
                string[] filas = Properties.Resources.Matriz.Split('\n');

                string[] encabezadosColumnas = filas[0].Split(',');

                matriz = new string[filas.Length, encabezadosColumnas.Length - 1];

                for (int c = 1; c < encabezadosColumnas.Length; c++)
                {

                    string simbolo = encabezadosColumnas[c];

                    colMatriz.Add(new ColumnaMatriz { NumeroColumna = c - 1, Simbolo = simbolo == "CM" ? "," : simbolo == "CS" ? "'" : simbolo == "CD" ? "\"" : simbolo });
                }
                for (int x = 1; x < filas.Length; x++)
                {
                    string[] filaActual = filas[x].Split(',');

                    for (int y = 1; y < filaActual.Length; y++)
                    {
                        var valor = String.IsNullOrWhiteSpace(filaActual[y]) ? "" : filaActual[y].Trim();
                        matriz[x - 1, y - 1] = valor;
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Ocurrio un error");
            }

            return (matriz, colMatriz);
        }

        public List<Compilador.Lexico.Error> ObtenerErrores()
        {
            List<Compilador.Lexico.Error> errores = new List<Compilador.Lexico.Error>();
            try
            {
                string[] filas = Properties.Resources.Errores.Split('\n');
                for (int i = 0; i < filas.Length; i++)
                {
                    string[] filaActual = filas[i].Split(',');
                    errores.Add(new Compilador.Lexico.Error { Codigo = filaActual[0], Descripcion = filaActual[1] });
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Ocurrio un error");
            }

            return errores;
        }


        public List<Gramatica> ObtenerGramaticas()
        {
            List<Gramatica> gramaticas = new List<Gramatica>();

            string[] gramaticasCSV = Properties.Resources.Gramaticas.Split('\n');

            foreach (var g in gramaticasCSV)
            {
                string[] definicion = g.Split(',');

                string raiz = definicion[0];

                string expreg = "";
                for (int i = 1; i < definicion.Length; i++)
                {
                    if (!string.IsNullOrEmpty(definicion[i]))
                    {
                        expreg += definicion[i] + " ";
                    }
                }

                gramaticas.Add(new Gramatica(raiz, new Regex(expreg.Trim())));
            }

            return gramaticas;
        }
    }
}

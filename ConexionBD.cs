using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace AnalizadorLexico
{
    class ConexionBD
    {
        static string cadenaConexion = "Data Source=127.0.0.1\\LOCALSQL; Initial Catalog = AUTOMATAS; User ID=LEXICO; Password=2021;Integrated Security=False";
        public SqlConnection conectarbd = new SqlConnection();

        SqlConnection con = new SqlConnection(cadenaConexion);

        SqlDataReader dr;

        void AbrirConexion()
        {


            if (!(con.State == ConnectionState.Open))
            {
                con.Open();
            }
        }

        void CerrarConexion()
        {
            if (!(con.State == ConnectionState.Closed))
            {
                con.Close();
            }
        }


        public (String[,], List<ColumnaMatriz>) obtenerMatriz()
        {
            List<ColumnaMatriz> colMatriz = new List<ColumnaMatriz>();
            string[,] matriz = new string[226, 99];
            try
            {
                AbrirConexion();

                String query = "SELECT * FROM MATRIZ;";

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dt);
                var columnas = dt.Columns;
                var filas = dt.Rows;



                foreach (DataColumn c in columnas)
                {
                    if (c.ColumnName.Contains("NUM"))
                    {
                        colMatriz.Add(new ColumnaMatriz { Simbolo = c.ColumnName.Replace("NUM", "").Trim(), NumeroColumna = c.Ordinal });
                    }
                    else if (c.ColumnName.Equals("CS"))
                    {
                        colMatriz.Add(new ColumnaMatriz { Simbolo = "'", NumeroColumna = c.Ordinal });
                    }
                    else if (c.ColumnName.Equals("CM"))
                    {
                        colMatriz.Add(new ColumnaMatriz { Simbolo = ".", NumeroColumna = c.Ordinal });
                    }
                    else if (new Regex(@"([A-ZÑ]{1}[1])").IsMatch(c.ColumnName))
                    {
                        colMatriz.Add(new ColumnaMatriz { Simbolo = c.ColumnName.Substring(0, 1), NumeroColumna = c.Ordinal });
                    }
                    else if (new Regex(@"([a-zñ]{1})").IsMatch(c.ColumnName))
                    {
                        colMatriz.Add(new ColumnaMatriz { Simbolo = c.ColumnName.Substring(0, 1), NumeroColumna = c.Ordinal });
                    }
                    else
                    {
                        colMatriz.Add(new ColumnaMatriz { Simbolo = c.ColumnName.Trim(), NumeroColumna = c.Ordinal });
                    }
                }


                for (int i = 0; i < filas.Count; i++)
                {
                    for (int j = 0; j < filas[i].ItemArray.Length; j++)
                    {
                        var valor = String.IsNullOrWhiteSpace(filas[i].ItemArray[j].ToString()) ? "" : filas[i].ItemArray[j].ToString();
                        matriz[i, j] = valor;
                    }
                }
            }
            catch (SqlException SQLe)
            {
                System.Windows.Forms.MessageBox.Show(SQLe.Message, "Ocurrio un error");
            }
            finally
            {
                CerrarConexion();
            }

            return (matriz, colMatriz);
        }


        public List<Error> obtenerErrores()
        {
            List<Error> errores = new List<Error>();
            try
            {
                AbrirConexion();

                String query = "SELECT * FROM ERRORES;";

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    errores.Add(new Error { Codigo = row["Error"].ToString(), Descripcion = row["Descripcion"].ToString() });
                }

            }
            catch (SqlException SQLe)
            {
                System.Windows.Forms.MessageBox.Show(SQLe.Message, "Ocurrio un error");
            }
            finally
            {
                CerrarConexion();
            }

            return errores;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    class ConexionBD
    {
        static string cadenaConexion = "Data Source=127.0.0.1\\LOCALSQL; Initial Catalog = AUTOMATAS; User ID=LEXICO; Password=2021;Integrated Security=False";
        public SqlConnection conectarbd = new SqlConnection();

        SqlConnection con = new SqlConnection(cadenaConexion);

        SqlDataReader dr;

        void AbrirConexion() {


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


        public (String[,],List<ColumnaMatriz>) obtenerMatriz()
        {
            List<ColumnaMatriz> colMatriz = new List<ColumnaMatriz>();
            string[,] matriz = new string[215, 99];
            try
            {
                AbrirConexion();

                String query = "SELECT * FROM MATRIZ;";

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dt);
                var columnas = dt.Columns;
                var filas = dt.Rows;

                columnas.Remove(columnas[0]);
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
                        colMatriz.Add(new ColumnaMatriz { Simbolo = ",", NumeroColumna = c.Ordinal });
                    }
                    else if (new Regex(@"([A-ZÑ]{1}[1])").IsMatch(c.ColumnName))
                    {
                        colMatriz.Add(new ColumnaMatriz { Simbolo = c.ColumnName.Substring(0, 1), NumeroColumna = c.Ordinal  });
                    }
                    else
                    {
                        colMatriz.Add(new ColumnaMatriz { Simbolo = c.ColumnName.Trim(), NumeroColumna = c.Ordinal  });
                    }
                }


                for (int i = 0; i < filas.Count; i++)
                {
                    for (int j = 1; j < filas[i].ItemArray.Length; j++)
                    {

                        var valor = filas[i].ItemArray[j] is null ? "" : filas[i].ItemArray[j].ToString();
                        matriz[i, j] = valor; 
                    }
                }
            }
            catch (SqlException SQLe)
            {
                System.Windows.Forms.MessageBox.Show(SQLe.Message,"Ocurrio un error");
            }
            finally
            {
                CerrarConexion();
            }

            return (matriz,colMatriz);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador.Sintactico
{
    public partial class Sintactico : Form
    {
       
        AnalizadorBottomUp miAnalizador;
        public Sintactico()
        {
            InitializeComponent();
        }       

        private void Sintactico_Load(object sender, EventArgs e)
        {
            string rutaBase = Directory.GetCurrentDirectory().Replace("bin\\Debug", "");
            System.IO.StreamReader sr = new System.IO.StreamReader($"{rutaBase}ArchivosTemporales\\Tokens.txt");
            rtxtTokens.Text = sr.ReadToEnd();
            sr.Close();

            AddLineNumbers(rtxtTokens, rtxtNumeracionTokens);

            dgvListaErrores.Columns.Add("Codigo", "Codigo");
            dgvListaErrores.Columns.Add("Descripcion", "Descripción");
            dgvListaErrores.Columns.Add("Linea", "Linea");
            dgvListaErrores.ReadOnly = true;
            dgvListaErrores.AllowUserToAddRows = false;
        }
        #region Enumeracion del textbox
        public int getWidth()
        {
            int w = 25;

            int line = rtxtTokens.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)rtxtTokens.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)rtxtTokens.Font.Size;
            }
            else
            {
                w = 50 + (int)rtxtTokens.Font.Size;
            }

            return w;
        }
        public void AddLineNumbers(RichTextBox codigo, RichTextBox numeracion)
        {

            Point pt = new Point(0, 0);

            int First_Index = codigo.GetCharIndexFromPosition(pt);
            int First_Line = codigo.GetLineFromCharIndex(First_Index);

            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;

            int Last_Index = codigo.GetCharIndexFromPosition(pt);
            int Last_Line = codigo.GetLineFromCharIndex(Last_Index);

            numeracion.SelectionAlignment = HorizontalAlignment.Center;

            numeracion.Text = "";
            numeracion.Width = getWidth();

            for (int i = First_Line; i <= Last_Line + 1; i++)
            {
                numeracion.Text += i + 1 + "\n";
            }
        }

        private void Analizador_Resize(object sender, EventArgs e)
        {
            AddLineNumbers(rtxtTokens, rtxtNumeracionTokens);
        }

        private void rtxtTokens_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = rtxtTokens.GetPositionFromCharIndex(rtxtTokens.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers(rtxtTokens, rtxtNumeracionTokens);
            }
        }

        private void rtxtTokens_VScroll(object sender, EventArgs e)
        {

            rtxtNumeracionTokens.Text = "";
            AddLineNumbers(rtxtTokens, rtxtNumeracionTokens);
            rtxtNumeracionTokens.Invalidate();
        }

        private void rtxtTokens_TextChanged(object sender, EventArgs e)
        {
            if (rtxtTokens.Text == "")
            {
                AddLineNumbers(rtxtTokens, rtxtNumeracionTokens);
            }
        }

        private void rtxtTokens_FontChanged(object sender, EventArgs e)
        {
            rtxtNumeracionTokens.Font = rtxtTokens.Font;
            rtxtTokens.Select();
            AddLineNumbers(rtxtTokens, rtxtNumeracionTokens);
        }

        private void rtxtLineNumber_MouseDown(object sender, MouseEventArgs e)
        {
            rtxtTokens.Select();
            rtxtNumeracionTokens.DeselectAll();
        }

#endregion

        private void cargarArchivoDeTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofdTokens.Title = "Selecciona el archivo a abrir.";
            ofdTokens.InitialDirectory = @"C:\Users\%username%\Desktop\";
            ofdTokens.Filter = "Archivos TXT | *.txt";
            if (ofdTokens.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofdTokens.FileName);
                rtxtTokens.Text = sr.ReadToEnd();
                sr.Close();
              
            }
            AddLineNumbers(rtxtTokens, rtxtNumeracionTokens);
        }

        private void iniciarAnalisisToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string[] tokensEntrada = rtxtTokens.Text.Split('\n');

            miAnalizador = new AnalizadorBottomUp();

            try
            {


                miAnalizador.Recorrido(tokensEntrada);

              
                tpErrores.Text = $"Lista de errores";
                rtxtSalida.Text = "El analizador termino la tarea con exito y sin errores.";

            }
            catch (Exception ex)
            {
                tpErrores.Text = $"Lista de errores: {miAnalizador.ErrorSintacticos.Count}.";
                rtxtSalida.Text = "El analizador termino la tarea con errores.\nRevisar la pestaña de errores.";
                dgvListaErrores.Rows.Clear();
                foreach (var err in miAnalizador.ErrorSintacticos)
                {
                    dgvListaErrores.Rows.Add(err.Codigo,err.Descripcion,err.Linea);
                }
                MessageBox.Show("La tarea concluyo con errores","Importante",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally {

                string derivaciones = "";
                foreach (var ent in miAnalizador.DerivacionesEntradas)
                {
                    derivaciones += "------------------Entrada----------------------\n";
                    derivaciones += $"{ent.Entrada}\n";
                    derivaciones += "----------------Derivaciones-------------------\n";
                    foreach (var der in ent.Derivaciones)
                    {
                        derivaciones += $"{der}\n";
                    }
                }
                rtxtDerivaciones.Text = derivaciones;

            }
        }

        private void volverALexicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Lexico.Analizador().Show(); 
            this.Hide();
        }

    }
}

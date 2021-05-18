using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalizadorLexico
{
    public partial class Analizador : Form
    {

        Automata miAutomata;
        public Analizador()
        {
            miAutomata = new Automata();

            InitializeComponent();
        }

        private void Analizador_Load(object sender, EventArgs e)
        {
            rtxtLineNumber.Font = rtxtCodigo.Font;
            rtxtCodigo.Select();
            AddLineNumbers();
        }
        #region Funcionalidad de la caja de texto para escribir codigo
        public int getWidth()
        {
            int w = 25;
            // get total lines of rtxtCodigo    
            int line = rtxtCodigo.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)rtxtCodigo.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)rtxtCodigo.Font.Size;
            }
            else
            {
                w = 50 + (int)rtxtCodigo.Font.Size;
            }

            return w;
        }
        public void AddLineNumbers()
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from rtxtCodigo    
            int First_Index = rtxtCodigo.GetCharIndexFromPosition(pt);
            int First_Line = rtxtCodigo.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from rtxtCodigo    
            int Last_Index = rtxtCodigo.GetCharIndexFromPosition(pt);
            int Last_Line = rtxtCodigo.GetLineFromCharIndex(Last_Index);
            // set Center alignment to rtxtLineNumber    
            rtxtLineNumber.SelectionAlignment = HorizontalAlignment.Center;
            // set rtxtLineNumber text to null & width to getWidth() function value    
            rtxtLineNumber.Text = "";
            rtxtLineNumber.Width = getWidth();
            // now add each line number to rtxtLineNumber upto last line    
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                rtxtLineNumber.Text += i + 1 + "\n";
            }
        }

        private void Analizador_Resize(object sender, EventArgs e)
        {
            AddLineNumbers();
        }

        private void rtxtCodigo_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = rtxtCodigo.GetPositionFromCharIndex(rtxtCodigo.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }

        private void rtxtCodigo_VScroll(object sender, EventArgs e)
        {

            rtxtLineNumber.Text = "";
            AddLineNumbers();
            rtxtLineNumber.Invalidate();
        }

        private void rtxtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (rtxtCodigo.Text == "")
            {
                AddLineNumbers();
            }
        }

        private void rtxtCodigo_FontChanged(object sender, EventArgs e)
        {
            rtxtLineNumber.Font = rtxtCodigo.Font;
            rtxtCodigo.Select();
            AddLineNumbers();
        }

        private void rtxtLineNumber_MouseDown(object sender, MouseEventArgs e)
        {
            rtxtCodigo.Select();
            rtxtLineNumber.DeselectAll();
        }

        #endregion

        private void iniciarAnalisisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miAutomata.ProximoEstado = 0;
            string cadena = rtxtCodigo.Text;
            String tokens = "";

            foreach (char caracter in cadena)
            {
               string result =  miAutomata.RecorridoMatriz(caracter);

                if (result == "SL")
                {
                    tokens += "\n";
                }
                else if (result != "")
                {
                    miAutomata.ProximoEstado = 0;
                    tokens += result+" ";
                }
            }

            rtxtTokens.Text = tokens;
        }

        private void guardarProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cargarProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

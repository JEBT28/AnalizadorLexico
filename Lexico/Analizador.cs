using Compilador.Sintactico;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Compilador.Lexico
{
    public partial class Analizador : Form
    {
        string rutaArchivo = @"";
        Automata miAutomata;
        AnalizadorBottomUp miAnalizadorBUP;

        List<Error> erroresEncontrados;
        public Analizador()
        {
            InitializeComponent();
            miAutomata = new Automata();
        }

        private void Analizador_Load(object sender, EventArgs e)
        {
            rtxtNumeracionCodigo.Font = rtxtCodigo.Font;
            rtxtCodigo.Select();

            AddLineNumbers(rtxtCodigo, rtxtNumeracionCodigo);

            dgvListaErrores.Columns.Add("Codigo", "Codigo");
            dgvListaErrores.Columns.Add("Descripcion", "Descripción");
            dgvListaErrores.Columns.Add("Linea", "Linea");
            dgvListaErrores.Columns.Add("Columna", "Columna");
            dgvListaErrores.ReadOnly = true;
            dgvListaErrores.AllowUserToAddRows = false;

            dgvTablaId.Columns.Add("Num", "# Identificador");
            dgvTablaId.Columns.Add("Nombre", "Nombre");
            dgvTablaId.Columns.Add("Tipo", "Tipo de dato");
            dgvTablaId.Columns.Add("Valor", "Valor");
            dgvTablaId.ReadOnly = true;
            dgvTablaId.AllowUserToAddRows = false;

            dgvTablaCN.Columns.Add("Num", "# Constante");
            dgvTablaCN.Columns.Add("Valor", "Valor");
            dgvTablaCN.Columns.Add("NumOP", "# Operador");
            dgvTablaCN.Columns.Add("Jerarquia", "Jerarquia");
            dgvTablaCN.ReadOnly = true;
            dgvTablaCN.AllowUserToAddRows = false;


            dgvErroresSintacticos.Columns.Add("Codigo", "Codigo");
            dgvErroresSintacticos.Columns.Add("Descripcion", "Descripción");
            dgvErroresSintacticos.Columns.Add("Linea", "Linea");
            dgvErroresSintacticos.ReadOnly = true;
            dgvErroresSintacticos.AllowUserToAddRows = false;


            dgvErroresSemanticos.Columns.Add("Codigo", "Codigo");
            dgvErroresSemanticos.Columns.Add("Descripcion", "Descripción");
            dgvErroresSemanticos.Columns.Add("Linea", "Linea");
            dgvErroresSemanticos.ReadOnly = true;
            dgvErroresSemanticos.AllowUserToAddRows = false;

        }
        #region Funcionalidad de la caja de texto para escribir codigo
        public int getWidth()
        {
            int w = 25;

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
            AddLineNumbers(rtxtCodigo, rtxtNumeracionCodigo);
        }

        private void rtxtCodigo_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = rtxtCodigo.GetPositionFromCharIndex(rtxtCodigo.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers(rtxtCodigo, rtxtNumeracionCodigo);
            }
        }

        private void rtxtCodigo_VScroll(object sender, EventArgs e)
        {

            rtxtNumeracionCodigo.Text = "";
            AddLineNumbers(rtxtCodigo, rtxtNumeracionCodigo);
            rtxtNumeracionCodigo.Invalidate();
        }

        private void rtxtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (rtxtCodigo.Text == "")
            {
                AddLineNumbers(rtxtCodigo, rtxtNumeracionCodigo);
            }
        }

        private void rtxtCodigo_FontChanged(object sender, EventArgs e)
        {
            rtxtNumeracionCodigo.Font = rtxtCodigo.Font;
            rtxtCodigo.Select();
            AddLineNumbers(rtxtCodigo, rtxtNumeracionCodigo);
        }

        private void rtxtLineNumber_MouseDown(object sender, MouseEventArgs e)
        {
            rtxtCodigo.Select();
            rtxtNumeracionCodigo.DeselectAll();
        }

        #endregion

        private void iniciarAnalisisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Declaracion de variables auxiliares 

            //Reset de la informacion de los controles del form
            dgvListaErrores.Rows.Clear();
            dgvTablaCN.Rows.Clear();
            dgvTablaId.Rows.Clear();
            rtxtTokens.Text = "";

            //Inicializar contadores y variables auxiliares
            int fila = 1;
            int columna = 1;
            int auxColumna = 0;
            string cadena = rtxtCodigo.Text;
            string fragmentoEvaluado = "";
            string fragmentoAnterior = "";
            erroresEncontrados = new List<Error>();

            // inicilaizacion de propiedades del automata
            miAutomata.ProximoEstado = 0;
            miAutomata.TablaIdentificadores = new List<Identificador>();
            miAutomata.TablaConstantesNum = new List<ConstanteNumerica>();

            try
            {
                //Recorrido de la cadena
                foreach (char caracter in cadena)
                {
                    fragmentoEvaluado += caracter;

                    //Llamada metodo del automata que determina el recorrido de la matriz
                    string result = miAutomata.RealizarMovimiento(caracter);

                    //Evaluacion del token devuelto
                    //Token igual a salto de linea
                    if (result == "SL")
                    {
                        columna = 0;

                        fila++;
                        AgregarToken("\n", Color.Black);

                        fragmentoEvaluado = "";
                    }
                    //Token existente devuelto
                    else if (result != "")
                    {
                        auxColumna++;

                        miAutomata.ProximoEstado = 0;
                        //El token es un error
                        if (result.Contains("ERROR"))
                        {
                            AgregarToken(result + " ", Color.Red);

                            Error nuevoError = miAutomata.IdentificarError(result);

                            nuevoError.Linea = fila;
                            nuevoError.Columna = columna;

                            erroresEncontrados.Add(nuevoError);
                        }
                        //El token es un identificador
                        else if (result.Contains("ID"))
                        {
                            result += miAutomata.ValidarIdentificador(fragmentoEvaluado, fragmentoAnterior, fila);

                            AgregarToken(result + " ", Color.Black);
                        }
                        //El token es una constante numerica
                        else if (result.Contains("CN"))
                        {
                            result += miAutomata.ValidarConstanteNumerica(fragmentoEvaluado);

                            AgregarToken(result + " ", Color.Black);
                        }
                        //El token es una palabra reservada o algun tipo de operador
                        else
                        {
                            AgregarToken(result + " ", Color.Black);
                        }

                        AddLineNumbers(rtxtTokens, rtxtNumeracionTokens);
                        columna += auxColumna;
                        auxColumna = 0;
                        fragmentoAnterior = fragmentoEvaluado;
                        fragmentoEvaluado = "";
                    }
                    //No devolvio un token
                    else
                    {
                        auxColumna++;
                    }
                }

                foreach (var iden in miAutomata.TablaIdentificadores)
                {
                    string linea = cadena.Split('\n')[iden.lineaDeclaracion - 1];
                    if (linea.Contains("="))
                    {
                        iden.Valor = linea.Substring(linea.IndexOf('=') + 1).Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ocurrio un error durante el recorrido de la cadena", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //Rutina que muestra los errores que fueron identificados durante el analisis
            if (erroresEncontrados.Count != 0)
            {
                foreach (Error er in erroresEncontrados)
                {
                    dgvListaErrores.Rows.Add(er.Codigo, er.Descripcion, er.Linea, er.Columna);
                }
                dgvListaErrores.AutoResizeColumns();

                tpgErrores.Text = $"Lista de errores lexicos ({erroresEncontrados.Count} encontrados)";
                rtxtSalida.Text = "El analizador termino la tarea y encontro errores, vea la lista de errores para mas información.";
                dgvListaErrores.AutoResizeColumns();

            }
            else
            {
                tpgErrores.Text = $"Lista de errores lexicos";
                rtxtSalida.Text = "El analizador termino la tarea con exito y sin errores.";

                string rutaBase = Directory.GetCurrentDirectory().Replace("bin\\Debug", "");
                EscribirArchivosTemporales($"{rutaBase}ArchivosTemporales\\Tokens.txt", rtxtTokens.Text);

                EscribirArchivosTemporales($"{rutaBase}ArchivosTemporales\\ConstantesNumericas.json", JsonConvert.SerializeObject(miAutomata.TablaConstantesNum));

                EscribirArchivosTemporales($"{rutaBase}ArchivosTemporales\\Identificadores.json", JsonConvert.SerializeObject(miAutomata.TablaIdentificadores));

                AnalisisSintacticoSemantico(rtxtTokens.Text.Split('\n'));

                EscribirArchivosTemporales($"{rutaBase}ArchivosTemporales\\program.asm", GenerarArchivoEnsamblador(rtxtCodigo.Text, miAutomata.TablaIdentificadores));


            }

            //Rutina que muestra la tabla de identificadores
            foreach (Identificador id in miAutomata.TablaIdentificadores)
            {
                dgvTablaId.Rows.Add(id.Id.ToString("00"), id.Nombre, id.Tipo, id.Valor);
            }

            //Rutina que muestra la tabla de constantes numericas
            foreach (ConstanteNumerica cn in miAutomata.TablaConstantesNum)
            {
                dgvTablaCN.Rows.Add(cn.Id.ToString("00"), cn.Valor, cn.NumeroOperador, cn.Jerarquia);
            }



        }

        private string GenerarArchivoEnsamblador(string codigo, List<Identificador> tablaidentificadores)
        {
            string[] Lineas = codigo.Split('\n');

            Ensamblador.CodigoEnsamblador generador = new Ensamblador.CodigoEnsamblador(TablaIdentificadores: tablaidentificadores);

            foreach (string linea in Lineas)
            {
                if (!String.IsNullOrWhiteSpace(linea.Trim()))
                {
                    generador.AgregarInstruccion(linea);
                }
            }

            return generador.CodigoASM;
        }

        public void AnalisisSintacticoSemantico(string[] tokensEntrada)
        {

            miAnalizadorBUP = new AnalizadorBottomUp();

            try
            {
                miAnalizadorBUP.Recorrido(tokensEntrada);

                tpgErroresSintacticos.Text = $"Lista de errores sintacticos";

                dgvListaErrores.Rows.Clear();
                dgvErroresSintacticos.Rows.Clear();

                rtxtSalida.Text = "El analizador termino la tarea con exito y sin errores.";

                if (miAnalizadorBUP.ErroresSemanticos.Count > 0)
                {
                    tpgErroresSemanticos.Text = $"Lista de errores semanticos: {miAnalizadorBUP.ErroresSemanticos.Count}.";
                    rtxtSalida.Text = "El analizador termino la tarea con errores semanticos.\nRevisar la pestaña de errores.";
                    dgvErroresSemanticos.Rows.Clear();

                    foreach (var err in miAnalizadorBUP.ErroresSemanticos)
                    {
                        dgvErroresSemanticos.Rows.Add(err.Codigo, err.Descripcion, err.Linea);
                    }

                }
                else
                {
                    tpgErroresSemanticos.Text = $"Lista de errores semanticos";
                    rtxtSalida.Text = "El analizador termino la tarea con exito y sin errores.";
                    dgvErroresSemanticos.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                tpgErroresSintacticos.Text = $"Lista de errores sintacticos: {miAnalizadorBUP.ErroresSintacticos.Count}.";
                rtxtSalida.Text = "El analizador termino la tarea con errores.\nRevisar la pestaña de errores.";
                dgvErroresSintacticos.Rows.Clear();
                foreach (var err in miAnalizadorBUP.ErroresSintacticos)
                {
                    dgvErroresSintacticos.Rows.Add(err.Codigo, err.Descripcion, err.Linea);
                }
                MessageBox.Show("La tarea concluyo con errores", "Importante", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (miAnalizadorBUP.ErroresSemanticos.Count > 0)
                {
                    tpgErroresSemanticos.Text = $"Lista de errores semanticos: {miAnalizadorBUP.ErroresSemanticos.Count}.";
                    rtxtSalida.Text = "El analizador termino la tarea con errores semanticos.\nRevisar la pestaña de errores.";
                    dgvErroresSemanticos.Rows.Clear();

                    foreach (var err in miAnalizadorBUP.ErroresSemanticos)
                    {
                        dgvErroresSemanticos.Rows.Add(err.Codigo, err.Descripcion, err.Linea);
                    }

                }
                else
                {
                    tpgErroresSemanticos.Text = $"Lista de errores semanticos";
                    rtxtSalida.Text = "El analizador termino la tarea con exito y sin errores.";
                    dgvErroresSemanticos.Rows.Clear();
                }
            }
            finally
            {

                string derivaciones = "";
                foreach (var ent in miAnalizadorBUP.DerivacionesEntradas)
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

        //Metodo que agrega los tokens al archivo de tokens y define el color del texto segun su tipo
        private void AgregarToken(string token, Color color)
        {
            int startIndex = rtxtTokens.Text.Length == 0 ? 0 : rtxtTokens.Text.Length - 1;

            int length = token.Length;

            rtxtTokens.AppendText(token);

            rtxtTokens.Select(startIndex, length);

            rtxtTokens.SelectionColor = color;
        }


        //Metodo que guarda el codigo escrito
        private void guardarProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivo.Trim()))
            {
                guardarComoToolStripMenuItem_Click(sender, e);
            }
            else
            {
                rtxtCodigo.SaveFile(rutaArchivo);
            }
        }

        //Metodo que carga el codigo guardado en un archivo 
        private void cargarProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofdCodigo.Title = "Selecciona el archivo a abrir.";
            ofdCodigo.InitialDirectory = @"C:\Users\%username%\Desktop\";
            ofdCodigo.Filter = "Archivos RTF | *.rtf";
            if (ofdCodigo.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = ofdCodigo.FileName;
                rtxtCodigo.LoadFile(rutaArchivo);
            }
            AddLineNumbers(rtxtCodigo, rtxtNumeracionCodigo);
        }

        //Metodo que redirige al analizador sintactico
        private void btnAnalisisSintactico_Click(object sender, EventArgs e)
        {
            if (erroresEncontrados.Count > 0)
            {
                MessageBox.Show("No se puede pasar al analizador sintactico porque contiene errores lexicos.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (String.IsNullOrWhiteSpace(rtxtCodigo.Text.Trim()))
            {
                MessageBox.Show("Nada que continuar analizando", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            new Sintactico.Sintactico().Show();


        }

        private void rtxtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (new Regex(@"[a-zA-Z0-9\s\-\báéíóü;:.,ñÑ#$&-_+*%'/{}()¿?¡!" + $"{'"'}]").IsMatch(e.KeyChar.ToString()) || e.KeyChar.Equals('|'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void NuevoProgramatoolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool nuevoArchivo = true;

            if (rtxtCodigo.Text.Length != 0)
            {
                nuevoArchivo = MessageBox.Show("¿Desea crear un nuevo archivo y descartar el actual?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes ? true : false;
            }

            if (nuevoArchivo)
            {
                rtxtCodigo.Text = "";
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sfdCodigo.Title = "Elige el lugar donde guardar el archivo.";
            sfdCodigo.InitialDirectory = @"C:\Users\%username%\Desktop\";
            sfdCodigo.Filter = "Archivos RTF | *.rtf";
            if (sfdCodigo.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = sfdCodigo.FileName;
                rtxtCodigo.SaveFile(sfdCodigo.FileName);
            }
            else
            {
                MessageBox.Show("Se cancelo la operacion", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Metodo que escribe en local los archivos que contienen informacion creada en el analizador lexico
        private void EscribirArchivosTemporales(string ruta, string contenido)
        {

            using (FileStream fileStream = new FileStream(ruta, FileMode.Create, FileAccess.ReadWrite))
            {
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.Write(contenido);
                streamWriter.Close();
                fileStream.Close();
            }


        }


    }
}

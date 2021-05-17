
namespace AnalizadorLexico
{
    partial class Analizador
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtxtLineNumber = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtxtCodigo = new System.Windows.Forms.RichTextBox();
            this.tabSalidas = new System.Windows.Forms.TabControl();
            this.tpgErrores = new System.Windows.Forms.TabPage();
            this.tpgSalida = new System.Windows.Forms.TabPage();
            this.rtxtTokens = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cargarProgramaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarProgramaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarAnalisisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.tabSalidas.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxtLineNumber
            // 
            this.rtxtLineNumber.BackColor = System.Drawing.Color.White;
            this.rtxtLineNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtLineNumber.Cursor = System.Windows.Forms.Cursors.PanNE;
            this.rtxtLineNumber.Dock = System.Windows.Forms.DockStyle.Left;
            this.rtxtLineNumber.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtLineNumber.ForeColor = System.Drawing.Color.SteelBlue;
            this.rtxtLineNumber.Location = new System.Drawing.Point(6, 6);
            this.rtxtLineNumber.Name = "rtxtLineNumber";
            this.rtxtLineNumber.ReadOnly = true;
            this.rtxtLineNumber.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtxtLineNumber.Size = new System.Drawing.Size(40, 431);
            this.rtxtLineNumber.TabIndex = 0;
            this.rtxtLineNumber.Text = "";
            this.rtxtLineNumber.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtxtLineNumber_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.rtxtCodigo);
            this.panel1.Controls.Add(this.rtxtLineNumber);
            this.panel1.Location = new System.Drawing.Point(12, 35);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(6);
            this.panel1.Size = new System.Drawing.Size(816, 443);
            this.panel1.TabIndex = 1;
            // 
            // rtxtCodigo
            // 
            this.rtxtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtCodigo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtCodigo.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtCodigo.Location = new System.Drawing.Point(46, 6);
            this.rtxtCodigo.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.rtxtCodigo.Name = "rtxtCodigo";
            this.rtxtCodigo.Size = new System.Drawing.Size(764, 431);
            this.rtxtCodigo.TabIndex = 1;
            this.rtxtCodigo.Text = "";
            this.rtxtCodigo.SelectionChanged += new System.EventHandler(this.rtxtCodigo_SelectionChanged);
            this.rtxtCodigo.VScroll += new System.EventHandler(this.rtxtCodigo_VScroll);
            this.rtxtCodigo.FontChanged += new System.EventHandler(this.rtxtCodigo_FontChanged);
            this.rtxtCodigo.TextChanged += new System.EventHandler(this.rtxtCodigo_TextChanged);
            // 
            // tabSalidas
            // 
            this.tabSalidas.Controls.Add(this.tpgErrores);
            this.tabSalidas.Controls.Add(this.tpgSalida);
            this.tabSalidas.Location = new System.Drawing.Point(12, 484);
            this.tabSalidas.Name = "tabSalidas";
            this.tabSalidas.SelectedIndex = 0;
            this.tabSalidas.Size = new System.Drawing.Size(816, 131);
            this.tabSalidas.TabIndex = 2;
            // 
            // tpgErrores
            // 
            this.tpgErrores.Location = new System.Drawing.Point(4, 22);
            this.tpgErrores.Name = "tpgErrores";
            this.tpgErrores.Padding = new System.Windows.Forms.Padding(3);
            this.tpgErrores.Size = new System.Drawing.Size(808, 105);
            this.tpgErrores.TabIndex = 0;
            this.tpgErrores.Text = "Lista de errores";
            this.tpgErrores.UseVisualStyleBackColor = true;
            // 
            // tpgSalida
            // 
            this.tpgSalida.Location = new System.Drawing.Point(4, 22);
            this.tpgSalida.Name = "tpgSalida";
            this.tpgSalida.Padding = new System.Windows.Forms.Padding(3);
            this.tpgSalida.Size = new System.Drawing.Size(808, 105);
            this.tpgSalida.TabIndex = 1;
            this.tpgSalida.Text = "Salida";
            this.tpgSalida.UseVisualStyleBackColor = true;
            // 
            // rtxtTokens
            // 
            this.rtxtTokens.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtTokens.Location = new System.Drawing.Point(864, 35);
            this.rtxtTokens.Name = "rtxtTokens";
            this.rtxtTokens.Size = new System.Drawing.Size(428, 443);
            this.rtxtTokens.TabIndex = 3;
            this.rtxtTokens.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cargarProgramaToolStripMenuItem,
            this.guardarProgramaToolStripMenuItem,
            this.iniciarAnalisisToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1316, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cargarProgramaToolStripMenuItem
            // 
            this.cargarProgramaToolStripMenuItem.Name = "cargarProgramaToolStripMenuItem";
            this.cargarProgramaToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
            this.cargarProgramaToolStripMenuItem.Text = "Cargar programa";
            // 
            // guardarProgramaToolStripMenuItem
            // 
            this.guardarProgramaToolStripMenuItem.Name = "guardarProgramaToolStripMenuItem";
            this.guardarProgramaToolStripMenuItem.Size = new System.Drawing.Size(116, 20);
            this.guardarProgramaToolStripMenuItem.Text = "Guardar programa";
            // 
            // iniciarAnalisisToolStripMenuItem
            // 
            this.iniciarAnalisisToolStripMenuItem.Name = "iniciarAnalisisToolStripMenuItem";
            this.iniciarAnalisisToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.iniciarAnalisisToolStripMenuItem.Text = "Iniciar analisis";
            // 
            // Analizador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1316, 627);
            this.Controls.Add(this.rtxtTokens);
            this.Controls.Add(this.tabSalidas);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Analizador";
            this.Text = "Analizador Lexico";
            this.Load += new System.EventHandler(this.Analizador_Load);
            this.Resize += new System.EventHandler(this.Analizador_Resize);
            this.panel1.ResumeLayout(false);
            this.tabSalidas.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtLineNumber;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rtxtCodigo;
        private System.Windows.Forms.TabControl tabSalidas;
        private System.Windows.Forms.TabPage tpgErrores;
        private System.Windows.Forms.TabPage tpgSalida;
        private System.Windows.Forms.RichTextBox rtxtTokens;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cargarProgramaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarProgramaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarAnalisisToolStripMenuItem;
    }
}


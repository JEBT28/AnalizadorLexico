
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Analizador));
            this.rtxtNumeracionCodigo = new System.Windows.Forms.RichTextBox();
            this.pnlEditor = new System.Windows.Forms.Panel();
            this.rtxtCodigo = new System.Windows.Forms.RichTextBox();
            this.tabSalidas = new System.Windows.Forms.TabControl();
            this.tpgSalida = new System.Windows.Forms.TabPage();
            this.rtxtSalida = new System.Windows.Forms.RichTextBox();
            this.tpgErrores = new System.Windows.Forms.TabPage();
            this.dgvListaErrores = new System.Windows.Forms.DataGridView();
            this.tpgId = new System.Windows.Forms.TabPage();
            this.dgvTablaId = new System.Windows.Forms.DataGridView();
            this.tpgCN = new System.Windows.Forms.TabPage();
            this.dgvTablaCN = new System.Windows.Forms.DataGridView();
            this.rtxtTokens = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cargarProgramaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarProgramaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarAnalisisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTokens = new System.Windows.Forms.Panel();
            this.rtxtNumeracionTokens = new System.Windows.Forms.RichTextBox();
            this.sfdCodigo = new System.Windows.Forms.SaveFileDialog();
            this.ofdCodigo = new System.Windows.Forms.OpenFileDialog();
            this.btnGuardarArchivoTokens = new System.Windows.Forms.Button();
            this.sfdTokens = new System.Windows.Forms.SaveFileDialog();
            this.pnlEditor.SuspendLayout();
            this.tabSalidas.SuspendLayout();
            this.tpgSalida.SuspendLayout();
            this.tpgErrores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaErrores)).BeginInit();
            this.tpgId.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTablaId)).BeginInit();
            this.tpgCN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTablaCN)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.pnlTokens.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxtNumeracionCodigo
            // 
            this.rtxtNumeracionCodigo.BackColor = System.Drawing.Color.White;
            this.rtxtNumeracionCodigo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtNumeracionCodigo.Cursor = System.Windows.Forms.Cursors.PanNE;
            this.rtxtNumeracionCodigo.Dock = System.Windows.Forms.DockStyle.Left;
            this.rtxtNumeracionCodigo.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtNumeracionCodigo.ForeColor = System.Drawing.Color.SteelBlue;
            this.rtxtNumeracionCodigo.Location = new System.Drawing.Point(6, 6);
            this.rtxtNumeracionCodigo.Name = "rtxtNumeracionCodigo";
            this.rtxtNumeracionCodigo.ReadOnly = true;
            this.rtxtNumeracionCodigo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtxtNumeracionCodigo.Size = new System.Drawing.Size(40, 431);
            this.rtxtNumeracionCodigo.TabIndex = 0;
            this.rtxtNumeracionCodigo.Text = "";
            this.rtxtNumeracionCodigo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtxtLineNumber_MouseDown);
            // 
            // pnlEditor
            // 
            this.pnlEditor.BackColor = System.Drawing.Color.White;
            this.pnlEditor.Controls.Add(this.rtxtCodigo);
            this.pnlEditor.Controls.Add(this.rtxtNumeracionCodigo);
            this.pnlEditor.Location = new System.Drawing.Point(12, 35);
            this.pnlEditor.Name = "pnlEditor";
            this.pnlEditor.Padding = new System.Windows.Forms.Padding(6);
            this.pnlEditor.Size = new System.Drawing.Size(816, 443);
            this.pnlEditor.TabIndex = 1;
            // 
            // rtxtCodigo
            // 
            this.rtxtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtCodigo.Dock = System.Windows.Forms.DockStyle.Right;
            this.rtxtCodigo.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtCodigo.Location = new System.Drawing.Point(51, 6);
            this.rtxtCodigo.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.rtxtCodigo.Name = "rtxtCodigo";
            this.rtxtCodigo.Size = new System.Drawing.Size(759, 431);
            this.rtxtCodigo.TabIndex = 1;
            this.rtxtCodigo.Text = "";
            this.rtxtCodigo.SelectionChanged += new System.EventHandler(this.rtxtCodigo_SelectionChanged);
            this.rtxtCodigo.VScroll += new System.EventHandler(this.rtxtCodigo_VScroll);
            this.rtxtCodigo.FontChanged += new System.EventHandler(this.rtxtCodigo_FontChanged);
            this.rtxtCodigo.TextChanged += new System.EventHandler(this.rtxtCodigo_TextChanged);
            // 
            // tabSalidas
            // 
            this.tabSalidas.Controls.Add(this.tpgSalida);
            this.tabSalidas.Controls.Add(this.tpgErrores);
            this.tabSalidas.Controls.Add(this.tpgId);
            this.tabSalidas.Controls.Add(this.tpgCN);
            this.tabSalidas.Location = new System.Drawing.Point(12, 484);
            this.tabSalidas.Name = "tabSalidas";
            this.tabSalidas.SelectedIndex = 0;
            this.tabSalidas.Size = new System.Drawing.Size(816, 131);
            this.tabSalidas.TabIndex = 2;
            // 
            // tpgSalida
            // 
            this.tpgSalida.Controls.Add(this.rtxtSalida);
            this.tpgSalida.Location = new System.Drawing.Point(4, 22);
            this.tpgSalida.Name = "tpgSalida";
            this.tpgSalida.Padding = new System.Windows.Forms.Padding(3);
            this.tpgSalida.Size = new System.Drawing.Size(808, 105);
            this.tpgSalida.TabIndex = 1;
            this.tpgSalida.Text = "Salida";
            this.tpgSalida.UseVisualStyleBackColor = true;
            // 
            // rtxtSalida
            // 
            this.rtxtSalida.BackColor = System.Drawing.Color.White;
            this.rtxtSalida.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtSalida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtSalida.Location = new System.Drawing.Point(3, 3);
            this.rtxtSalida.Name = "rtxtSalida";
            this.rtxtSalida.ReadOnly = true;
            this.rtxtSalida.Size = new System.Drawing.Size(802, 99);
            this.rtxtSalida.TabIndex = 0;
            this.rtxtSalida.Text = "";
            // 
            // tpgErrores
            // 
            this.tpgErrores.Controls.Add(this.dgvListaErrores);
            this.tpgErrores.Location = new System.Drawing.Point(4, 22);
            this.tpgErrores.Name = "tpgErrores";
            this.tpgErrores.Padding = new System.Windows.Forms.Padding(3);
            this.tpgErrores.Size = new System.Drawing.Size(808, 105);
            this.tpgErrores.TabIndex = 0;
            this.tpgErrores.Text = "Lista de errores";
            this.tpgErrores.UseVisualStyleBackColor = true;
            // 
            // dgvListaErrores
            // 
            this.dgvListaErrores.BackgroundColor = System.Drawing.Color.White;
            this.dgvListaErrores.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvListaErrores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaErrores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListaErrores.Location = new System.Drawing.Point(3, 3);
            this.dgvListaErrores.Name = "dgvListaErrores";
            this.dgvListaErrores.RowHeadersVisible = false;
            this.dgvListaErrores.Size = new System.Drawing.Size(802, 99);
            this.dgvListaErrores.TabIndex = 0;
            // 
            // tpgId
            // 
            this.tpgId.Controls.Add(this.dgvTablaId);
            this.tpgId.Location = new System.Drawing.Point(4, 22);
            this.tpgId.Name = "tpgId";
            this.tpgId.Size = new System.Drawing.Size(808, 105);
            this.tpgId.TabIndex = 2;
            this.tpgId.Text = "Tabla Identificadores";
            this.tpgId.UseVisualStyleBackColor = true;
            // 
            // dgvTablaId
            // 
            this.dgvTablaId.BackgroundColor = System.Drawing.Color.White;
            this.dgvTablaId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTablaId.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTablaId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTablaId.Location = new System.Drawing.Point(0, 0);
            this.dgvTablaId.Name = "dgvTablaId";
            this.dgvTablaId.RowHeadersVisible = false;
            this.dgvTablaId.Size = new System.Drawing.Size(808, 105);
            this.dgvTablaId.TabIndex = 1;
            // 
            // tpgCN
            // 
            this.tpgCN.Controls.Add(this.dgvTablaCN);
            this.tpgCN.Location = new System.Drawing.Point(4, 22);
            this.tpgCN.Name = "tpgCN";
            this.tpgCN.Size = new System.Drawing.Size(808, 105);
            this.tpgCN.TabIndex = 3;
            this.tpgCN.Text = "Tabla Constantes";
            this.tpgCN.UseVisualStyleBackColor = true;
            // 
            // dgvTablaCN
            // 
            this.dgvTablaCN.BackgroundColor = System.Drawing.Color.White;
            this.dgvTablaCN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTablaCN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTablaCN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTablaCN.Location = new System.Drawing.Point(0, 0);
            this.dgvTablaCN.Name = "dgvTablaCN";
            this.dgvTablaCN.RowHeadersVisible = false;
            this.dgvTablaCN.Size = new System.Drawing.Size(808, 105);
            this.dgvTablaCN.TabIndex = 1;
            // 
            // rtxtTokens
            // 
            this.rtxtTokens.BackColor = System.Drawing.Color.White;
            this.rtxtTokens.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtTokens.Dock = System.Windows.Forms.DockStyle.Right;
            this.rtxtTokens.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.rtxtTokens.Location = new System.Drawing.Point(52, 6);
            this.rtxtTokens.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.rtxtTokens.Name = "rtxtTokens";
            this.rtxtTokens.ReadOnly = true;
            this.rtxtTokens.Size = new System.Drawing.Size(400, 431);
            this.rtxtTokens.TabIndex = 3;
            this.rtxtTokens.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cargarProgramaToolStripMenuItem,
            this.guardarProgramaToolStripMenuItem,
            this.iniciarAnalisisToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1316, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "Guardar como";
            // 
            // cargarProgramaToolStripMenuItem
            // 
            this.cargarProgramaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cargarProgramaToolStripMenuItem.Image")));
            this.cargarProgramaToolStripMenuItem.Name = "cargarProgramaToolStripMenuItem";
            this.cargarProgramaToolStripMenuItem.Size = new System.Drawing.Size(125, 20);
            this.cargarProgramaToolStripMenuItem.Text = "Cargar programa";
            this.cargarProgramaToolStripMenuItem.Click += new System.EventHandler(this.cargarProgramaToolStripMenuItem_Click);
            // 
            // guardarProgramaToolStripMenuItem
            // 
            this.guardarProgramaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("guardarProgramaToolStripMenuItem.Image")));
            this.guardarProgramaToolStripMenuItem.Name = "guardarProgramaToolStripMenuItem";
            this.guardarProgramaToolStripMenuItem.Size = new System.Drawing.Size(132, 20);
            this.guardarProgramaToolStripMenuItem.Text = "Guardar programa";
            this.guardarProgramaToolStripMenuItem.Click += new System.EventHandler(this.guardarProgramaToolStripMenuItem_Click);
            // 
            // iniciarAnalisisToolStripMenuItem
            // 
            this.iniciarAnalisisToolStripMenuItem.Image = global::AnalizadorLexico.Properties.Resources.start;
            this.iniciarAnalisisToolStripMenuItem.Name = "iniciarAnalisisToolStripMenuItem";
            this.iniciarAnalisisToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.iniciarAnalisisToolStripMenuItem.Text = "Iniciar analisis";
            this.iniciarAnalisisToolStripMenuItem.Click += new System.EventHandler(this.iniciarAnalisisToolStripMenuItem_Click);
            // 
            // pnlTokens
            // 
            this.pnlTokens.BackColor = System.Drawing.Color.White;
            this.pnlTokens.Controls.Add(this.rtxtNumeracionTokens);
            this.pnlTokens.Controls.Add(this.rtxtTokens);
            this.pnlTokens.Location = new System.Drawing.Point(846, 35);
            this.pnlTokens.Name = "pnlTokens";
            this.pnlTokens.Padding = new System.Windows.Forms.Padding(6);
            this.pnlTokens.Size = new System.Drawing.Size(458, 443);
            this.pnlTokens.TabIndex = 2;
            // 
            // rtxtNumeracionTokens
            // 
            this.rtxtNumeracionTokens.BackColor = System.Drawing.Color.White;
            this.rtxtNumeracionTokens.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtNumeracionTokens.Cursor = System.Windows.Forms.Cursors.PanNE;
            this.rtxtNumeracionTokens.Dock = System.Windows.Forms.DockStyle.Left;
            this.rtxtNumeracionTokens.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtNumeracionTokens.ForeColor = System.Drawing.Color.SteelBlue;
            this.rtxtNumeracionTokens.Location = new System.Drawing.Point(6, 6);
            this.rtxtNumeracionTokens.Name = "rtxtNumeracionTokens";
            this.rtxtNumeracionTokens.ReadOnly = true;
            this.rtxtNumeracionTokens.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtxtNumeracionTokens.Size = new System.Drawing.Size(40, 431);
            this.rtxtNumeracionTokens.TabIndex = 0;
            this.rtxtNumeracionTokens.Text = "";
            // 
            // sfdCodigo
            // 
            this.sfdCodigo.DefaultExt = "rtf";
            // 
            // ofdCodigo
            // 
            this.ofdCodigo.FileName = "openFileDialog1";
            // 
            // btnGuardarArchivoTokens
            // 
            this.btnGuardarArchivoTokens.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnGuardarArchivoTokens.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuardarArchivoTokens.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardarArchivoTokens.ForeColor = System.Drawing.Color.White;
            this.btnGuardarArchivoTokens.Location = new System.Drawing.Point(846, 504);
            this.btnGuardarArchivoTokens.Name = "btnGuardarArchivoTokens";
            this.btnGuardarArchivoTokens.Size = new System.Drawing.Size(458, 27);
            this.btnGuardarArchivoTokens.TabIndex = 5;
            this.btnGuardarArchivoTokens.Text = "Guardar archivo de tokens";
            this.btnGuardarArchivoTokens.UseVisualStyleBackColor = false;
            this.btnGuardarArchivoTokens.Click += new System.EventHandler(this.btnGuardarArchivoTokens_Click);
            // 
            // Analizador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1316, 627);
            this.Controls.Add(this.btnGuardarArchivoTokens);
            this.Controls.Add(this.pnlTokens);
            this.Controls.Add(this.tabSalidas);
            this.Controls.Add(this.pnlEditor);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Analizador";
            this.Text = "Analizador Lexico";
            this.Load += new System.EventHandler(this.Analizador_Load);
            this.Resize += new System.EventHandler(this.Analizador_Resize);
            this.pnlEditor.ResumeLayout(false);
            this.tabSalidas.ResumeLayout(false);
            this.tpgSalida.ResumeLayout(false);
            this.tpgErrores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaErrores)).EndInit();
            this.tpgId.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTablaId)).EndInit();
            this.tpgCN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTablaCN)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlTokens.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtNumeracionCodigo;
        private System.Windows.Forms.Panel pnlEditor;
        private System.Windows.Forms.RichTextBox rtxtCodigo;
        private System.Windows.Forms.TabControl tabSalidas;
        private System.Windows.Forms.TabPage tpgErrores;
        private System.Windows.Forms.TabPage tpgSalida;
        private System.Windows.Forms.RichTextBox rtxtTokens;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cargarProgramaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarProgramaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarAnalisisToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvListaErrores;
        private System.Windows.Forms.RichTextBox rtxtSalida;
        private System.Windows.Forms.Panel pnlTokens;
        private System.Windows.Forms.RichTextBox rtxtNumeracionTokens;
        private System.Windows.Forms.SaveFileDialog sfdCodigo;
        private System.Windows.Forms.OpenFileDialog ofdCodigo;
        private System.Windows.Forms.TabPage tpgId;
        private System.Windows.Forms.DataGridView dgvTablaId;
        private System.Windows.Forms.TabPage tpgCN;
        private System.Windows.Forms.DataGridView dgvTablaCN;
        private System.Windows.Forms.Button btnGuardarArchivoTokens;
        private System.Windows.Forms.SaveFileDialog sfdTokens;
    }
}


﻿
namespace Compilador.Sintactico
{
    partial class Sintactico
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sintactico));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpSalida = new System.Windows.Forms.TabPage();
            this.tpErrores = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cargarArchivoDeTokensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarAnalisisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.ofdTokens = new System.Windows.Forms.OpenFileDialog();
            this.pnlEditor = new System.Windows.Forms.Panel();
            this.rtxtTokens = new System.Windows.Forms.RichTextBox();
            this.rtxtNumeracionTokens = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pnlEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpSalida);
            this.tabControl1.Controls.Add(this.tpErrores);
            this.tabControl1.Location = new System.Drawing.Point(30, 493);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(840, 122);
            this.tabControl1.TabIndex = 0;
            // 
            // tpSalida
            // 
            this.tpSalida.Location = new System.Drawing.Point(4, 22);
            this.tpSalida.Name = "tpSalida";
            this.tpSalida.Padding = new System.Windows.Forms.Padding(3);
            this.tpSalida.Size = new System.Drawing.Size(832, 96);
            this.tpSalida.TabIndex = 0;
            this.tpSalida.Text = "Salida";
            this.tpSalida.UseVisualStyleBackColor = true;
            // 
            // tpErrores
            // 
            this.tpErrores.Location = new System.Drawing.Point(4, 22);
            this.tpErrores.Name = "tpErrores";
            this.tpErrores.Padding = new System.Windows.Forms.Padding(3);
            this.tpErrores.Size = new System.Drawing.Size(832, 162);
            this.tpErrores.TabIndex = 1;
            this.tpErrores.Text = "Lista de errores";
            this.tpErrores.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cargarArchivoDeTokensToolStripMenuItem,
            this.iniciarAnalisisToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1316, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cargarArchivoDeTokensToolStripMenuItem
            // 
            this.cargarArchivoDeTokensToolStripMenuItem.Name = "cargarArchivoDeTokensToolStripMenuItem";
            this.cargarArchivoDeTokensToolStripMenuItem.Size = new System.Drawing.Size(150, 20);
            this.cargarArchivoDeTokensToolStripMenuItem.Text = "Cargar archivo de tokens";
            this.cargarArchivoDeTokensToolStripMenuItem.Click += new System.EventHandler(this.cargarArchivoDeTokensToolStripMenuItem_Click);
            // 
            // iniciarAnalisisToolStripMenuItem
            // 
            this.iniciarAnalisisToolStripMenuItem.Name = "iniciarAnalisisToolStripMenuItem";
            this.iniciarAnalisisToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.iniciarAnalisisToolStripMenuItem.Text = "Iniciar analisis";
            this.iniciarAnalisisToolStripMenuItem.Click += new System.EventHandler(this.iniciarAnalisisToolStripMenuItem_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(852, 44);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(438, 443);
            this.richTextBox2.TabIndex = 3;
            this.richTextBox2.Text = "";
            // 
            // ofdTokens
            // 
            this.ofdTokens.FileName = "tokens";
            // 
            // pnlEditor
            // 
            this.pnlEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlEditor.BackColor = System.Drawing.Color.White;
            this.pnlEditor.Controls.Add(this.rtxtTokens);
            this.pnlEditor.Controls.Add(this.rtxtNumeracionTokens);
            this.pnlEditor.Location = new System.Drawing.Point(30, 44);
            this.pnlEditor.Name = "pnlEditor";
            this.pnlEditor.Padding = new System.Windows.Forms.Padding(6);
            this.pnlEditor.Size = new System.Drawing.Size(816, 443);
            this.pnlEditor.TabIndex = 4;
            // 
            // rtxtTokens
            // 
            this.rtxtTokens.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtTokens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtTokens.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtTokens.Location = new System.Drawing.Point(46, 6);
            this.rtxtTokens.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.rtxtTokens.Name = "rtxtTokens";
            this.rtxtTokens.Size = new System.Drawing.Size(764, 431);
            this.rtxtTokens.TabIndex = 1;
            this.rtxtTokens.Text = "";
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
            // Sintactico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1316, 627);
            this.Controls.Add(this.pnlEditor);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1332, 666);
            this.Name = "Sintactico";
            this.Text = "Sintactico";
            this.Load += new System.EventHandler(this.Sintactico_Load);
            this.tabControl1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlEditor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpSalida;
        private System.Windows.Forms.TabPage tpErrores;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cargarArchivoDeTokensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarAnalisisToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.OpenFileDialog ofdTokens;
        private System.Windows.Forms.Panel pnlEditor;
        private System.Windows.Forms.RichTextBox rtxtTokens;
        private System.Windows.Forms.RichTextBox rtxtNumeracionTokens;
    }
}
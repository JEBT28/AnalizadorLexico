
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpSalida);
            this.tabControl1.Controls.Add(this.tpErrores);
            this.tabControl1.Location = new System.Drawing.Point(30, 423);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(840, 188);
            this.tabControl1.TabIndex = 0;
            // 
            // tpSalida
            // 
            this.tpSalida.Location = new System.Drawing.Point(4, 22);
            this.tpSalida.Name = "tpSalida";
            this.tpSalida.Padding = new System.Windows.Forms.Padding(3);
            this.tpSalida.Size = new System.Drawing.Size(832, 162);
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
            // 
            // iniciarAnalisisToolStripMenuItem
            // 
            this.iniciarAnalisisToolStripMenuItem.Name = "iniciarAnalisisToolStripMenuItem";
            this.iniciarAnalisisToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.iniciarAnalisisToolStripMenuItem.Text = "Iniciar analisis";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(24, 44);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(621, 369);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(669, 44);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(621, 369);
            this.richTextBox2.TabIndex = 3;
            this.richTextBox2.Text = "";
            // 
            // Sintactico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1316, 627);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1332, 666);
            this.Name = "Sintactico";
            this.Text = "Sintactico";
            this.tabControl1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
    }
}
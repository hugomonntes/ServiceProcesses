namespace SERV_HILOS_EX3
{
    partial class Form1
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
            this.txbDescarga = new System.Windows.Forms.TextBox();
            this.btnDescargar = new System.Windows.Forms.Button();
            this.txbMostrar = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnD2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txbDescarga
            // 
            this.txbDescarga.Location = new System.Drawing.Point(146, 53);
            this.txbDescarga.Name = "txbDescarga";
            this.txbDescarga.Size = new System.Drawing.Size(331, 20);
            this.txbDescarga.TabIndex = 0;
            // 
            // btnDescargar
            // 
            this.btnDescargar.Location = new System.Drawing.Point(483, 53);
            this.btnDescargar.Name = "btnDescargar";
            this.btnDescargar.Size = new System.Drawing.Size(82, 20);
            this.btnDescargar.TabIndex = 1;
            this.btnDescargar.Text = "Descargar";
            this.btnDescargar.UseVisualStyleBackColor = true;
            this.btnDescargar.Click += new System.EventHandler(this.btnDescargar_Click);
            // 
            // txbMostrar
            // 
            this.txbMostrar.Location = new System.Drawing.Point(146, 79);
            this.txbMostrar.Multiline = true;
            this.txbMostrar.Name = "txbMostrar";
            this.txbMostrar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbMostrar.Size = new System.Drawing.Size(331, 85);
            this.txbMostrar.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(146, 191);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(331, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(146, 217);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(331, 85);
            this.textBox2.TabIndex = 4;
            // 
            // btnD2
            // 
            this.btnD2.Location = new System.Drawing.Point(493, 191);
            this.btnD2.Name = "btnD2";
            this.btnD2.Size = new System.Drawing.Size(82, 20);
            this.btnD2.TabIndex = 5;
            this.btnD2.Text = "Descargar";
            this.btnD2.UseVisualStyleBackColor = true;
            this.btnD2.Click += new System.EventHandler(this.btnDescargar2_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnDescargar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnD2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txbMostrar);
            this.Controls.Add(this.btnDescargar);
            this.Controls.Add(this.txbDescarga);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbDescarga;
        private System.Windows.Forms.Button btnDescargar;
        private System.Windows.Forms.TextBox txbMostrar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnD2;
    }
}


namespace SERV_HILOS_EX4
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
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnBusqueda = new System.Windows.Forms.Button();
            this.btnPosicion = new System.Windows.Forms.Button();
            this.btnHttp = new System.Windows.Forms.Button();
            this.txbChars = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(151, 39);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(413, 20);
            this.txtUrl.TabIndex = 0;
            // 
            // btnBusqueda
            // 
            this.btnBusqueda.Location = new System.Drawing.Point(12, 130);
            this.btnBusqueda.Name = "btnBusqueda";
            this.btnBusqueda.Size = new System.Drawing.Size(75, 23);
            this.btnBusqueda.TabIndex = 1;
            this.btnBusqueda.Text = "Busqueda";
            this.btnBusqueda.UseVisualStyleBackColor = true;
            this.btnBusqueda.Click += new System.EventHandler(this.btnBusqueda_Click);
            // 
            // btnPosicion
            // 
            this.btnPosicion.Location = new System.Drawing.Point(322, 65);
            this.btnPosicion.Name = "btnPosicion";
            this.btnPosicion.Size = new System.Drawing.Size(75, 23);
            this.btnPosicion.TabIndex = 2;
            this.btnPosicion.Text = "Posicion";
            this.btnPosicion.UseVisualStyleBackColor = true;
            // 
            // btnHttp
            // 
            this.btnHttp.Location = new System.Drawing.Point(489, 65);
            this.btnHttp.Name = "btnHttp";
            this.btnHttp.Size = new System.Drawing.Size(75, 23);
            this.btnHttp.TabIndex = 3;
            this.btnHttp.Text = "HTTP";
            this.btnHttp.UseVisualStyleBackColor = true;
            // 
            // txbChars
            // 
            this.txbChars.Location = new System.Drawing.Point(93, 133);
            this.txbChars.Name = "txbChars";
            this.txbChars.Size = new System.Drawing.Size(145, 20);
            this.txbChars.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txbChars);
            this.Controls.Add(this.btnHttp);
            this.Controls.Add(this.btnPosicion);
            this.Controls.Add(this.btnBusqueda);
            this.Controls.Add(this.txtUrl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnBusqueda;
        private System.Windows.Forms.Button btnPosicion;
        private System.Windows.Forms.Button btnHttp;
        private System.Windows.Forms.TextBox txbChars;
    }
}


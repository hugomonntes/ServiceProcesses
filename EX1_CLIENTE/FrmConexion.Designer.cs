namespace EX1_CLIENTE
{
    partial class FrmConexion
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
            this.components = new System.ComponentModel.Container();
            this.IP = new System.Windows.Forms.Label();
            this.txbIp = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblPuerto = new System.Windows.Forms.Label();
            this.txbPuerto = new System.Windows.Forms.TextBox();
            this.btnConectar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IP
            // 
            this.IP.AutoSize = true;
            this.IP.Location = new System.Drawing.Point(21, 9);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(17, 13);
            this.IP.TabIndex = 0;
            this.IP.Text = "IP";
            // 
            // txbIp
            // 
            this.txbIp.Location = new System.Drawing.Point(53, 6);
            this.txbIp.Name = "txbIp";
            this.txbIp.Size = new System.Drawing.Size(204, 20);
            this.txbIp.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // lblPuerto
            // 
            this.lblPuerto.AutoSize = true;
            this.lblPuerto.Location = new System.Drawing.Point(9, 36);
            this.lblPuerto.Name = "lblPuerto";
            this.lblPuerto.Size = new System.Drawing.Size(38, 13);
            this.lblPuerto.TabIndex = 3;
            this.lblPuerto.Text = "Puerto";
            // 
            // txbPuerto
            // 
            this.txbPuerto.Location = new System.Drawing.Point(53, 33);
            this.txbPuerto.Name = "txbPuerto";
            this.txbPuerto.Size = new System.Drawing.Size(204, 20);
            this.txbPuerto.TabIndex = 4;
            // 
            // btnConectar
            // 
            this.btnConectar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConectar.Location = new System.Drawing.Point(97, 65);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(75, 23);
            this.btnConectar.TabIndex = 5;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            // 
            // FrmConexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 100);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.txbPuerto);
            this.Controls.Add(this.lblPuerto);
            this.Controls.Add(this.txbIp);
            this.Controls.Add(this.IP);
            this.Name = "FrmConexion";
            this.Text = "FrmConexion";
            this.Load += new System.EventHandler(this.FrmConexion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IP;
        public System.Windows.Forms.TextBox txbIp;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label lblPuerto;
        public System.Windows.Forms.TextBox txbPuerto;
        public System.Windows.Forms.Button btnConectar;
    }
}
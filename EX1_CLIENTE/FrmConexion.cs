using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EX1_CLIENTE
{
    public partial class FrmConexion : Form
    {
        public FrmConexion()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e) // TODO hacer logica inversa y asociar a boton 
        {
            
            if (!IPAddress.TryParse(txbIp.Text, out _))
            {
                MessageBox.Show("IP incorrecta");
            }

            if (!int.TryParse(txbPuerto.Text, out _))
            {
                MessageBox.Show("Puerto incorrecto");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FrmConexion_Load(object sender, EventArgs e)
        {

        }
    }
}

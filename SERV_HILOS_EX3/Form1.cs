using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SERV_HILOS_EX3
{
    public partial class Form1 : Form
    {
        public Form1() // TODO Curro ya se que tengo que cambiarle el icono al formulario
        {
            InitializeComponent();
        }

        async Task<string> DownloadFileAsync(string fileName, int delayMs)
        {
            await Task.Delay(delayMs);
            return $"File {fileName} descargada en {delayMs} ms";
        }

        Random r = new Random();
        public int getNumeroAleatorio(int limit)
        {
            return r.Next(4000,4000 + limit);
        }

        private async void btnDescargar_Click(object sender, EventArgs e)
        {
            int delay = getNumeroAleatorio(2000);
            string texto = await DownloadFileAsync(txbDescarga.Text, delay);
            txbMostrar.Text += texto + Environment.NewLine;
        }

        private async void btnDescargar2_Click(object sender, EventArgs e)
        {
            int delay = getNumeroAleatorio(2000);
            string texto = await DownloadFileAsync(textBox1.Text, delay);
            textBox2.Text += texto + Environment.NewLine;
        }
    }
}

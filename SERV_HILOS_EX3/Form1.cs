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
        public Form1()
        {
            InitializeComponent();
        }

        async Task<string> DownloadFileAsync(string fileName, int delayMs)
        {
            return $"File {fileName} descargada en {delayMs} ms";
        }

        Random r = new Random();
        public int getNumeroAleatorio(int limit)
        {
            return r.Next(limit);
        }

        private async void btnDescargar_Click(object sender, EventArgs e)
        {
            int delay = getNumeroAleatorio(10);
            Task<string> texto = DownloadFileAsync(txbDescarga.Text, delay);
            string b = await texto;
            await Task.Delay(delay * 100);
            txbMostrar.Text += b + Environment.NewLine;
        }
    }
}

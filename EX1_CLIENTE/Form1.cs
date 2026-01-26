using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EX1_CLIENTE//Titulo, icono. Acceptbutton en  sec.
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        IPAddress ip = IPAddress.Parse("127.0.0.1");
        int port = 9000;

        public async Task<String> DataManager(string comando)
        {
            try
            {
                using (Socket conexion = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp))
                {
                    IPEndPoint ep = new IPEndPoint(ip, port);
                    await conexion.ConnectAsync(ep);

                    Encoding codificacion = Console.OutputEncoding;
                    using (NetworkStream ns = new NetworkStream(conexion))
                    using (StreamReader sr = new StreamReader(ns, codificacion))
                    using (StreamWriter sw = new StreamWriter(ns, codificacion))
                    {
                        sw.AutoFlush = true;
                        string msg = await sr.ReadLineAsync();
                        await sw.WriteLineAsync(comando);
                        msg = await sr.ReadLineAsync();
                        return msg;
                    }
                }
            }
            catch (SocketException)
            {
                return "No se pudo conectar con el servidor";
            }
            catch (IOException)
            {
                return "Error de comunicación con el servidor";
            }
            catch (ObjectDisposedException)
            {
                return "Conexión cerrada inesperadamente";
            }
        }

        private async void Buttons_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Name == "btnClose")
            {
                if (txbPassword.Text == "")
                {
                    await DataManager("close");
                    lblResultado.Text = "Resultado: Introduce una password";
                }
                else
                {
                    await DataManager($"close {txbPassword.Text}");
                    lblResultado.Text = "Resultado: Servidor Cerrado";
                }
            }
            else
            {
                lblResultado.Text = $"Resultado: {await DataManager(((Button)sender).Text)}";
            }
        }

        private void btnConexion_Click(object sender, EventArgs e)
        {
            FrmConexion form = new FrmConexion();
            form.txbIp.Text = ip.ToString();
            form.txbPuerto.Text = port.ToString();
            bool flag = true;
            DialogResult result;
            result = form.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                MessageBox.Show("No se han guardado la Ip y Puerto", "OJO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (result == DialogResult.OK)
            {
                int puertoMaximo = IPEndPoint.MaxPort;
                flag = true;
                if (!IPAddress.TryParse(form.txbIp.Text, out IPAddress ipValidada))
                {
                    MessageBox.Show("Error con la IP", "IP NO VÁLIDA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    flag = false;
                }
                if (!int.TryParse(form.txbPuerto.Text, out int puertoValidado))
                {
                    MessageBox.Show("Error en el puerto", "PUERTO NO VÁLIDO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    flag = false;
                }
                if (puertoValidado < 0 || puertoValidado > puertoMaximo)
                {
                    MessageBox.Show("Error puerto fuera de rango", "PUERTO NO VÁLIDO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    flag = false;
                }
                if (flag)
                {
                    ip = ipValidada;
                    port = puertoValidado;
                    lblResultado.Text = $"Ip:{ip.ToString()}, Puerto:{port.ToString()}";
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

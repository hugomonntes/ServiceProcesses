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

namespace EX1_CLIENTE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string ip { set; get; } = "127.0.0.1";
        public static int port { set; get; } = 9000;

        public async Task<String> DataManager(string comando, string password)
        {
            if (!IPAddress.TryParse(ip, out IPAddress ipChecked))
            {
                return "IP no válida";
            }

            try
            {
                using (Socket socket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp))
                {
                    await socket.ConnectAsync(ipChecked, port);

                    using (NetworkStream network = new NetworkStream(socket))
                    using (StreamReader sr = new StreamReader(network, Console.OutputEncoding))
                    using (StreamWriter sw = new StreamWriter(network, Console.OutputEncoding))
                    {
                        sw.AutoFlush = true;

                        await sw.WriteLineAsync(comando);
                        string comando2 = sr.ReadLine();
                        await sw.WriteLineAsync(password);
                        string password2 = sr.ReadLine();


                        string respuesta = await sr.ReadLineAsync();
                        return respuesta;
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
            Button btn = sender as Button;
            string comando = btn.Text.ToString();
            string password = txbPassword.Text;

            if (string.IsNullOrWhiteSpace(password))
            {
                lblResultado.Text = "Introduce una contraseña";
            }

            string resultado = await DataManager(comando, password);

            lblResultado.Text = resultado;
        }

        private void btnConexion_Click(object sender, EventArgs e)
        {
            FrmConexion frm = new FrmConexion();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                ip = frm.txbIp.Text;
                port = int.Parse(frm.txbPuerto.Text);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

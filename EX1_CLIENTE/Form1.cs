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
                    using (StreamReader sr = new StreamReader(network, Encoding.UTF8))
                    using (StreamWriter sw = new StreamWriter(network, Encoding.UTF8))
                    {
                        sw.AutoFlush = true;

                        await sw.WriteLineAsync(comando);
                        await sw.WriteLineAsync(password);
                        string comando2 = sr.ReadLine();
                        string password2 = sr.ReadLine();


                        string respuesta = await sr.ReadLineAsync();
                        return respuesta ?? "Sin respuesta del servidor";
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

        public async void Buttons_click(object sender, EventArgs e) // TODO completar
        {
            string buttonContent = ((Button)sender).Text;
            string password = txbPassword.Text; // Hacer comprobaciones
            string message = await DataManager(ip, port);
        }

        private void btnConexion_Click(object sender, EventArgs e)
        {
            FrmConexion frmConexion = new FrmConexion();
            if (validationField(frmConexion.txbIp) && validationField(frmConexion.txbPuerto))
            {
                ip = frmConexion.txbIp.Text;
                int.TryParse(frmConexion.txbPuerto.Text, out port); // TODO comprobar si esto tira excepcion o algo en caso de fallo o como gestionarlo
                frmConexion.btnConectar.Click += async (sender2, e2) => { await DataManager(ip, port); };
            }
            frmConexion.ShowDialog();
        }

        private bool validationField(TextBox textBox)
        {
            if (textBox.Text.Trim() == null || textBox.Text.Trim() == "")
            {
                return false;
            }
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

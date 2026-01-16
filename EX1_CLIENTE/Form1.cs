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

        public static string ip;
        public static int port;

        public async Task<String> DataManager(string ip, int port) // Para validar IP utilizar la clase IPAdress tiene un tryparse
        {
            if (ip != null && IPAddress.TryParse(ip, out IPAddress ipChecked))
            {
                try
                {
                    using (Socket socketConnect = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                    {
                        IPEndPoint iPEnd = new IPEndPoint(ipChecked, port);
                        await socketConnect.ConnectAsync(iPEnd);

                        Encoding encoding = Console.OutputEncoding;
                        using (NetworkStream network = new NetworkStream(socketConnect))
                        using (StreamReader sr = new StreamReader(network, encoding))
                        using (StreamWriter sw = new StreamWriter(network, encoding))
                        {
                            sw.AutoFlush = true;
                            string message = await sr.ReadLineAsync();
                            await sw.WriteLineAsync();
                            message = await sr.ReadLineAsync();
                            return message;
                        }
                    }
                }
                catch (Exception ex) when (ex is SocketException || ex is IOException)
                {
                    return ex.Message;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return null; // TODO check
        }

        public async void Buttons_click(object sender, EventArgs e)
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
    }
}

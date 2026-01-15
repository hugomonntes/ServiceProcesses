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

        public async Task<String> DataManager(IPAddress ip, int port)
        {
            try
            {
                using (Socket socketConnect = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IPEndPoint iPEnd = new IPEndPoint(ip, port);
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

        public async void Buttons_click(object sender, EventArgs e)
        {
            string message = await DataManager(IPAddress.Parse("127.0.0.1"), 31416);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

        public async Task<Button> clickButtons(IPAddress ip, int port)
        {
            try
            {
                using (Socket connect = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IPEndPoint iPEnd = new IPEndPoint(ip, port);
                    await connect.ConnectAsync(iPEnd);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}

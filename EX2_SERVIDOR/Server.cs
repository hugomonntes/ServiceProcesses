using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EX2_SERVIDOR
{
    internal class Server
    {
        private List<Usuario> usuarios = new List<Usuario>();
        private int port;
        private bool ServerIsRunning = false;
        public static Socket socket;
        public string userName;

        public void Init()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            using (socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(endPoint);
                socket.Listen(500); // TODO cambiar numero escuchas
                do
                {
                    Socket client = socket.Accept();
                    Thread thread = new Thread(() => ClientManager(client));
                }
                while (ServerIsRunning);
            }
        }

        public int isFreePort(int port)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            using (socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                bool isFree = true;
                do
                {
                    try
                    {
                        socket.Bind(endPoint);
                        socket.Listen(1);
                    }
                    catch (SocketException s) when (s.SocketErrorCode == SocketError.AddressAlreadyInUse) // TODO OJO utilizo propiedad socketError no ErrorCode
                    {
                        port++;
                        isFree = false;
                    }
                    catch (SocketException)
                    {
                        port++;
                        isFree = false;
                    }
                }
                while (!isFree);
                return port;
            }
        }

        public void Stop()
        {

        }

        public void ClientManager(Socket socketClient)
        {
            using (socketClient)
            {
                IPEndPoint clientEndPoint = (IPEndPoint)socketClient.RemoteEndPoint;
                using (NetworkStream network = new NetworkStream(socketClient))
                using (StreamWriter sw = new StreamWriter(network, Console.OutputEncoding))
                using (StreamReader sr = new StreamReader(network, Console.OutputEncoding))
                {
                    sw.AutoFlush = true;
                    try
                    {
                        userName = sr.ReadLine();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
    }
}

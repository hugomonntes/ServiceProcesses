using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EX1_SERVIDOR
{
    internal class Server
    {
        public bool ServerIsRunning { get; set; } = true;
        public int Port { get; set; } = 31416; // TODO comprobar puerto libre

        public void Init()
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, Port);
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(iPEndPoint);
                socket.Listen(10);
                Console.WriteLine($"Conectado a {iPEndPoint}"); // TODO Revisar
                while (ServerIsRunning)
                {
                    Socket client = socket.Accept();
                    Thread thread = new Thread(() => ClientManager(client));
                    thread.Start();
                }

            }
        }

        private void ClientManager(Socket socketClient)
        {
            using (socketClient)
            {
                IPEndPoint clientEndPoint = (IPEndPoint)socketClient.RemoteEndPoint;
                Console.WriteLine($"Cliente conectado desde {clientEndPoint.Address}:{clientEndPoint.Port}");
                Encoding encoding = Console.OutputEncoding;
                using (NetworkStream networkStream = new NetworkStream(socketClient))
                using (StreamReader sReader = new StreamReader(networkStream, encoding))
                using (StreamWriter sWriter = new StreamWriter(networkStream, encoding))
                {
                    sWriter.AutoFlush = true;
                    string init = "START";
                    sWriter.WriteLine(init);
                    string? command = "";
                    while (command != null)
                    {
                        try
                        {
                            command = sReader.ReadLine().Trim();

                            if (command == "time")
                            {
                                sWriter.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
                            }
                            else if (command == "date")
                            {
                                sWriter.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
                            }
                            else if (command == "all")
                            {
                                sWriter.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                            }
                            else if (command.StartsWith("close"))
                            {
                                // TODO crear una función para gestionar contraseñas
                            }
                            else
                            {
                                sWriter.WriteLine("ERROR: Comando no reconocido");
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
        }
    }
}

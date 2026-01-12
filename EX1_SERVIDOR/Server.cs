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
        public string closePassword { get; set; } = $"close {ReadFile("password")}";

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
                    sWriter.WriteLine("START");
                    string command = "";
                    do // TODO quitar do while
                    {
                        try
                        {
                            command = sReader.ReadLine().Trim(); // TODO NullPointerException
                            switch (command)
                            {
                                case "time":
                                    sWriter.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
                                    break;
                                case "date":
                                    sWriter.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
                                    break;
                                case "all":
                                    sWriter.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                                    break;
                                case "close":
                                    //TODO crear una función para gestionar contraseñas
                                    break;
                                case closePassword: // TODO fix
                                    //TODO crear una función para gestionar contraseñas
                                    break;
                                default:
                                    sWriter.WriteLine("ERROR: Comando no reconocido");
                                    break;
                            }
                            if (command == $"close {closePassword}")
                            {
                                sWriter.WriteLine(closePassword);
                            }
                        }
                        catch (Exception ex) when (ex is SocketException || ex is IOException)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    }
                    while (command != null);
                }
            }
        }

        public static string ReadFile(string FileName)
        {
            string path = $"{Environment.GetEnvironmentVariable("programdata")}\\{FileName}.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd().Trim();
            }
        }
    }
}

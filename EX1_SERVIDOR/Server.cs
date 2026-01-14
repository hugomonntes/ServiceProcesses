using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EX1_SERVIDOR
{
    internal class Server
    {
        public bool ServerIsRunning { get; set; } = true;
        public int Port { get; set; } = 9000; // TODO comprobar puerto libre
        public string Password { get; set; } = ReadFile("password");
        private Socket socketServer;

        public void InitServer(int Port)
        {
            bool PortUsed = false;
            do
            {
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, Port);
                using (socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    try
                    {
                        socketServer.Bind(iPEndPoint);
                        socketServer.Listen(10);
                        Console.WriteLine($"Conectado a {iPEndPoint}");
                    }
                    catch (SocketException s) when (s.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                    {
                        PortUsed = true;
                        Port++;
                    } catch (SocketException)
                    {
                        PortUsed = true;
                        Port++;
                    }

                }
            } while (!PortUsed);


            while (ServerIsRunning)
            {
                try
                {
                    Socket client = socketServer.Accept();
                    Thread thread = new Thread(() => ClientManager(client));
                    thread.IsBackground = true;
                    thread.Start();
                }
                catch (SocketException s)
                {
                }
            }
        }

        public void StopServer(Socket socket)
        {
            ServerIsRunning = false;
            socket.Close();
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
                    sWriter.WriteLine("CLIENT");
                    string command = "";
                    try
                    {
                        command = sReader.ReadLine();
                        if (command != null)
                        {
                            command = command.Trim();
                        }

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
                        else if (command == $"close {Password}")
                        {
                            //close password: Junto con el comando close se debe verificar que viene
                            //una contraseña. Si esta es correcta el servidor ha de finalizar y se lo
                            //indica al cliente.Si no devuelve un mensaje de error al cliente(Debe
                            //diferenciarse el error de contraseña no válida o que no se haya enviado
                            //la contraseña).
                            sWriter.WriteLine($"close {Password}");
                            StopServer(socketServer);
                            sWriter.WriteLine("Conexión con el servidor finalizada");
                        }
                        else if (command == $"close")
                        {
                            sWriter.WriteLine("Comando close sin contraseña");
                        }
                        else if (command != $"close {Password}")
                        {
                            sWriter.WriteLine("Contraseña Incorrecta");
                        }
                        else
                        {
                            sWriter.WriteLine("ERROR: Comando no reconocido");
                        }
                    }
                    catch (Exception ex) when (ex is SocketException || ex is IOException)
                    {
                        Console.WriteLine(ex.Message);
                    }
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

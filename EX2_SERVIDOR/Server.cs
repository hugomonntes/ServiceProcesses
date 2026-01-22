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
        private List<StreamWriter> streamWriters = new List<StreamWriter>();
        private int port = 9000;
        private bool ServerIsRunning = true;
        public Socket socket;
        public object lockObject = new object();

        public void Init()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            using (socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(endPoint);
                socket.Listen(500); // TODO cambiar numero escuchas
                Console.WriteLine("Server Running...");
                do
                {
                    Socket client = socket.Accept();
                    Thread thread = new Thread(() => ClientManager(client));
                    thread.Start();
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
            socket.Close();
            ServerIsRunning = false;
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
                        sw.WriteLine("Introduce tu nombre:");
                        string userName = sr.ReadLine();
                        Usuario usuario;
                        lock (lockObject)
                        {
                            usuario = new Usuario(userName, clientEndPoint.Address.ToString(), sw);
                            usuarios.Add(usuario);
                        }
                        while (true) // Creo que solo interesa salir del bucle si hacen exit
                        {
                            string msg = sr.ReadLine();
                            streamWriters.Add(sw);
                            foreach (StreamWriter writer in streamWriters)
                            {
                                if (writer != sw)
                                {
                                    writer.WriteLine($"{usuario.nombre}: {msg}");
                                }
                            }
                            
                            if (msg == "#exit")
                            {
                                lock (lockObject)
                                {
                                    usuarios.Remove(usuario);
                                }
                                socketClient.Close();
                            }

                            if (msg == "#list")
                            {
                                foreach (Usuario user in usuarios)
                                {
                                    sw.WriteLine(user.nombre);
                                }
                            }
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

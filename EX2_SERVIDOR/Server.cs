using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EX2_SERVIDOR//Control cierre abrupto
{
    internal class Server
    {
        private List<Usuario> usuarios = new List<Usuario>();
        private readonly int port = 9001;
        private bool ServerIsRunning = true;
        public object lockUsers = new object();

        public void Init()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, getFreePort(port));
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(endPoint);
                socket.Listen(500); // TODO cambiar numero escuchas
                Console.WriteLine($"Server Running in port {port}....");
                do
                {
                    Socket client = socket.Accept();
                    Thread thread = new Thread(() => RequestManager(client));
                    thread.Start();
                }
                while (ServerIsRunning);
            }
        }

        public int getFreePort(int initialPort) // Comprobar Maxport
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, initialPort);
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                bool isFree = false;
                do
                {
                    try
                    {
                        socket.Bind(endPoint);
                        socket.Listen(1);
                        isFree = true;
                    }
                    catch (SocketException)
                    {
                        initialPort++;
                    }
                }
                while (!isFree);
                return initialPort;
            }
        }

        public void Stop(Socket socket)
        {
            socket.Close();
            ServerIsRunning = false;
        }

        public void RequestManager(Socket requestSocket)
        {
            using (requestSocket)
            {
                IPEndPoint clientEndPoint = (IPEndPoint)requestSocket.RemoteEndPoint;
                using (NetworkStream network = new NetworkStream(requestSocket))
                using (StreamWriter sw = new StreamWriter(network, Console.OutputEncoding))
                using (StreamReader sr = new StreamReader(network, Console.OutputEncoding))
                {
                    sw.AutoFlush = true;
                    bool isConnected = true;
                    try
                    {
                        sw.WriteLine("Introduce tu nombre:");
                        string userName = sr.ReadLine();
                        Usuario usuario;
                        lock (lockUsers)
                        {
                            usuario = new Usuario(userName, clientEndPoint.Address.ToString(), sw);
                            usuarios.Add(usuario);
                            BroadcastMessage(usuario, $"{usuario.nombre} se ha unido al chat");
                        }

                        while (isConnected)
                        {
                            string msg = sr.ReadLine();
                            if (msg == null)
                            {
                                isConnected = false;
                            }
                            else
                            {
                                switch (msg)
                                {
                                    case "#list":
                                        lock (lockUsers)
                                        {
                                            foreach (Usuario user in usuarios)
                                            {
                                                sw.WriteLine($"Conectado: {user.nombre}");
                                            }
                                        }
                                        break;
                                    case "#exit":
                                        lock (lockUsers)
                                        {
                                            usuarios.Remove(usuario);
                                            requestSocket.Close();
                                            isConnected = false;
                                            BroadcastMessage(usuario, $"{usuario.nombre} ha dejado el chat");
                                        }
                                        break;
                                    default:
                                        lock (lockUsers)
                                        {
                                            BroadcastMessage(usuario, $"{usuario.nombre}: {msg}");
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    catch (IOException)
                    {

                    }
                }
            }
        }

        public void BroadcastMessage(Usuario sender, string msg)
        {
            lock (lockUsers)
            {
                foreach (Usuario user in usuarios)
                {
                    if (user.StreamWriter != sender.StreamWriter)
                    {
                        user.StreamWriter.WriteLine(msg);
                    }
                }
            }
        }

    }
}

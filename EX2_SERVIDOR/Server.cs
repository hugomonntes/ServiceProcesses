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
        private readonly int port = 9001;
        private bool ServerIsRunning = true;
        public Socket socket;
        public object lockUsers = new object();

        public void Init()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, getFreePort(port));
            using (socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(endPoint);
                socket.Listen(500); // TODO cambiar numero escuchas
                Console.WriteLine("Server Running....");
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
            using (socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
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

        public void Stop()
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

                        Usuario usuario = new Usuario(userName, clientEndPoint.Address.ToString(), sw);
                        usuarios.Add(usuario);
                        BroadcastMessage(usuario,$"{usuario.nombre} se ha unido al chat");

                        while (isConnected)
                        {
                            string msg = sr.ReadLine();
                            if (msg == null)
                            {
                                isConnected = false; // TODO OJO con cerrado abrupto cierra todos dsps hacer lock o algo 
                            }
                            else
                            {
                                lock (lockUsers)
                                {
                                    switch (msg)
                                    {
                                        case "#list":
                                            foreach (Usuario user in usuarios)
                                            {
                                                sw.WriteLine($"Conectado: {user.nombre}");
                                            }
                                            break;
                                        case "#exit":
                                            usuarios.Remove(usuario);
                                            requestSocket.Close();
                                            isConnected = false;
                                            BroadcastMessage(usuario,$"{usuario.nombre} ha dejado el chat");
                                            break;
                                        default:
                                            BroadcastMessage(usuario,$"{usuario.nombre}: {msg}");
                                            break;
                                    }
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

        public void BroadcastMessage(Usuario sender, string msg)
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

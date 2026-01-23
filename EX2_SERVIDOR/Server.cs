using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EX2_SERVIDOR
{
    internal class Server // TODO ojo con el cierre forzado de un cliente queda en bucle infinito(corregido)
    {
        private List<Usuario> usuarios = new List<Usuario>();
        private int port = 9000;
        private bool ServerIsRunning = true;
        public Socket socket;
        public object lockUsers = new object();

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

        public void ClientManager(Socket socketClient) // Hacer inyeccion streamwriter fuera del bucle, controlar hilos,
                                                       // corregir si se escibre un commando no enviar por chat, OJO si un user
                                                       // se sale con texot excrito cierra conexion del server
        {
            using (socketClient)
            {
                IPEndPoint clientEndPoint = (IPEndPoint)socketClient.RemoteEndPoint;
                using (NetworkStream network = new NetworkStream(socketClient))
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
                        }
                        while (isConnected)
                        {
                            string msg = sr.ReadLine();
                            if (msg == null)
                            {
                                isConnected = false;
                            }

                            lock (lockUsers)
                            {
                                if (isConnected)
                                {
                                    foreach (Usuario user in usuarios)
                                    {
                                        if (user.StreamWriter != sw)
                                        {
                                            user.StreamWriter.WriteLine($"{usuario.nombre}: {msg}");
                                        }
                                    }
                                }
                            }

                            if (msg == "#exit")
                            {
                                lock (lockUsers)
                                {
                                    if (isConnected)
                                    {
                                        usuarios.Remove(usuario);
                                    }
                                }
                                socketClient.Close();
                                isConnected = false;
                            }

                            if (msg == "#list")
                            {
                                foreach (Usuario user in usuarios)
                                {
                                    sw.WriteLine($"Conectado: {user.nombre}");
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

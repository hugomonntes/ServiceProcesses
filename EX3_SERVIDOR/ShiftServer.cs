using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EX3_SERVIDOR
{
    internal class ShiftServer
    {
        string[] users;
        List<string> waitQueue = new List<string>();
        int port = 31416;
        Socket socket;
        bool serverIsRunning = true;

        public void ReadNames(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    users = sr.ReadToEnd().Split(";");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error en el archivo");
            }
        }

        public int ReadPin(string path)
        {
            string pin = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string content = sr.ReadToEnd().Trim(); // OJO con excepcion en caso de null (probar)
                    for (int i = 0; i < 4; i++)
                    {
                        pin += content[i];
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error con el archivo");
                return -1;
            }
            return int.Parse(pin) | -1;
        }

        public bool IsFreePort(int port)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, port);
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    socket.Bind(iPEndPoint);
                    socket.Listen(1);
                }
                catch (SocketException)
                {
                    return false;
                }
                return true;
            }
        }

        public int GetFreePort(int initialPort)
        {
            IPEndPoint iP = new IPEndPoint(IPAddress.Any, initialPort);
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                bool isFree = false;
                do
                {
                    try
                    {
                        socket.Bind(iP);
                        socket.Listen(1);
                        isFree = true;
                    }
                    catch (SocketException)
                    {
                        initialPort++;
                    }
                }
                while (!isFree && initialPort < IPEndPoint.MaxPort); // Comprobar lógica del bucle
                return initialPort;
            }
        }


        public void Init()
        {
            if (!IsFreePort(port))
            {
                port = GetFreePort(1024);
            }
            Console.WriteLine("Puerto: " + port);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, port);
            using (socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(iPEndPoint);
                socket.Listen(100);
                Console.WriteLine($"Usuario conectado");

                while (serverIsRunning)
                {
                    Socket client = socket.Accept();
                    Thread thread = new Thread(() => RequestManager(client));
                    thread.Start();
                }
            }
            //ReadNames($"{Environment.GetEnvironmentVariable("userprofile")}\\usuarios.txt");

        }

        public void StopServer()
        {
            serverIsRunning = false;
            socket.Close();
        }

        public void RequestManager(Socket socket)
        {
            using (socket)
            {
                IPEndPoint ip = (IPEndPoint)socket.RemoteEndPoint;
                using (NetworkStream network = new NetworkStream(socket))
                using (StreamReader streamReader = new StreamReader(network, Console.OutputEncoding))
                using (StreamWriter streamWriter = new StreamWriter(network, Console.OutputEncoding))
                {
                    streamWriter.AutoFlush = true;
                    streamWriter.WriteLine("Bienvenido");
                    streamWriter.WriteLine("Introduce tu nombre: ");
                    try
                    {
                        string nombre = streamReader.ReadLine();

                    }
                    catch (IOException e) { }

                }
            }

        }
    }
}

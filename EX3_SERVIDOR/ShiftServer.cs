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
        string[] users = ReadNames($"{Environment.GetEnvironmentVariable("userprofile")}\\usuarios.txt");
        List<string> waitQueue = loadList($"{Environment.GetEnvironmentVariable("userprofile")}\\waitQueue.txt");
        private readonly object lockQueue = new object();
        int port = 31416;
        Socket socketServer;
        bool serverIsRunning = true;

        public static string[] ReadNames(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    return sr.ReadToEnd().Split(";");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error en el archivo");
                return new string[0];
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
            return int.Parse(pin); // FIXME
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
            bool isFree = false;
            do
            {
                try
                {
                    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                    {
                        socket.Bind(iP);
                        socket.Listen(1);
                        isFree = true;
                    }
                }
                catch (SocketException)
                {
                    initialPort++;
                }
                return initialPort;
            }
            while (!isFree && initialPort < IPEndPoint.MaxPort); // Comprobar lógica del bucle
        }


        public void Init()
        {
            if (!IsFreePort(port))
            {
                port = GetFreePort(1024);
            }
            Console.WriteLine("Puerto: " + port);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, port);
            using (socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socketServer.Bind(iPEndPoint);
                socketServer.Listen(100);
                Console.WriteLine($"Usuario conectado");
                Console.WriteLine($"{Environment.GetEnvironmentVariable("userprofile")}\\usuarios.txt");
                while (serverIsRunning)
                {
                    try
                    {
                        Socket client = socketServer.Accept();
                        Thread thread = new Thread(() => RequestManager(client));
                        thread.Start();
                    }
                    catch (SocketException) { }
                }
            }

        }

        public void Stop()
        {
            serverIsRunning = false;
            socketServer.Close();
        }

        public void RequestManager(Socket socketClient)
        {
            using (socketClient)
            {
                IPEndPoint ip = (IPEndPoint)socketClient.RemoteEndPoint;
                using (NetworkStream network = new NetworkStream(socketClient))
                using (StreamReader sr = new StreamReader(network, Console.OutputEncoding))
                using (StreamWriter sw = new StreamWriter(network, Console.OutputEncoding))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine("Bienvenido");
                    sw.Write("Introduce tu nombre: ");
                    try
                    {
                        string userName = sr.ReadLine();
                        if (userOnList(users, userName) || userName == "admin")
                        {
                            if (userName == "admin")
                            {
                                MenuAdmin(sr, sw, socketClient);
                            }
                            else
                            {
                                MenuUser(sr, sw, userName);
                            }
                        }
                        else
                        {
                            sw.WriteLine("Usuario Desconectado");
                            socketClient.Close();
                        }
                    }
                    catch (IOException e) { }
                }
            }
        }

        public bool userOnList(string[] names, string nameToSearch)
        {
            foreach (string name in names)
            {
                if (name == nameToSearch)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<string> loadList(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string[] list = sr.ReadToEnd().Split(Environment.NewLine);
                    return list.ToList();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("No se pudo cargar archiovos");
                return new List<string>();
            }
        }

        private void MenuAdmin(StreamReader sr, StreamWriter sw, Socket socketClient)
        {
            sw.Write("Introduce un pin: ");
            bool pinChecked = pinChecked = int.TryParse(sr.ReadLine(), out int pin);
            int correctPin = ReadPin($"{Environment.GetEnvironmentVariable("userprofile")}\\pin.txt");
            string command;
            bool adminIsConnected = true;
            if (pinChecked && correctPin == pin)
            {
                do
                {
                    sw.Write("Introduce un comando (list | add | del X | chpin X | shutdown | exit): ");
                    command = sr.ReadLine();

                    string[] comandoDoble = command.Split(" ");

                    switch (comandoDoble[0])
                    {
                        case "list":
                            lock (lockQueue)
                            {
                                for (int i = 0; i < waitQueue.Count; i++)
                                {
                                    sw.WriteLine($"{waitQueue[i]}");
                                }
                            }
                            break;
                        case "add":
                            lock (lockQueue)
                            {
                                waitQueue.Add($"admin - {DateTime.Now:dd/MM/yyyy - HH:mm:ss}");
                                sw.WriteLine("OK");
                            }
                            break;
                        case "del":
                            if (comandoDoble.Length > 1 && int.TryParse(comandoDoble[1], out int index))
                            {
                                lock (lockQueue)
                                {
                                    if (index >= 0 && index < waitQueue.Count)
                                    {
                                        waitQueue.RemoveAt(index);
                                        sw.WriteLine("Usuario eliminado");
                                    }
                                    else
                                    {
                                        sw.WriteLine("indice no válido");
                                    }
                                }
                            }
                            break;

                        case "chpin":
                            if (comandoDoble.Length > 1 && int.TryParse(comandoDoble[1], out int newPin))
                            {
                                using (StreamWriter sw2 = new StreamWriter($"{Environment.GetEnvironmentVariable("userprofile")}\\pin.txt"))
                                {
                                    sw2.WriteLine(newPin);
                                }

                                sw.WriteLine("Pin cambiado");
                            }
                            break;

                        case "shutdown":
                            lock (lockQueue)
                            {
                                using (StreamWriter sw3 = new StreamWriter($"{Environment.GetEnvironmentVariable("userprofile")}\\waitQueue.txt"))
                                {
                                    foreach (var item in waitQueue)
                                    {
                                        sw3.WriteLine(item);
                                    }
                                }
                            }

                            Stop();
                            adminIsConnected = false;
                            break;

                        case "exit":
                            sw.WriteLine("Desconectando...");
                            adminIsConnected = false;
                            break;

                        default:
                            sw.WriteLine("Comando no válido");
                            break;
                    }

                } while (adminIsConnected);
            }
            else
            {
                sw.WriteLine("Pin Incorrecto");
            }
        }

        private void MenuUser(StreamReader sr, StreamWriter sw, string userName)
        {
            string command;

            do
            {
                sw.Write("Introduce un comando (list | add): ");
                command = sr.ReadLine();

                switch (command)
                {
                    case "list":
                        lock (lockQueue)
                        {
                            foreach (string user in waitQueue)
                            {
                                sw.WriteLine(user);
                            }
                        }
                        break;

                    case "add":
                        lock (lockQueue)
                        {
                            if (!userOnList(waitQueue.ToArray(), userName))
                            {
                                waitQueue.Add($"{userName} - {DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss")}");
                                sw.WriteLine("OK");
                            }
                            else
                            {
                                sw.WriteLine("Este user ya exite");
                            }
                        }
                        break;
                    default:
                        sw.WriteLine("Comando no válido");
                        break;
                }

            } while (command != "list" && command != "add");
        }


    }
}

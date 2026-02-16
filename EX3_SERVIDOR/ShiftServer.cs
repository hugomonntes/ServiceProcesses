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
                            if (userName == "admin") // TODO revisar para mejorarlo
                            {
                                sw.Write("Intorduce un pin: ");
                                int pin = int.Parse(sr.ReadLine());
                                int correctPin;
                                try
                                {
                                    correctPin = ReadPin($"{Environment.GetEnvironmentVariable("userprofile")}\\pin.txt");
                                }
                                catch
                                {
                                    correctPin = 1234;
                                }
                                if (pin == correctPin)
                                {
                                    string command;
                                    do
                                    {
                                        sw.Write("Introduce un comando (list | add | del + pos | chpin + pin...): ");
                                        command = sr.ReadLine();
                                        string[] comandoDoble = new string[2];
                                        if (command != null)
                                        {
                                            comandoDoble = command.Split(" ");
                                        }
                                        switch (comandoDoble[0])
                                        {
                                            case "list":
                                                foreach (string user in waitQueue)
                                                {
                                                    sw.WriteLine(user);
                                                }
                                                break;
                                            case "add":
                                                if (!userOnList(waitQueue.ToArray(), userName))
                                                {
                                                    waitQueue.Add($"{userName} - {DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss")}");
                                                    sw.WriteLine("OK");
                                                }
                                                else
                                                {
                                                    sw.WriteLine("Este user ya exite");
                                                }
                                                break;
                                            case "del":
                                                if (int.TryParse(comandoDoble[1], out int comandoChecked) && comandoChecked > 0 && comandoChecked < waitQueue.Count())
                                                {
                                                    waitQueue.RemoveAt(comandoChecked);
                                                }
                                                else
                                                {
                                                    sw.WriteLine("Error al eliminar user");
                                                }
                                                break;
                                            case "chpin":
                                                if (int.TryParse(comandoDoble[1], out int pinChecked))
                                                {
                                                    using (StreamWriter sw2 = new StreamWriter($"{Environment.GetEnvironmentVariable("userprofile")}\\pin.txt"))
                                                    {
                                                        sw2.WriteLine(pinChecked);
                                                        sw.WriteLine("Pin guardado bien");
                                                    }
                                                }
                                                else
                                                {
                                                    sw.WriteLine("Error al guardar pin");
                                                }
                                                break;
                                            case "exit":
                                                sw.WriteLine("Desconectando...");
                                                socketClient.Close();
                                                break;
                                            case "shutdown":
                                                Stop();
                                                using (StreamWriter sw3 = new StreamWriter($"{Environment.GetEnvironmentVariable("userprofile")}\\waitQueue.txt"))
                                                {
                                                    foreach (var item in waitQueue)
                                                    {
                                                        sw3.WriteLine(item);
                                                    }
                                                }
                                                break;
                                            default:
                                                sw.WriteLine("Commando no válido");
                                                break;
                                        }
                                    }
                                    while (command != "list" && command != "add");
                                }
                            }
                            else
                            {
                                string command;
                                do
                                {
                                    sw.Write("Introduce un comando (list | add): ");
                                    command = sr.ReadLine();
                                    switch (command)
                                    {
                                        case "list":
                                            foreach (string user in waitQueue)
                                            {
                                                sw.WriteLine(user);
                                            }
                                            break;
                                        case "add":
                                            if (!userOnList(waitQueue.ToArray(), userName))
                                            {
                                                waitQueue.Add($"{userName} - {DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss")}");
                                                sw.WriteLine("OK");
                                            }
                                            else
                                            {
                                                sw.WriteLine("Este user ya exite");
                                            }
                                            break;
                                        default:
                                            sw.WriteLine("Commando no válido");
                                            break;
                                    }
                                }
                                while (command != "list" && command != "add");
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
    }
}

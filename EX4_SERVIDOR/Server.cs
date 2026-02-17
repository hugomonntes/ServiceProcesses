using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EX4_SERVIDOR
{
    internal class Server
    {
        int Port = GetFreePort(31416);
        Socket socketServer;
        bool serverIsRunning = true;
        public void Init()
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, Port);
            using (socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socketServer.Bind(ip);
                socketServer.Listen(100);
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
            socketServer.Close();
            serverIsRunning = false;
        }

        public void RequestManager(Socket socketClient)
        {
            using (socketClient)
            {
                IPEndPoint iP = (IPEndPoint)socketClient.RemoteEndPoint;
                using (NetworkStream nw = new NetworkStream(socketClient))
                using (StreamReader sr = new StreamReader(nw, Console.OutputEncoding))
                using (StreamWriter sw = new StreamWriter(nw, Console.OutputEncoding))
                {
                    sw.WriteLine("Introduce un comando (gw | sw | gr | sr + record | close + clave): ");
                    try
                    {
                        string command = sr.ReadLine();

                    }
                    catch (IOException) { }
                }
            }
        }

        public static int GetFreePort(int defaultPort)
        {
            bool isFree = false;
            IPEndPoint iP = new IPEndPoint(IPAddress.Any, defaultPort);
            while (!isFree && defaultPort <= IPEndPoint.MaxPort)
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    try
                    {
                        socket.Bind(iP);
                        socket.Listen(1);
                        isFree = true;
                    }
                    catch (SocketException)
                    {
                        defaultPort++;
                    }
                }
            }
            return defaultPort;
        }

        public static string[] ReadFile(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    return sr.ReadToEnd().Split(",");
                }
            }
            catch (IOException)
            {
                return new string[0];
            }
        }

        public static string[] ParseToUpper(string[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].ToUpper();
            }
            return words;
        }

        public static List<Record> ReadBinaryFile(string path)
        {
            List<Record> records = new List<Record>();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            {
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    string nombre = br.ReadString();
                    int cantidadSegundos = br.ReadInt32();
                    records.Add(new Record(nombre, cantidadSegundos));
                }
            }
            return records;
        }
    }
}

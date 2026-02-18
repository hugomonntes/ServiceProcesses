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
        List<string> words;
        List<Record> records;
        string pathFile = $"{Environment.GetEnvironmentVariable("userprofile")}\\lista.txt";
        string pathFileRecords = $"{Environment.GetEnvironmentVariable("userprofile")}\\records.txt";
        object lockListas = new object();

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
            words = ParseToUpper(ReadFile(pathFile)).ToList();
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
                    sw.AutoFlush = true;
                    sw.WriteLine("Introduce un comando (gw | sw + palabra | gr | sr + record | close + clave): ");
                    try
                    {
                        string command = sr.ReadLine();
                        if (command != null)
                        {
                            string[] commandSplited = command.Split(" ");
                            switch (commandSplited[0])
                            {
                                case "gw":
                                    lock (lockListas)
                                    {
                                        sw.WriteLine(words[GetRandomNumber(words.Count)]);
                                    }
                                    break;
                                case "sw":
                                    if (commandSplited.Length == 2)
                                    {
                                        lock (lockListas)
                                        {
                                            if (SaveOnFile(commandSplited[1], pathFile))
                                            {
                                                words.Add(commandSplited[1]);
                                                sw.WriteLine("OK");
                                            }
                                            else
                                            {
                                                sw.WriteLine("ERROR");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        sw.WriteLine("ERROR");
                                    }
                                    break;
                                case "gr":
                                    lock (lockListas)
                                    {
                                        records = ReadBinaryFile(pathFileRecords);
                                        foreach (Record record in records)
                                        {
                                            sw.WriteLine($"{record.nombre} - {record.cantidadSegundos}");
                                        }
                                    }
                                    break;
                                case "sr":
                                    if (commandSplited.Length == 2)
                                    { // TDO trycatch
                                        sw.WriteLine("Introduce nombre: ");
                                        string nombre = sr.ReadLine();
                                        sw.WriteLine("Introduce el tiempo: ");
                                        int tiempo = int.Parse(sr.ReadLine());
                                        Record record = new Record(nombre, tiempo);
                                        if (WriteRecords(record, pathFileRecords))
                                        {
                                            sw.WriteLine("ACCEPT");
                                        }
                                        else
                                        {
                                            sw.WriteLine("REJECT");
                                        }
                                    }
                                    break;
                                case "close":
                                    if (commandSplited.Length == 2)
                                    {
                                        Stop();
                                    }
                                    break;
                            }
                        }
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
            try
            {
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
            }
            catch (IOException) { }
            return records;
        }

        public List<Record> AddRecord(List<Record> records, Record recordToCompare)
        {
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].cantidadSegundos > recordToCompare.cantidadSegundos)
                {
                    records[i] = recordToCompare;
                }
            }
            return records;
        }

        public bool WriteRecords(Record record, string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    AddRecord(records, record);
                    foreach (Record recorda in records)
                    {
                        bw.Write($"{recorda.nombre} {recorda.cantidadSegundos}");
                    }
                }
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        static Random random = new Random();
        public static int GetRandomNumber(int limit)
        {
            return random.Next(limit);
        }

        public bool SaveOnFile(string word, string pathFile)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(pathFile, true))
                {
                    sw.Write($",{word}");
                }
            }
            catch (IOException e)
            {
                return false;
            }
            return true;
        }
    }
}

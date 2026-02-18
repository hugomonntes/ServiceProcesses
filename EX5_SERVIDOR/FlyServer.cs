using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EX5_SERVIDOR
{
    internal class FlyServer
    {
        List<FlyRunner> clients = new List<FlyRunner>();
        int Port = 31416;
        Socket socketServer;
        bool serverIsRunning = true;
        object lockFlies = new object();

        public static void GetPort()
        {

        }

        public void InitServer()
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, Port);
            using (socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socketServer.Bind(iPEndPoint);
                socketServer.Listen(100);
                while (serverIsRunning)
                {
                    try
                    {
                        Socket client = socketServer.Accept();
                        Thread thread = new Thread(() => FlyRunnerThread(client)); // Pasar fn
                        thread.Start();
                    }
                    catch (SocketException ex) { }
                }
            }
        }

        public void StopServer()
        {
            socketServer.Close();
            serverIsRunning = false;
        }

        public void FlyRunnerThread(Socket client)
        {
            using (client)
            {
                IPEndPoint iP = (IPEndPoint)client.RemoteEndPoint;
                try
                {
                    using (NetworkStream ns = new NetworkStream(client))
                    using (StreamReader sr = new StreamReader(ns, Console.OutputEncoding))
                    using (StreamWriter sw = new StreamWriter(ns, Console.OutputEncoding))
                    {
                        FlyRunner fr = new FlyRunner(sw);
                        lock (lockFlies)
                        {
                            clients.Add(fr);
                        }
                        sw.AutoFlush = true;
                        sw.WriteLine("Fly");
                        string? command = "";
                        while (command != null)
                        {
                            sw.WriteLine("Escribe comando: ");
                            command = sr.ReadLine();
                            switch (command)
                            {
                                case "fsw":
                                    int number = GetRandomNumber(3);
                                    sw.WriteLine(number);
                                    switch (number)
                                    {
                                        case 0:
                                            lock (lockFlies)
                                            {
                                                fr.KilledFlies++;
                                                sw.WriteLine($"Killed {fr.KilledFlies} fly/flies!!");
                                            }
                                            break;
                                        case 1:
                                            lock (lockFlies)
                                            {
                                                fr.Bites++;
                                                sw.WriteLine($"You have been bitten. Number of bites: {fr.Bites}.");
                                            }
                                            break;
                                        case 2:
                                            lock (lockFlies)
                                            {
                                                bool isSame = true;

                                                while (isSame)
                                                {
                                                    int moscaAleatoria = GetRandomNumber(clients.Count);
                                                    if (clients[moscaAleatoria].Sw != fr.Sw)
                                                    {
                                                        clients[moscaAleatoria].Bites++;
                                                        sw.WriteLine("Other fly bites you!!");
                                                    }
                                                    else
                                                    {
                                                        isSame = false;
                                                    }
                                                }
                                            }
                                            break;
                                    }
                                    break;
                                case "quit":
                                    lock (lockFlies)
                                    {
                                        for (int i = 0; i < clients.Count; i++)
                                        {
                                            if (sw != clients[i].Sw)
                                            {
                                                clients[i].Sw.WriteLine($"Someone leaves with {fr.Bites} bites and {fr.KilledFlies} flies killed.");
                                            }
                                        }
                                        clients.Remove(fr);
                                    }
                                    client.Close();
                                    break;
                                default:
                                    fr.Bites += 2;
                                    break;
                            }
                        }
                    }
                }
                catch (IOException ex) { }
            }
        }

        static Random random = new Random();
        public static int GetRandomNumber(int limit)
        {
            return random.Next(limit);
        }
    }
}

namespace EX1_SERVIDOR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            //Console.WriteLine(server.ReadFile("password"));
            server.InitServer();
        }
    }
}

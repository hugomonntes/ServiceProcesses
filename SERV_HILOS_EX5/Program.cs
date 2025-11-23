namespace SERV_HILOS_EX5
{
    internal class Program
    {
        public static Random random = new Random();
        public static int getRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public static List<int> listaNumeros = new List<int>();
        public static object l = new object();
        public static bool isRunning = false;
        public static int contadorPrimos = 0;
        public static void fnProductor()
        {
            while (!isRunning)
            {
                lock (l)
                {
                    if (!isRunning)
                    {
                        int numero = getRandomNumber(1000, 10000);
                        Console.WriteLine(numero);
                        listaNumeros.Add(numero);
                        isRunning = true;
                    }

                    if (contadorPrimos >= 5)
                    {
                        isRunning = false;
                    }
                }

            }
        }

        public static void fnConsumidor()
        {
            for (int i = 0; i < listaNumeros.Count; i++)
            {
                if (esPrimo(listaNumeros[i]))
                {
                    contadorPrimos++;
                }
                listaNumeros.Remove(i);
            }
        }

        public static bool esPrimo(int numero)
        {
            for (int i = 2; i < numero; i++)
            {
                if (numero % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            Thread productor = new Thread(fnProductor);
            Thread consumidor = new Thread(fnConsumidor);

            productor.Start();
            consumidor.Start();

            productor.Join();
            consumidor.Join();
        }
    }
}

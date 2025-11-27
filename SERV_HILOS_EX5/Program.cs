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
        public static int contadorPrimos = 0;
        public static bool productorRunning = true;
        public static object semaforo = new object();
        public static void fnProductor()
        {
            while (productorRunning)
            {
                lock (semaforo)
                {
                    if (productorRunning)
                    {
                        int numero = getRandomNumber(1000, 10000);
                        Console.WriteLine(numero);
                        listaNumeros.Add(numero);
                    }
                }
            }
        }

        public static void fnConsumidor()//Continua hasta que no haya numeros, ojo con lock, colores
        {
            while (productorRunning)
            {
                while (listaNumeros.Count > 0)
                {
                    int numero = listaNumeros[0];
                    listaNumeros.RemoveAt(0);
                    if (esPrimo(numero))
                    {
                        contadorPrimos++;
                        lock (semaforo)
                        {
                            if (contadorPrimos == 5)
                            {
                                contadorPrimos = 0;
                                Console.WriteLine("He detectado 5 primos");
                                productorRunning = false;
                            }
                        }
                    }
                }
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

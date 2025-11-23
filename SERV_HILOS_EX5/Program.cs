namespace SERV_HILOS_EX5
{
    internal class Program
    {
        public static List<int> listaNumerosOriginal;
        public static Random random = new Random();
        public static int getRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public static void fnProductor(object listaNumeros)
        {
            listaNumerosOriginal = (List<int>)listaNumeros;
            int numero = getRandomNumber(1000, 10000);
            Console.WriteLine(numero);
            listaNumerosOriginal.Add(numero);
        }

        public static void fnConsumidor(object listaNumeros)
        {
            int contadorPrimos = 0;
            listaNumerosOriginal = (List<int>)listaNumeros;
            for (int i = 0; i < listaNumerosOriginal.Count; i++)
            {
                if (esPrimo(listaNumerosOriginal[i]))
                {
                    contadorPrimos++;
                }
                listaNumerosOriginal.Remove(i);
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
            Thread productor = new Thread(); // fn
            Thread consumidor = new Thread(); // fn

        }
    }
}

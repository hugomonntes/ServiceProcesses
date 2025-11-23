using System.Threading.Tasks;

namespace SERV_HILOS_EX6
{
    internal class Program
    {
        public static Random r = new Random();
        public static int getRandom(int min, int limit)
        {
            return r.Next(min, limit);
        }

        public static int searchNumber(int numberToSearch, int row, int[,] listaNumeros)
        {
            for (int i = 0; i < listaNumeros.Length; i++)
            {
                if (listaNumeros[row, i] == numberToSearch)
                {
                    return i;
                }
            }
            return -1;
        }

        static async void Main(string[] args)
        {
            List<Task<int>> listaTareas = new List<Task<int>>();
            int[,] listaNumeros = new int[10, 10000000];
            int numeroBuscar = 1123;

            for (int i = 0; i < listaNumeros.GetLength(0); i++)
            {
                for (int j = 0; j < listaNumeros.GetLength(1); j++)
                {
                    listaNumeros[i, j] = getRandom(1, (listaNumeros.Length / 2));
                }
            }

            for (int i = 0; i < listaNumeros.GetLength(0); i++)
            {
                int fila = i;
                Task<int> tarea = Task.Run(() => searchNumber(numeroBuscar, fila, listaNumeros));
                listaTareas.Add(tarea);
            }

            while (listaTareas.Count > 0)
            {
                Task<int> tareaFinalizada = await Task.WhenAny(listaTareas);
                listaTareas.Remove(tareaFinalizada);
                if (tareaFinalizada.Result != -1)
                {
                    Console.WriteLine($"Numero {numeroBuscar} encontrado en la fila {listaTareas.IndexOf(tareaFinalizada)} en la columna {tareaFinalizada.Result}");
                    break;
                }
            }
        }
    }
}

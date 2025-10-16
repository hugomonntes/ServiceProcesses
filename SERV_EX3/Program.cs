using System;
using System.Threading.Channels;

namespace SERV_EX2
{
    public class App
    {
        public delegate double Operation(double x);

        public static double requestData()
        {
            Console.Write("Introduce un número: ");
            return double.TryParse(Console.ReadLine(), out double userNumber) ? userNumber : 0.0;
        }

        public static string requestOperationType()
        {
            string option;
            do
            {
                Console.Write("Quieres el cuadrado o el cubo? (Cuadrado / Cubo): ");
                option = Console.ReadLine().Trim().ToLower();
            }
            while (option != "cuadrado" && option != "cubo");
            return option;
        }

        static void Main(string[] args)
        {
            double userNumber = requestData();
            string option = requestOperationType();
            Operation operationDelegate;
            if (option == "cuadrado")
            {
                operationDelegate = (userNumber) => userNumber * userNumber;
            }
            else
            {
                operationDelegate = (userNumber) => userNumber * userNumber * userNumber;
            }
            Console.WriteLine($"{operationDelegate(userNumber)}");
        }
    }
}

//Finalmente se ejecuta la variable con la función que se haya
//seleccionado.
//Toma como referencia el ejemplo de Operaciones de delegados pues es
//similar (si cabe, más simple).
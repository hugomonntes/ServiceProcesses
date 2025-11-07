using System.Reflection.Emit;

namespace SERV_EX5
{
    internal class Program
    {
        public delegate void MyDelegate();

        public static bool menuGenerator(String[] optionsName, MyDelegate[] functions)
        {
            int optionSelected;
            if (optionsName == null || functions == null)
            {
                Console.WriteLine("Error alguno de los arrays es nulo.");
                return false;
            }

            if (optionsName.Contains(null) || functions.Contains(null))
            {
                Console.WriteLine("Error alguno de los componentes de los arrays es nulo.");
                return false;
            }

            if (optionsName.Length != functions.Length)
            {
                Console.WriteLine("Error los arrays no tienen la misma longitud.");
                return false;
            }

            do
            {
                for (int i = 0; i < optionsName.Length; i++)
                {
                    Console.WriteLine($"{i + 1}.- {optionsName[i]}");
                }
                Console.WriteLine($"{optionsName.Length + 1}.- Salir");

                Console.Write("Elige una opción: ");
                string option = Console.ReadLine();

                if (int.TryParse(option, out optionSelected))
                {
                    if (optionSelected >= 1 && optionSelected <= optionsName.Length)
                    {
                        functions[optionSelected - 1]();
                    }
                    else if (optionSelected == optionsName.Length + 1)
                    {
                        Console.WriteLine("Saliendo...");
                    }
                    else
                    {
                        Console.WriteLine("Opción fuera de rango.");
                    }
                }
                else
                {
                    Console.WriteLine("Introduce un número.");
                }
            }
            while (optionSelected != optionsName.Length + 1);
            return true;
        }
        static void Main(string[] args)
        {
            menuGenerator(new string[] { "Op1", "Op2", "Op3", "op4" },
            new MyDelegate[] {
            () => Console.WriteLine("A"),
            () => Console.WriteLine("B"),
            () => Console.WriteLine("C"),
            () => Console.WriteLine("D"),

            });
        }
    }
}

//Al ejecutar MenuGenerator saldrá un menú clásico con las opciones indicadas
//por el primer vector y una última opción Exit (independientemente del número
//de entradas del menú). Si se introduce una opción no válida (fuera del rango)
//dará un mensaje de error y vuelve a pedirlo. Solo finaliza el menú si se
//selecciona la última opción, que será la de salir.



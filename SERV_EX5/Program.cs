using System.Reflection.Emit;

namespace SERV_EX5
{
    internal class Program
    {
        public delegate void MyDelegate();

        public static bool menuGenerator(String[] optionsName, MyDelegate[] functions)
        {
            int optionSelected;
            if (optionsName.Length == functions.Length)
            {
                do
                {
                    for (int i = 0; i < optionsName.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}.- {optionsName[i]}");
                    }
                    Console.WriteLine($"{optionsName.Length + 1} Salir");
                    Console.WriteLine("Elige una opcion: ");
                    string option = Console.ReadLine();
                    if (int.TryParse(option, out optionSelected))
                    {
                        if (optionSelected >= 1 && optionSelected <= optionsName.Length)
                        {
                            functions[optionSelected - 1]();
                        }
                        else if (optionSelected == optionsName.Length + 1)
                        {
                            Console.WriteLine("Gracias por usar el programa");
                        }
                        else
                        {
                            Console.WriteLine("Opcion no válida");
                        }
                    }
                }
                while (optionSelected != optionsName.Length + 1);
            }
            else
            {
                Console.WriteLine("Error el tamaño de las opciones es diferente al de las funciones");
            }
            return true;
        }
        static void Main(string[] args)
        {
            menuGenerator(new string[] { "Op1", "Op2", "Op3" }, 
            new MyDelegate[] { 
            () => Console.WriteLine("A"),
            () => Console.WriteLine("B"),
            () => Console.WriteLine("C")
            });
        }
    }
}

//Al ejecutar MenuGenerator saldrá un menú clásico con las opciones indicadas
//por el primer vector y una última opción Exit (independientemente del número
//de entradas del menú). Si se introduce una opción no válida (fuera del rango)
//dará un mensaje de error y vuelve a pedirlo. Solo finaliza el menú si se
//selecciona la última opción, que será la de salir.



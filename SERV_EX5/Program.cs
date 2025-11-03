namespace SERV_EX5
{
    internal class Program
    {
        public delegate void MyDelegate();

        public static void menuGenerator(String[] optionsName, MyDelegate[] delegates)
        {

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}

//Al ejecutar MenuGenerator saldrá un menú clásico con las opciones indicadas
//por el primer vector y una última opción Exit (independientemente del número
//de entradas del menú). Si se introduce una opción no válida (fuera del rango)
//dará un mensaje de error y vuelve a pedirlo. Solo finaliza el menú si se
//selecciona la última opción, que será la de salir.



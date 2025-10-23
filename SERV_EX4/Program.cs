using System.Net;
using System.Reflection.Emit;

namespace SERV_EX4
{
    internal class Program
    {   
        static void Main(string[] args)
        {
            //Crea estos dos vectores en un Main:
            int[] notas = { 5, 2, 8, 1, 9, 4 };
            string[] palabras = { "Sol", "Luna", "Estrella", "Cielo" };
            //Usa métodos de la clase Array junto con lambdas para obtener directamente
            //estos elementos del vector notas:
            //• Saber de si hay algún aprobado(Si existe o no) en notas.
            Console.WriteLine($"Hay aprobados: {notas.Any(n => n >= 5)}");
            //• Mostrar los aprobados de notas.
            int[] notasAprobadas = notas.Where(n => n >= 5).ToArray();//FindAll
            Array.ForEach(notasAprobadas, n => Console.WriteLine($"Aprobados con un {n}"));
            //• Indicar la posición en el array del último aprobado
            Console.WriteLine($"Ultimo aprobado en la posicion:  {Array.FindLastIndex(notas, n => n >= 5)}");
            //• Mostrar la nota del último aprobado.
            Console.WriteLine($"Nota ultimo aprobado {notas.Last(n => n >= 5)}");
            //• Cuanto tienen nota par.
            Console.WriteLine($"Cuantos tienen nota par {notas.Count(n => n % 2 == 0)}");
            //Y ahora haz lo siguiente con el vector palabras:
            //• Cual es la primera palabra de más de 3 caracteres.
            Console.WriteLine($"La primera palabra de más de 3 caracteres es: {palabras.First(s => s.Length > 3)}");
            //• Mostrar todas las palabras en mayúsculas.
            Array.ForEach(palabras, palabra => Console.WriteLine($"Palabra en mayus: {palabra.ToUpper()}"));
            //• Indica la posición de la primera palabra que empiece por E
            Console.WriteLine($"La primera palabra que empieza por E esta en la posicion: {Array.Find(palabras ,palabra => palabra.StartsWith("E"))}");

        }
    }
}

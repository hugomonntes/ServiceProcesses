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
            Console.WriteLine(notas.Any(n => n >= 5));
            //• Mostrar los aprobados de notas.
            int[] notasAprobadas = notas.Where(n => n >= 5).ToArray();
            Array.ForEach(notasAprobadas, n => Console.WriteLine(n));
            //• Indicar la posición en el array del último aprobado
            int ultimaPosicionAprobado = Array.FindLastIndex(notas, n => n >= 5);
            Console.WriteLine(ultimaPosicionAprobado);
            //• Mostrar la nota del último aprobado.
            Console.WriteLine(notas.Last(n => n >= 5));
            //• Cuanto tienen nota par.
            Console.WriteLine(notas.);
            //Y ahora haz lo siguiente con el vector palabras:
            //• Cual es la primera palabra de más de 3 caracteres.
            //• Mostrar todas las palabras en mayúsculas.
            //• Indica la posición de la primera palabra que empiece por E

        }
    }
}

using System.Net;
using System.Reflection.Emit;

namespace SERV_EX4
{
    internal class Program
    {   
        public static bool isAprove(int[] califications, ref int[] aproveCalifications)
        {
            int j = 0;
            for (int i = 0; i < califications.Length; i++)
            {
                if (califications[i] >= 5)
                {
                    aproveCalifications[j] = califications[i];
                    j++;
                }
            }
            return aproveCalifications.Length > 0;
        }

        public static void showAproveCalifications(int[] califications)
        {

        }

        static void Main(string[] args)
        {
            //Crea estos dos vectores en un Main:
            int[] notas = { 5, 2, 8, 1, 9, 4 };
            string[] palabras = { "Sol", "Luna", "Estrella", "Cielo" };
            //Usa métodos de la clase Array junto con lambdas para obtener directamente
            //estos elementos del vector notas:
            //• Saber de si hay algún aprobado(Si existe o no) en notas.
            //• Mostrar los aprobados de notas.
            //• Indicar la posición en el array del último aprobado
            //• Mostrar la nota del último aprobado.
            //• Cuanto tienen nota par.
            //Y ahora haz lo siguiente con el vector palabras:
            //• Cual es la primera palabra de más de 3 caracteres.
            //• Mostrar todas las palabras en mayúsculas.
            //• Indica la posición de la primera palabra que empiece por E

        }
    }
}

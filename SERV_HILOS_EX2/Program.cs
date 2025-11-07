using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SERV_HILOS_EX1
{
    internal class Program
    {
        //Realiza en consola el juego de carreras de caballos con al menos 5 caballos(haz
        //un array de hilos) pero teniendo en cuenta que ahora cada caballo es un objeto de
        //tipo Thread.El usuario hace su apuesta y luego empieza la carrera de caballos de
        //forma que cada uno se mueve una distancia aleatoria y “duerme” un tiempo
        //aleatorio.Al empezar la carrera el Main (o la función inicial) se queda en espera.

        //Una vez que un caballo llega a la meta todos paran y el main continúa indicando el
        //caballo ganador y si el usuario ha ganado. Se permitirá la repetición del juego. No
        //uses expresiones lambda en este ejercicio.

        //Visualmente deben verse en varias líneas los caballos avanzar, cada uno en la suya.
        //Puede ser tan simple como un asterisco que avance desde el principio de la línea
        //hasta la meta.

        //Nota: De cara a realizar pruebas de este juego, se recomienda quitar la
        //aleatoriedad temporalmente para forzar a que varios caballos lleguen a un tiempo y
        //ver que solo uno es el que “cruza” la meta.
        public static void initThreads(Thread[] horsesThreads)
        {
            for (int i = 0; i < horsesThreads.Length; i++)
            {
                horsesThreads[i] = new Thread(advancePosition);
                //horsesThreads[i].Start(getRandomNumber(5));
            }
        }

        public static void eachThread(Thread horse)
        {
            horse.Start(5);
        }

        public static void advancePosition(object randomNumber)
        {
            int incio = 0;
            int meta = 100;
            string horseModel = ".-.º";
            int randomParse = (int)randomNumber;
            while (incio <= meta)
            {
                incio += randomParse;
                Console.SetCursorPosition(incio, 0);
                Console.Write(horseModel);
            }
        }

        private static Random randomNumber = new();
        public static int getRandomNumber(int limit)
        {
            return randomNumber.Next(1, limit + 1);
        }

        static void Main(string[] args)
        {
            int meta = 100;
            Thread[] horsesThreads = new Thread[5];
            int j = 0;
            for (int i = 0; i < horsesThreads.Length; i++)
            {
                Console.SetCursorPosition(0,j+=15);
            }
            initThreads(horsesThreads);
            Console.ReadKey();
        }
    }
}

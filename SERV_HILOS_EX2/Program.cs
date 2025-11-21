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

        private static Random randomNumber = new();
        public static int getRandomNumber(int limit)
        {
            return randomNumber.Next(1, limit + 1);
        }

        public static int pedirNumeroCaballos()
        {
            Console.Write("Introduce numero de caballos: ");
            int.TryParse(Console.ReadLine(), out int numeroCaballos);
            return numeroCaballos;
        }

        static readonly object lockRun = new();
        static bool isRunning = true;
        static int ganador = 0;
        public static void avanzarCaballos(object y)
        {
            int x = 0;
            while (isRunning)
            {
                lock (lockRun)
                {
                    if (isRunning)
                    {
                        Console.SetCursorPosition(x += getRandomNumber(10), (int)y);
                        Console.WriteLine("*");
                        if (x >= 50)
                        {
                            isRunning = false;
                            ganador = (int)y;
                        }
                    }
                }
                Thread.Sleep(getRandomNumber(1000));
            }
        }

        static void Main(string[] args)
        {
           
        }
    }
}

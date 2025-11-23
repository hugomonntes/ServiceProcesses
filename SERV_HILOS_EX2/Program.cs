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

        public static int pedirEntero()
        {
            int num = 0;
            bool flag;
            do
            {
                Console.WriteLine("Introduce un numero entero");
                flag = int.TryParse(Console.ReadLine(), out num);
            } while (!flag);

            return num;
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
            int dinero = 1000;
            int apuesta = 0;
            int caballoElegido = 0;
            int cantidadCaballos = 0;
            int y = 0;
            Thread[] caballos;
            do
            {
                isRunning = true;
                caballoElegido = 0;
                apuesta = 0;
                Console.WriteLine($"Tu saldo es {dinero}$");
                Console.WriteLine("¿Cuantos caballos quieres? (0 salir)");
                cantidadCaballos = pedirEntero();
                caballos = new Thread[cantidadCaballos];
                Console.WriteLine($"Selecciona uno de los {cantidadCaballos} caballos ");

                while (caballoElegido < 1 || caballoElegido > cantidadCaballos)
                {
                    Console.WriteLine("Mete un numero dentro del rango de caballos");
                    caballoElegido = pedirEntero();
                }
                Console.WriteLine($"Cual es tu apuesta? (Saldo {dinero}$)");
                while (apuesta <= 0 || apuesta > dinero)
                {
                    Console.WriteLine("introduce un numero mayor que 0 y que puedas pagar!");
                    apuesta = pedirEntero();
                }
                dinero -= apuesta;

                Console.WriteLine($"Saldo tras la apuesta:{dinero}");
                Console.WriteLine("Enter para empezar la carrera");
                Console.ReadKey();
                Console.Clear();
               
                for (int i = 0; i < caballos.Length; i++)
                {
                    caballos[i] = new Thread(avanzarCaballos);
                }
                for (int i = 1; i <= caballos.Length; i++)
                {
                    caballos[i - 1].Start(y + i);
                }
                for (int i = 0; i < caballos.Length; i++)
                {
                    caballos[i].Join();
                }
                Console.Clear();
                Console.WriteLine($"Ha ganado el caballo {ganador}!");
                if (ganador == caballoElegido)
                {
                    Console.WriteLine("Has ganado!");
                    dinero += (apuesta * 2);
                    Console.WriteLine($"Has ganado {(apuesta * 2)}$");
                }
                else
                {
                    Console.WriteLine("Has perdido...");
                }
                Console.ReadKey();
                Console.Clear();
                if (dinero == 0)
                {
                    cantidadCaballos = 0;
                    Console.WriteLine("Te has quedado sin dinero para apostar...");
                    Console.WriteLine("Enter para salir...");
                    Console.ReadKey();
                }
            } while (cantidadCaballos != 0);
        }
    }
}

using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Intrinsics.X86;
using System.Threading;

namespace SERV_HILOS_EX1
{
    internal class Program
    {
        //a) Crea dos hilos(Thread) compitiendo que traten de incrementar(el primer hilo) o
        //decrementar(el segundo hilo) en 1 unidad una variable que comienza en 0.
        //Funcionarán de forma continua hasta que a llegue a 500(primer hilo) o a -500
        //(segundo hilo).Además se mostrará en pantalla la variable cada vez que cambie
        //indicando quién la ha cambiado(thread 1 o thread 2 y cada uno en un color).
        //Ambos hilos deben parar en cuanto uno consiga su objetivo. No uses
        //setCursorPosition.
        //El Main, una vez que lanza los hilos, se queda en espera hasta que ambos hilos
        //finalizan, luego informa de cual ha ganado.


        static void Main(string[] args)
        {
            object counterLock = new object();
            int counter = 0;
            bool isFinished = false;
            string winner = "";
            Thread thread1 = new Thread(() =>
            {
                while (!isFinished)
                {
                    lock (counterLock)
                    {
                        if (isFinished)
                        {
                            break;
                        }

                        if (counter >= 500)
                        {
                            isFinished = true;
                            winner = "Thread Suma";
                            break;
                        }

                        counter++;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"threadSuma {counter}");
                    }
                }
            });

            Thread thread2 = new Thread(() =>
            {
                while (!isFinished)
                {
                    lock (counterLock)
                    {
                        if (isFinished)
                        {
                            break;
                        }

                        if (counter <= -500)
                        {
                            isFinished = true;
                            winner = "Thread Resta";
                            break;
                        }

                        counter--;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"threadResta {counter}");
                    }
                }
            });
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Console.WriteLine($"Ganador: {winner}");
        }
    }
}

// Si un hilo acaba el otro aún da una vulet más.(ok)
//- No haces esta parte del enunciado:  "//El Main, una vez que lanza los hilos, se queda en espera hasta que ambos hilos finalizan, luego informa de cual ha ganado."

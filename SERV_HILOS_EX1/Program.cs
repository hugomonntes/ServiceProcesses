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
        //b) Las funciones de hilos serán expresiones lambda(si quieres y los ves claro haz
        //ya directamente este apartado).

        static void Main(string[] args)
        {
            int counter = 0;

            Thread thread1 = new Thread(() =>
            {
                while (counter > -500 && counter < 500)
                {
                    counter++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("+ " + counter);
                }
            });
            thread1.Start();

            Thread thread2 = new Thread(() =>
            {
                while (counter > -500 && counter < 500)
                {
                    counter--;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("- " + counter);
                }
            });
            thread2.Start();
            //Console.WriteLine(counter);
        }
    }
}

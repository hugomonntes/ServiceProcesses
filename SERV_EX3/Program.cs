using System;
using System.Threading.Channels;

namespace SERV_EX2
{
    public class App
    {
        public delegate double Operation(double x);

        public double requestData()
        {
            Console.Write("Introduce un número: ");
            return double.TryParse(Console.ReadLine(), out double userNumber) ? userNumber : 0.0;
        }


        static void Main(string[] args)
        {
            
        }
    }
}

//Crea un delegado que tenga parámetro double y devuelva también un
//double.
//Haz un pequeño programa que pida un número al usuario y luego le
//pregunte si desea el cuadrado o el cubo del mismo.
//Una vez que el usuario ha escogido, debe asignarse a una variable del
//tipo delegado definido previamente la función lambda que realice la
//operación de cuadrado o cubo.
//Finalmente se ejecuta la variable con la función que se haya
//seleccionado.
//Toma como referencia el ejemplo de Operaciones de delegados pues es
//similar (si cabe, más simple).
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERV_EX1
{
    internal class CommandCat
    {
        //2. Comando cat: Se le pasa un archivo de texto.Muestrea el contenido del
        //mismo en la consola.Si justo tras el comando tiene el argumento -n con un
        //número a continuación solo mostrará la cantidad de líneas indicadas por
        //dicho número (en este caso evita leer el archivo entero si no es necesario).
        //Ejemplos:
        //cat myfile.txt
        //cat -n5 c:\windows\win.ini
        public static bool checkArgs(string args)
        {
            return args.StartsWith("-n") && int.TryParse($"{args[2]}", out _);
        }

        public static void createCommandCat(string[] args) // cat[0] -n50   [1] ruta[2]
        {
            if (checkArgs(args[1]))
            {
                StreamReader stReader = new(args[2]);


                string dataFile = stReader.ReadToEnd();
                Console.WriteLine(dataFile);
            }
        }
    }
}

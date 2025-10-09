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
        public static bool checkArgs(string args, out int number)
        {
            string[] splitedArgs = args.Split("-n");
            return int.TryParse($"{splitedArgs[1]}", out number);
        }

        public static void createCommandCat(string[] args) // -n50[0] ruta[1]
        {
            string dataFile = "";
            int number;
            if (args.Length >= 1)
            {
                checkArgs(args[0], out number);
                StreamReader stReader = new(args[1]);
                for (int i = 0; i < number; i++)
                {
                    dataFile += stReader.ReadLine();
                }
                Console.WriteLine(dataFile);
            }
        }
    }
}

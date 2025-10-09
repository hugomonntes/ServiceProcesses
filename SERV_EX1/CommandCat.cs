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

        public static void createCommandCat(String fileName, string[] args) // cat[0] -n5[1] ruta[2]
        {
            if (args.Length > 0)
            {
                StreamReader stReader = new(fileName);
                //if (args[1])
                //{

                //}

                string dataFile = stReader.ReadToEnd();
                Console.WriteLine(dataFile);
            }
        }
    }
}

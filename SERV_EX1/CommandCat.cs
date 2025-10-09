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
        public static bool checkArgsModifierN(string arg, out int number)
        {
            number = 0;
            if (arg.StartsWith("-n"))
            {
                string numStr = arg.Substring(2);
                if (int.TryParse(numStr, out int numberFormated))
                {
                    number = numberFormated;
                    return true;
                }
            }
            return false;
        }

        public static bool checkFileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        public static void showFileContent(string fileName, int numLinesToShow)
        {
            if (checkFileExists(fileName))
            {
                using StreamReader reader = new(fileName);
                int contador = 0;
                string? linea;

                while ((linea = reader.ReadLine()) != null && contador < numLinesToShow)
                {
                    Console.WriteLine(linea);
                    contador++;
                }

                if (contador == 0)
                {
                    Console.WriteLine("El archivo está vacío");
                }
            }
        }

        public static void showFullFile(string fileName) // En caso de que no me pase el modificador -n
        {
            if (checkFileExists(fileName))
            {
                using StreamReader reader = new(fileName);
                string? linea;
                while ((linea = reader.ReadLine()) != null)
                {
                    Console.WriteLine(linea);
                }
            }
            else
            {
                Console.WriteLine("El archivo no existe");
            }

        }

        public static void commandCat(string[] args) // -n50[0] ruta[1]
        {
            if (args.Length > 0)
            {
                if (checkArgsModifierN(args[0], out int numLineas))
                {
                    if (checkFileExists(args[1]))
                    {
                        string fileName = args[1];
                        showFileContent(fileName, numLineas);
                    }
                    else
                    {
                        Console.WriteLine("El archivo no se encuentra");
                    }
                }
                else
                {
                    string fileName = args[0];
                    showFullFile(fileName);
                }
            }
            else
            {
                Console.WriteLine("Usa así cat -nX ruta");
            }

        }
    }
}

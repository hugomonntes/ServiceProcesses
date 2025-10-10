using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERV_EX1
{
    public class GeneralCommands
    {   
        public static bool checkArgsModifier(string arg, out int number, string modInit)
        {
            number = 0;
            if (arg.StartsWith(modInit))
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
    }
}

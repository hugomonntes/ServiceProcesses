using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERV_EX1
{
    public class CommandUtils
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

        public static void showFileContent(string fileName, int numLinesToShow)
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
                Console.WriteLine("El archivo esta vacio");
            }

        }

        public static void showFullFile(string fileName) // En caso de que no me pase el modificador -n
        {
            using StreamReader reader = new(fileName);
            reader.ReadToEnd();
        }

        public static void writeFile(string fileName, string textToAdd, bool appendMode)
        {
            using (StreamWriter writer = new StreamWriter(fileName, append: appendMode)) // Creo que tengo que controlar excep por si no se encuentra el file
            {
                writer.WriteLine(textToAdd);
                Console.WriteLine("Texto añadido");
            }
        }
    }
}

using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace SERV_EX1
{
    internal class CommandLS
    {
        //Recuerda que lo que se le pasa a continuación del comando de consola se
        //recibe como array de strings en un parámetro del Main(string[] args).
        //Puedes ver un ejemplo en el apartado Lista de Argumentos del Apéndice II del
        //tema 3: Otros aspecto de C#.
        //Los comandos serán los siguientes:

        //1.Comando ls: se le puede pasar un directorio o una variable de entorno.
        //Muestra el contenido de dicho directorio, tanto subdirectorios como archivos.
        //Diferenciará por colores y en los archivos además indicará el tamaño de los
        //mismos.

        public static void createCommandLS(string[] args) // TODO comprobar args y mover bucle
        {
            if (args != null && Directory.Exists(args[0]))
            {
                DirectoryInfo directoryToSearch = new DirectoryInfo(args[0]);
                foreach (var directory in directoryToSearch.GetDirectories())
                {
                    Console.WriteLine(directory);
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var file in directory.GetFiles())
                    {
                        Console.WriteLine(file + ", " + file.Length + "KB");
                    }
                    Console.ForegroundColor = directory is DirectoryInfo ? ConsoleColor.Green : ConsoleColor.Red;
                }
            }
            else
            {
                Console.WriteLine("No existe el directorio");
            }
        }
    }
}

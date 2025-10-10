using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERV_EX1
{
    public class CommandNF
    {
        //3. Comando newfile: Crea un archivo con el texto que haya a continuación.
        //Si justo tras el comando se pone -a, añade el texto en vez de sobreescribirlo.
        //Ejemplos:
        //newfile myfile.txt “This text goes in the myfile.”
        //newfile -a miarchivo.txt “And this one is added.”

        public static void createCommandNewFile(string[] args) // Me falta comprobar numero de args 
        {
            if (GeneralMethods.checkArgsModifier(args[0], out int number, "-a"))
            {
                if (GeneralMethods.checkFileExists(args[1]))
                {
                    GeneralMethods.writeFile(args[1], args[2], true);
                }
            } else
            {
                if (GeneralMethods.checkFileExists(args[0]))
                {
                    GeneralMethods.writeFile(args[0], args[1], false);
                }
            }
        }
    }
}

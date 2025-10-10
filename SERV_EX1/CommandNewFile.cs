using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERV_EX1
{
    public class CommandNewFile
    {
        //3. Comando newfile: Crea un archivo con el texto que haya a continuación.
        //Si justo tras el comando se pone -a, añade el texto en vez de sobreescribirlo.
        //Ejemplos:
        //newfile myfile.txt “This text goes in the myfile.”
        //newfile -a miarchivo.txt “And this one is added.”
        
        public static void crearCommandNewFile(string[] args)
        {
            if (GeneralCommands.checkArgsModifier(args[0], out int number, "-a"))
            {
                GeneralCommands.checkFileExists(args[1]);
            }
        }
    }
}

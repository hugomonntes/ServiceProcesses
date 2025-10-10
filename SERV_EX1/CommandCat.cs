namespace SERV_EX1
{
    public class CommandCat
    {
        public static void commandCat(string[] args)
        {
            if (args.Length > 0)
            {
                if (GeneralMethods.checkArgsModifier(args[0], out int numLineas, "-n"))
                {
                    if (GeneralMethods.checkFileExists(args[1]))
                    {
                        GeneralMethods.showFileContent(args[1], numLineas);
                    }
                    else
                    {
                        Console.WriteLine("El archivo no se encuentra");
                    }
                }
                else
                {
                    string fileName = args[0];
                    GeneralMethods.showFullFile(fileName);
                }
            }
            else
            {
                Console.WriteLine("Usa así: cat [-nX] ruta");
            }
        }
    }
}

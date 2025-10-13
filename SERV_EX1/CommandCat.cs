namespace SERV_EX1
{
    public class CommandCat
    {
        public static void commandCat(string[] args)
        {
            if (args.Length > 0)
            {
                if (CommandUtils.checkArgsModifier(args[0], out int numLineas, "-n"))
                {
                    if (CommandUtils.checkFileExists(args[1]))
                    {
                        CommandUtils.showFileContent(args[1], numLineas);
                    }
                    else
                    {
                        Console.WriteLine("El archivo no se encuentra");
                    }
                }
                else
                {
                    string fileName = args[0];
                    CommandUtils.showFullFile(fileName);
                }
            }
            else
            {
                Console.WriteLine("Pon este formato cat [-nNumero] ruta");
            }
        }
    }
}

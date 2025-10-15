namespace SERV_EX1
{
    public class CommandCat
    {
        public static void commandCat(string[] args)
        {
            try
            {
                if (args.Length == 2)
                {
                    if (CommandUtils.checkArgsModifier(args[0], out int numLineas, "-n"))
                    {
                        CommandUtils.showFileContent(args[1], numLineas);
                    }
                    else
                    {
                        Console.WriteLine("Pon este formato cat -nNumero ruta");
                    }
                }
                else if (args.Length == 1)
                {
                    string fileName = args[0];
                    CommandUtils.showFullFile(fileName);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error de archivo!");
            }
        }
    }
}

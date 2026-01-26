using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX3_SERVIDOR
{
    internal class ShiftServer
    {
        string[] users;
        List<string> waitQueue = new List<string>();

        public void readNames(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    users = sr.ReadToEnd().Split(";");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error en el archivo");
            }
        }

        public int readPin(string path)
        {
            int[] pin = new int[4]; 
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string content = sr.ReadToEnd().Trim(); // OJO con excepcion en caso de null (probar)
                    for (int i = 0; i < 4; i++)
                    {
                        pin[i] = content[i];
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error con el archivo");
                return -1;
            }
            return pin[0];
        }
    }
}

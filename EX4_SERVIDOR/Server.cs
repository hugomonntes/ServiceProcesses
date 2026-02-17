using System;
using System.Collections.Generic;
using System.Text;

namespace EX4_SERVIDOR
{
    internal class Server
    {
        public static string[] ReadFile(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    return sr.ReadToEnd().Split(",");
                }
            }
            catch (IOException)
            {
                return new string[0];
            }
        }

        public static string[] parseToUpper(string[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].ToUpper();
            }
            return words;
        }
    }
}

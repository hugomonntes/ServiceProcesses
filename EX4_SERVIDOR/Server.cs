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

        public static string[] ParseToUpper(string[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].ToUpper();
            }
            return words;
        }

        public static List<Record> ReadBinaryFile(string path)
        {
            List<Record> records = new List<Record>();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            {
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    string nombre = br.ReadString();
                    int cantidadSegundos = br.ReadInt32();
                    records.Add(new Record(nombre, cantidadSegundos));
                }
            }
            return records;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX2_SERVIDOR
{
    internal class Usuario
    {
        public string nombre { get; set; }
        private string ip { get; set; }
        public StreamWriter StreamWriter { get; set; }

        public Usuario(string nombre, string ip, StreamWriter streamWriter)
        {
            this.ip = ip;
            this.nombre = nombre;
            this.StreamWriter = streamWriter;
        }
    }
}

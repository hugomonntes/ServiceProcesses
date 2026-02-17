using System;
using System.Collections.Generic;
using System.Text;

namespace EX4_SERVIDOR
{
    internal class Record
    {
        public string nombre;
        private string Nombre { get; set; }

        public int cantidadSegundos;
        private int CantidadSegundos { get; set; }
        public Record() { }

        public Record(string nombre, int cantidadSegundos)
        {
            this.nombre = nombre;
            this.cantidadSegundos = cantidadSegundos;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EX5_SERVIDOR
{
    internal class FlyRunner
    {
        public StreamWriter Sw { get; set; }
        public int KilledFlies {  get; set; }
        public int Bites { get; set; }
        public FlyRunner(StreamWriter sw)
        {
            Sw = sw;
            KilledFlies = 0;
            Bites = 0;
        }
    }
}

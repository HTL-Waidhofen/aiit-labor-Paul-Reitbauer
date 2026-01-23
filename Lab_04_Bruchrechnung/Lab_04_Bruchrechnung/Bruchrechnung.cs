using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_04_Bruchrechnung
{
    internal class Bruchrechnung
    {
        Bruch b1;
        Bruch b2;
         public static Bruchrechnung Parse (string line)
        {
            string[] teile = line.Split('+', '-', '*', ':');
            Bruch b1 = Bruch.Parse(teile[0]);
            Bruch b2 = Bruch.Parse(teile[1]);
            return new Bruchrechnung(b1, b2);
        }

        public Bruch GetResult()
        {

        }
    }
}

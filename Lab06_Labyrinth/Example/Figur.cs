using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    internal class Figur
    {
        int hoehe = 16; 
        int breite = 16; 
        int x; 
        int y;

        public Figur(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Bewegen(int dx, int dy)
        {
            x += dx;
            y += dy;
        }
    }
}

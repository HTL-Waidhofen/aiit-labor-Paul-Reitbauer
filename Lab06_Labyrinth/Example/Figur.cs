using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Example
{
    internal class Figur
    {
        int hoehe = 16; 
        int breite = 16; 
        int x; 
        int y;
        Ellipse geometrie;

        public Figur(int x, int y)
        {
            this.x = x;
            this.y = y;
            geometrie = new Ellipse();
            geometrie.Width = breite;
            geometrie.Height = hoehe;
            geometrie.Fill = Brushes.Red;
            Canvas.SetLeft(geometrie, x);
            Canvas.SetTop(geometrie, y);
        }

        public void Bewegen(int dx, int dy, string[] zeilen)
        {
            int neueX = x + dx;
            int neueY = y + dy;

            // Mittelpunkt der Figur verwenden, damit Kollision mit Zellen korrekt erkannt wird
            int centerX = neueX + breite;
            int centerY = neueY + hoehe ;

            int col = centerX /20;
            int row = centerY /20;

            
            if (row <= 0 || row >= zeilen.Length) return;
            string mazeRow = zeilen[row].Replace("\r", "");
            if (col < 0 || col >= mazeRow.Length) return;

            
            if (mazeRow[col] != '#')
            {
                x = neueX;
                y = neueY;
                Canvas.SetLeft(geometrie, x);
                Canvas.SetTop(geometrie, y);
            }
        }
        public Ellipse GetEllipse()
        {
            return geometrie;
        }

    }
}

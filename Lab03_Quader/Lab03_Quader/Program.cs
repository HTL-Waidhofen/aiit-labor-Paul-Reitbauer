using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_Quader
{
    class Quader
    {
        //Maße in mm
        private double hoehe;
        private double breite;
        private double laenge; 

        public Quader() : this(1, 1, 1)
        {
        }

        public Quader (double hoehe, double breite, double laenge)
        {
            this.hoehe = hoehe;
            this.breite = breite;
            this.laenge = laenge;
        }

        public static double ParseValue (string text)
        {
            double value = 0;
            text = text.Replace(" ", ""); //entfernt alle Leerzeichen --> "2 cm" wird zu "2cm"
            if (text.EndsWith("cm"))
            {
                string valueStr = text.Replace("cm", "");
                value = Double.Parse(valueStr) * 10;
            }
            else if (text.EndsWith("mm"))
            {
                string valueStr = text.Replace("mm", "");
                value = Double.Parse(valueStr);
            }
            return value;
        }

        public static Quader Parse(string text)     //statische --> Klassenmethode
        {
            double hoehe = 0;
            double breite = 0;
            double laenge = 0;

            text = text.Replace(" ", ""); //entfernt alle Leerzeichen --> "2 cm;3cm;5mm" wird zu "2cm;3cm;5mm"
            string[] teile = text.Split(';'); //["2cm", "3cm", "5mm"]
            
            hoehe = ParseValue(teile[0]); 
            breite = ParseValue(teile[1]);
            laenge = ParseValue(teile[2]);

            return new Quader(hoehe, breite, laenge);
        } 

        public double GetVolume()
        {
            return hoehe * breite * laenge;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            // Bitte Quader eingeben: 2cm; 3cm; 5mm
            Console.Write("Bitte Quader Eingeben: ");
            string eingabe = Console.ReadLine();

            Quader q = Quader.Parse(eingabe);   //Klassenmethode

            Console.WriteLine($"Volume des Quaders: {q.GetVolume()} mm³");
            Console.ReadKey();
        }
    }
}

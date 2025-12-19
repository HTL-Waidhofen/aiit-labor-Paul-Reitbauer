using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stromstaerke
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string eingabe;
            do
            {
                Console.WriteLine("Geben sie einen Widerstandswert ein (z.B. 100;Ohm)");
                string widerstand = Console.ReadLine();
                string[] widerstand_split = widerstand.Split(';');
                double widerstand_double;
                widerstand_double = double.Parse(widerstand_split[0]);

                switch (widerstand_split[1])
                {
                    case "Ohm":
                        widerstand_double = widerstand_double * 1;
                        break;

                    case "kOhm":
                        widerstand_double = widerstand_double * 1000;
                        break;

                    case "MOhm":
                        widerstand_double = widerstand_double * 1000000;
                        break;

                    default:
                        Console.WriteLine("Überprüfen Sie ihre Eingabe");
                        break;
                }


                Console.WriteLine("Geben sie einen Spannungswert ein (z.B. 24;V");
                string spannung = Console.ReadLine();
                string[] spannung_split = spannung.Split(';');
                double spannung_double;
                spannung_double = double.Parse(spannung_split[0]);
                switch (spannung_split[1])
                {
                    case "V":
                        spannung_double = spannung_double * 1;
                        break;

                    case "kV":
                        spannung_double = spannung_double * 1000;
                        break;

                    case "mV":
                        spannung_double = spannung_double / 1000;
                        break;

                    default:
                        Console.WriteLine("Überprüfen Sie ihre Eingabe");
                        break;
                }

                double strom = spannung_double / widerstand_double;

                Console.WriteLine($"Strom = {strom} A");

                Console.WriteLine("Sie können das Programm mit q beenden");
                eingabe = Console.ReadLine();
                
            } while (eingabe != "q");

            Console.ReadKey();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_04_Bruchrechnung
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bitte bruch eingeben");
            string line1 = Console.ReadLine();

            Console.WriteLine("Bitte bruch eingeben");
            string line2 = Console.ReadLine();

            Bruch b1 = Bruch.Parse(line1);
            Bruch b2 = Bruch.Parse(line2);

            Console.WriteLine($"Wie wollen sie rechnen +, - , *, /");
            string operation = Console.ReadLine();

            switch (operation)
            {
                case "+":
                    b1.Add(b2);
                    break;
                case "-":
                    b1.Subtract(b2);
                    break;
                case "*":
                    b1.Multiply(b2);
                    break;
                case "/":
                    b1.Divide(b2);
                    break;
                default:
                    Console.WriteLine("Ungultige Eingabe");
                    break;
            }

            Console.WriteLine($"Gekurtzter Bruch = {b1}");

            Console.WriteLine();


        }
    }
}

using System;
using System.Threading.Channels;

namespace Biobiljett
{
    internal class Program
    {
        const double TAX_RATE = 0.06;          // 6% moms
        const double STUDENT_DISCOUNT = 0.15;  // 15% rabatt
        const string CURRENCY = "SEK";

        static void Main(string[] args)
        {
            // Filmer
            string[] filmer = { "Titanic", "The Lion King", "Star Wars" };

            // Visningstider
            string[] visningstider = { "15:00", "17:30", "20:00" };

            // Grundpriser
            double[] grundpriser = { 120.0, 110.0, 100.0 };

            Console.WriteLine("Välkommen till biobokningen!");
            Console.WriteLine("Välj en film nedan:");
            Console.WriteLine("-------------------------------");
            

            for (int i = 0; i < filmer.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {filmer[i]} visas kl {visningstider[i]} och kostar {grundpriser[i]} kr.");
            }

            int filmVal = -1;
            while (true)
            {
                Console.WriteLine("Välj film genom att skriva numret 1 till " + filmer.Length + ":");
                string input = Console.ReadLine();
                if (int.TryParse(input, out filmVal) && filmVal >= 1 && filmVal <= filmer.Length)
                {
                    filmVal = filmVal - 1;  // Index 0-baserat
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltigt val, försök igen.");
                }
            }

            int antalBiljetter = 0;
            while (true)
            {
                Console.WriteLine("Ange antal biljetter:");
                string input = Console.ReadLine();
                if (int.TryParse(input, out antalBiljetter) && antalBiljetter > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltigt antal, försök igen.");
                }
            }

            // Ingen fråga om studentrabatt - räkna alltid fullpris
            double totalPris = CalculatePrice(antalBiljetter, grundpriser[filmVal]);

            // Lägg på moms
            double moms = totalPris * TAX_RATE;
            double prisMedMoms = totalPris + moms;

            Console.WriteLine("\n----- Kvitto -----");
            Console.WriteLine($"Film: {filmer[filmVal]}");
            Console.WriteLine($"Tid: {visningstider[filmVal]}");
            Console.WriteLine($"Antal biljetter: {antalBiljetter}");
            Console.WriteLine($"Studentrabatt: Nej");
            Console.WriteLine($"Pris exkl. moms: {totalPris:F2} {CURRENCY}");
            Console.WriteLine($"Moms ({TAX_RATE * 100}%): {moms:F2} {CURRENCY}");
            Console.WriteLine($"Totalt att betala: {prisMedMoms:F2} {CURRENCY}");
            Console.WriteLine("-------------------");
        }

        static double CalculatePrice(int tickets, double basePrice)
        {
            return tickets * basePrice;
        }

        static double CalculatePrice(int tickets, double basePrice, double discountPercent)
        {
            double total = tickets * basePrice;
            double discountAmount = total * discountPercent;
            return total - discountAmount;
        }
    }
}

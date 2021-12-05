using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overtake_Expert_System
{
    class View
    {

        public View() { }     
  
        public static void PercentageDisplay(double percentage)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nPercentage : {percentage}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintText(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DisplayCorrest()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Correct answer");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DisplayIncorect()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("incorrect answer");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintTime(string text)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{text}");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

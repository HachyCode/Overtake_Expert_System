using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Overtake_Expert_System
{
    class Program
    {
        private static bool end = false;
        static void Main()
        {
            Task timer = new Task(() => Timer()); //New task thread for timer.
            timer.Start();

            Model model = new Model();

            end = true;
            Console.ReadKey(true);
        }
        private static void Timer()
        {
            View.PrintTime("Timer started");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (!end)
            {
                Thread.Sleep(1000);
            }
            stopWatch.Stop();

            View.PrintTime($"RunTime: {stopWatch.Elapsed}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using Mischel.ConsoleDotNet;

namespace fullscreen
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ConsoleScreenBuffer sb1 = JConsole.GetActiveScreenBuffer())
            {
                sb1.WindowWidth = 50;
                sb1.Width = sb1.WindowWidth;
                sb1.WriteLine("Window mode");
                sb1.WriteLine(String.Format("Size = {0},{1}", sb1.Width, sb1.Height));
                sb1.WriteLine(String.Format("Window = {0},{1}", sb1.WindowWidth, sb1.WindowHeight));
                Console.ReadKey();

                sb1.SetDisplayMode(ConsoleDisplayMode.Fullscreen);
                sb1.WriteLine("Full screen mode!");
                sb1.WriteLine(String.Format("Size = {0},{1}", sb1.Width, sb1.Height));
                sb1.WriteLine(String.Format("Window = {0},{1}", sb1.WindowWidth, sb1.WindowHeight));
                Console.ReadKey();
            }
        }
    }
}

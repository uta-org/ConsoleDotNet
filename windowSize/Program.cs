using System;
using System.Collections.Generic;
using System.Text;

using Mischel.ConsoleDotNet;

namespace windowSize
{
    class Program
    {
        static void Main(string[] args)
        {
            Random colors = new Random();
            using (ConsoleScreenBuffer sb1 = JConsole.GetActiveScreenBuffer())
            {
                // Fill the screen buffer
                for (int row = 0; row < sb1.Height; row++)
                {
                    StringBuilder builder = new StringBuilder(sb1.Width);
                    string text = string.Format("Row {0} ", row);
                    while (builder.Length < sb1.Width)
                    {
                        builder.Append(text);
                    }
                    sb1.WriteXY(builder.ToString(), 0, row);
                    ConsoleColor fg = (ConsoleColor)colors.Next(15);
                    ConsoleColor bg = (ConsoleColor)colors.Next(15);
                    sb1.FillAttributeXY(fg, bg, builder.Length, 0, row);
                }

                // wait for a key press
                Console.ReadKey();

                // Move a part of the buffer to the top
                sb1.MoveBufferArea(50, 200, 30, 50, 0, 0);
                Console.ReadKey();

                // Position the window to show the area that was moved
                sb1.SetWindowPosition(20, 190);
                Console.ReadKey();

                // Create a new screen buffer
                using (ConsoleScreenBuffer sb2 = new ConsoleScreenBuffer())
                {
                    sb2.WriteLine("Second screen buffer");
                    // Change buffer size and window size
                    sb2.SetBufferSize(100, 100);
                    sb2.SetWindowSize(sb2.MaximumWindowWidth, sb2.MaximumWindowHeight);

                    // set new buffer as active -- changes console window size
                    JConsole.SetActiveScreenBuffer(sb2);
                    Console.ReadKey();

                    // Show reading screen buffer...
                    sb2.WriteXY("Copied with ReadXY, ReadAttributesXY", 0, 1);
                    string chars = sb1.ReadXY(50, 0, 1);
                    sb2.WriteXY(chars, 0, 2);
                    ConsoleCharAttribute[] attrs = sb1.ReadAtrributesXY(50, 0, 1);
                    sb2.WriteAttributesXY(attrs, 50, 0, 2);

                    sb2.WriteXY("Copied with ReadBlock/WriteBlock", 0, 3);
                    ConsoleCharInfo[,] block = new ConsoleCharInfo[10, 20];
                    sb1.ReadBlock(block, 0, 0, 20, 100, 40, 110);
                    sb2.WriteBlock(block, 0, 0, 0, 4, 20, 14);

                    Console.ReadKey();
                    // reset active buffer
                    JConsole.SetActiveScreenBuffer(sb1);


                }
                Console.ReadKey();
            }
        }
    }
}

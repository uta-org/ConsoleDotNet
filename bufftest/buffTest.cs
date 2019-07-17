using System;
using System.Collections.Generic;
using System.Text;

using Mischel.ConsoleDotNet;

namespace bufftest
{
    class buffTest
    {
        static void Main(string[] args)
        {
            // get the active screen buffer and write to it.
            using (ConsoleScreenBuffer sb1 = JConsole.GetActiveScreenBuffer())
            {
                // Write some stuff...
                sb1.WriteLine("Default location is (0, 0), with gray on black as the colors.");
                sb1.ForegroundColor = ConsoleColor.Blue;
                sb1.BackgroundColor = ConsoleColor.Yellow;
                sb1.WriteLine("Next line is blue on yellow.");
                // position the cursor
                sb1.SetCursorPosition(40, 2);
                sb1.Write("Press any key...");
                // Use Console.ReadKey because we don't have ConsoleInputBuffer yet.
                Console.ReadKey();

                // Clear the screen and reset the colors
                sb1.Clear();
                sb1.WriteLine("Colors remain blue on yellow");
                sb1.ResetColor();
                sb1.WriteLine("Until we reset the colors");

                // and do a positional write
                string msg = "Press any key...";
                sb1.WriteXY(msg, 40, 2);
                sb1.FillAttributeXY(ConsoleColor.White, ConsoleColor.Red, msg.Length, 40, 2);

                // must set cursor position because WriteXY doesn't change it
                sb1.SetCursorPosition(40 + msg.Length, 2);
                Console.ReadKey();

                // Create a new screen buffer
                using (ConsoleScreenBuffer sb2 = new ConsoleScreenBuffer())
                {
                    sb2.WriteLine("Secondary screen buffer!");
                    sb2.Write("Press any key to return.");
                    JConsole.SetActiveScreenBuffer(sb2);
                    Console.ReadKey();
                    JConsole.SetActiveScreenBuffer(sb1);
                }
                sb1.WriteLine("");
                sb1.WriteLine("Back to original screen buffer.");
                sb1.Write("Press any key to exit");
                Console.ReadKey();

                // Draw a text-mode button
                for (int y = 20; y < 23; y++)
                {
                    sb1.FillCharXY(' ', 10, 20, y);
                    sb1.FillAttributeXY(ConsoleColor.Black, ConsoleColor.Red, 10, 20, y);
                }
                sb1.WriteXY(" OK ", 23, 21);
                sb1.FillAttributeXY(ConsoleColor.Yellow, ConsoleColor.Black, 4, 23, 21);

                // Define a button
                ConsoleCharInfo[,] okButton = {
                    {
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red))
                    },
                    {
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Yellow, ConsoleColor.Black)),
                        new ConsoleCharInfo('O', new ConsoleCharAttribute(ConsoleColor.Yellow, ConsoleColor.Black)),
                        new ConsoleCharInfo('K', new ConsoleCharAttribute(ConsoleColor.Yellow, ConsoleColor.Black)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Yellow, ConsoleColor.Black)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red))
                    },
                    {
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red)),
                        new ConsoleCharInfo(' ', new ConsoleCharAttribute(ConsoleColor.Black, ConsoleColor.Red))
                    }
                };

                // write the button to the screen buffer
                sb1.WriteBlock(okButton, 0, 0, 60, 20, 70, 22);

                Console.ReadKey();
            }
        }
    }
}

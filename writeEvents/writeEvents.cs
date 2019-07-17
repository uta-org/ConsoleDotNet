using System;
using System.Collections.Generic;
using System.Text;

using Mischel.ConsoleDotNet;

namespace writeEvents
{
    class writeEvents
    {
        static ConsoleScreenBuffer sb;

        static void Main(string[] args)
        {
            sb = JConsole.GetActiveScreenBuffer();
            try
            {
                sb.WriteLine("Write events...");
                EventArgs[] ea = new EventArgs[13];
                ea[0] = new ConsoleWindowBufferSizeEventArgs(80, 100);
                ea[1] = MakeKeyEvent('H', ConsoleKey.H, 35, true);
                ea[2] = MakeKeyEvent('H', ConsoleKey.H, 35, false);
                ea[3] = MakeKeyEvent('e', ConsoleKey.E, 18, true);
                ea[4] = MakeKeyEvent('e', ConsoleKey.E, 18, false);
                ea[5] = MakeKeyEvent('l', ConsoleKey.L, 38, true);
                ea[6] = MakeKeyEvent('l', ConsoleKey.L, 38, false);
                ea[7] = MakeKeyEvent('l', ConsoleKey.L, 38, true);
                ea[8] = MakeKeyEvent('l', ConsoleKey.L, 38, false);
                ea[9] = MakeKeyEvent('o', ConsoleKey.O, 24, true);
                ea[10] = MakeKeyEvent('o', ConsoleKey.O, 24, false);
                ea[11] = MakeKeyEvent(Convert.ToChar(13), ConsoleKey.Enter, 28, true);
                ea[12] = MakeKeyEvent(Convert.ToChar(13), ConsoleKey.Enter, 28, false);
                using (ConsoleInputBuffer ib = JConsole.GetInputBuffer())
                {
                    ib.WindowInput = true;
                    ib.BufferSizeChange += new ConsoleBufferSizeEventHandler(ib_BufferSizeChange);
                    ib.KeyDown += new ConsoleKeyEventHandler(ib_KeyDown);
                    ib.KeyUp += new ConsoleKeyEventHandler(ib_KeyUp);
                    ib.WriteEvents(ea);
                    ib.ProcessEvents();
//                    string s = ib.ReadLine();
//                    sb.WriteLine(String.Format("You said '{0}'", s));
                    sb.Write("Press any key to exit...");
                    ib.ReadKey();
                }
            }
            finally
            {
                sb.Dispose();
            }
        }

        static ConsoleKeyEventArgs MakeKeyEvent(char keyChar, ConsoleKey key, int scanCode, bool keyDown)
        {
            ConsoleKeyEventArgs eKey = new ConsoleKeyEventArgs();
            eKey.KeyDown = keyDown;
            eKey.RepeatCount = 1;
            eKey.KeyChar = keyChar;
            eKey.Key = key;
            eKey.VirtualScanCode = scanCode;
            return eKey;
        }

        static void ib_KeyUp(object sender, ConsoleKeyEventArgs e)
        {
            sb.WriteLine(String.Format("KeyUp: {0}, {1}", e.Key, e.VirtualScanCode));
        }

        static void ib_KeyDown(object sender, ConsoleKeyEventArgs e)
        {
            sb.WriteLine(String.Format("KeyDown: '{0}' {1}", e.KeyChar, e.Key));
        }

        static void ib_BufferSizeChange(object sender, ConsoleWindowBufferSizeEventArgs e)
        {
            sb.WriteLine(String.Format("Buffer size change ({0},{1})", e.X, e.Y));
        }
    }
}

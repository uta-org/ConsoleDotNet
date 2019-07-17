using System;
using System.Collections.Generic;
using System.Text;

using Mischel.ConsoleDotNet;

namespace readTest
{
    class readTest
    {
        static void Main(string[] args)
        {
            using (ConsoleInputBuffer ib = JConsole.GetInputBuffer())
            {
                while (true)
                {
                    ConsoleInputEventInfo[] events = ib.ReadEvents(10);
                    Console.WriteLine("{0} events", events.Length);
                    foreach (ConsoleInputEventInfo ev in events)
                    {
                        Console.WriteLine("Event type = {0}", ev.EventType);
                        switch (ev.EventType)
                        {
                            case ConsoleInputEventType.KeyEvent:
                                Console.WriteLine("Key {0}", ev.KeyEvent.KeyDown ? "down" : "up");
                                Console.WriteLine("Scan code = {0}", ev.KeyEvent.VirtualScanCode);
                                Console.WriteLine("Virtual key code = {0}", ev.KeyEvent.VirtualKeyCode);
                                Console.WriteLine("Control key state = {0}", ev.KeyEvent.ControlKeyState);
                                Console.WriteLine("Ascii char = {0}", ev.KeyEvent.AsciiChar);
                                break;
                            case ConsoleInputEventType.MouseEvent:
                                if ((ev.MouseEvent.EventFlags & (ConsoleMouseEventType)0xfffff) == 0)
                                    Console.Write("Mouse button,");
                                if ((ev.MouseEvent.EventFlags & ConsoleMouseEventType.DoubleClick) != 0)
                                    Console.Write("Double click,");
                                if ((ev.MouseEvent.EventFlags & ConsoleMouseEventType.MouseWheeled) != 0)
                                    Console.Write("Mouse wheeled,");
                                if ((ev.MouseEvent.EventFlags & ConsoleMouseEventType.MouseMoved) != 0)
                                    Console.Write("Mouse moved,");
                                if ((ev.MouseEvent.EventFlags & ConsoleMouseEventType.MouseHWheeled) != 0)
                                    Console.Write ("Mouse hWheeled,");
                                Console.WriteLine("Button state = {0}", ev.MouseEvent.ButtonState);
                                break;
                        }
                    }
                }
            }

        }
     }
}

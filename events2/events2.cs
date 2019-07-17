using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using Mischel.ConsoleDotNet;

namespace events2
{
    class events2
    {
        static private ConsoleScreenBuffer sb;
        static void Main(string[] args)
        {
            Console.Title = "testo";
            sb = JConsole.GetActiveScreenBuffer();
            try
            {
                using (ConsoleInputBuffer ib = JConsole.GetInputBuffer())
                {
                    // Enable screen buffer window size events
                    ib.WindowInput = true;

                    // show the current input mode
                    ConsoleInputModeFlags mf = ib.InputMode;
                    sb.WriteLine(string.Format("Input mode = {0}, hex: {1:X}", mf, (int)mf));
                    sb.WriteLine(string.Format("Window Input = {0}", ib.WindowInput));

                    // set up the event handlers
                    ib.KeyDown += new ConsoleKeyEventHandler(ib_KeyDown);
                    ib.KeyUp += new ConsoleKeyEventHandler(ib_KeyUp);
                    ib.MouseButton += new ConsoleMouseEventHandler(ib_MouseButton);
                    ib.MouseMove += new ConsoleMouseEventHandler(ib_MouseMove);
                    ib.MouseDoubleClick += new ConsoleMouseEventHandler(ib_MouseDoubleClick);
                    ib.MouseScroll += new ConsoleMouseEventHandler(ib_MouseScroll);
                    ib.BufferSizeChange += new ConsoleBufferSizeEventHandler(ib_BufferSizeChange);
                    ib.Focus += new ConsoleFocusEventHandler(ib_Focus);
                    ib.Menu += new ConsoleMenuEventHandler(ib_Menu);

                    // Change buffer size to test window sizing events.
                    sb.SetBufferSize(100, 300);

                    // Queue the thread
                    System.Threading.ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc));

                    // process events.  Control+C will exit the application.
                    while (true)
                    {
                        ib.ProcessEvents();
                        // Sleep at least 1 ms.  If you don't do this, your program
                        // will consume 100% of the processor time.
                        System.Threading.Thread.Sleep(1);
                    }
                }
            }
            finally
            {
                sb.Dispose();
            }
        }

        static void ThreadProc(Object stateInfo)
        {
            // Simulate a long process.
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                sb.WriteLine("tick");
            }
        }

        static void ib_MouseScroll(object sender, ConsoleMouseEventArgs e)
        {
            bool bScrollDown = (e.ButtonState & ConsoleMouseButtonState.ScrollDown) != 0;
            sb.WriteLine(string.Format("Mouse scroll: {0}, {1}", e.EventFlags, bScrollDown ? "down" : "up"));
        }

        static void ib_MouseDoubleClick(object sender, ConsoleMouseEventArgs e)
        {
            sb.WriteLine(string.Format("Double click: {0}", e.ButtonState));
        }

        static void ib_MouseMove(object sender, ConsoleMouseEventArgs e)
        {
            sb.WriteLine(string.Format("Mouse move: ({0},{1})", e.X, e.Y));
        }

        static void ib_MouseButton(object sender, ConsoleMouseEventArgs e)
        {
            sb.WriteLine(string.Format("Mouse button: {0}", e.ButtonState));
        }

        static void ib_KeyUp(object sender, ConsoleKeyEventArgs e)
        {
            sb.WriteLine(string.Format("Key Up, {0}", e.Key));
        }

        static void ib_KeyDown(object sender, ConsoleKeyEventArgs e)
        {
            sb.WriteLine(string.Format("Key Down, {0}, {1}", e.Key, Convert.ToInt32(e.KeyChar)));
        }

        static void ib_Menu(object sender, ConsoleMenuEventArgs e)
        {
            sb.WriteLine(string.Format("Menu event: {0}", e.CommandId));
        }

        static void ib_Focus(object sender, ConsoleFocusEventArgs e)
        {
            sb.WriteLine(string.Format("Focus: {0}", e.SetFocus));
        }

        static void ib_BufferSizeChange(object sender, ConsoleWindowBufferSizeEventArgs e)
        {
            sb.WriteLine(string.Format("Buffer size change: ({0},{1})", e.X, e.Y));
        }
    }
}

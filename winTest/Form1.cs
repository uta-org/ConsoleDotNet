using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Mischel.ConsoleDotNet;

namespace winTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const string consoleWindowTitle = "Shared Console";
        private bool topHalf = true;

        private void btnAttach_Click(object sender, EventArgs e)
        {
            // Get handle of console window
            IntPtr consoleWindowHandle = WinApi.FindWindow(null, consoleWindowTitle);

            if (consoleWindowHandle == IntPtr.Zero)
            {
                // window not found.  We must be the first, so create the console.
                JConsole.AllocConsole();
                Console.Title = consoleWindowTitle;
                topHalf = true;
            }
            else
            {
                // Found the console window.  Attach to it.
                // Get process ID
                int processId = 0;
                int threadId = WinApi.GetWindowThreadProcessId(consoleWindowHandle, ref processId);
                JConsole.AttachConsole(processId);
                topHalf = false;
            }
            // prevent Ctrl+C from generating a close event
            Console.TreatControlCAsInput = true;
            // handle console control events
            JConsole.ControlEvent += new ConsoleControlEventHandler(JConsole_ControlEvent);

            btnAttach.Enabled = false;
            btnDetach.Enabled = true;
            WriteMessage("Attached");
        }

        void JConsole_ControlEvent(object sender, ConsoleControlEventArgs e)
        {
            // Here you can examine e.EventType to perform different
            // actions on CtrlC, CtrlBreak, Close, Logoff, or Shutdown
            // If you want the System.Console control handler to handle
            // CtrlC and CtrlBreak, return false for those two events.
            switch (e.EventType)
            {
                case ConsoleControlEventType.CtrlC:
                case ConsoleControlEventType.CtrlBreak:
                    // If you want the System.Console control handler to handle
                    // these events, then just return with e.Cancel set to false.
                    e.Cancel = true;
                    break;
                case ConsoleControlEventType.CtrlClose:
                case ConsoleControlEventType.CtrlLogoff:
                case ConsoleControlEventType.CtrlShutdown:
                    e.Cancel = true; // prevent the app from closing
                    break;
            }
        }

        private void btnDetach_Click(object sender, EventArgs e)
        {
            WriteMessage("Detached");
            JConsole.FreeConsole();
            btnDetach.Enabled = false;
            btnAttach.Enabled = true;
        }

        private void WriteMessage(string s)
        {
            int posY;
            int topY;
            if (topHalf)
            {
                topY = 0;
                posY = Console.WindowHeight / 2;
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                topY = 1 + (Console.WindowHeight / 2);
                posY = Console.WindowHeight - 1;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            // Scroll the writing area
            Console.MoveBufferArea(0, topY + 1, Console.BufferWidth, posY - topY, 0, topY);
            // and then write
            Console.SetCursorPosition(0, posY);
            Console.Write(s);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            if (s != null && s != string.Empty)
                WriteMessage(s);
        }
    }
}
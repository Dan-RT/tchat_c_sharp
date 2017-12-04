using System;
using System.Threading;
using System.Windows.Forms;

namespace tuto_client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread client_1 = new Thread(new ThreadStart(Display_thread_start));
            Thread client_2 = new Thread(new ThreadStart(Display_thread_start_2));
            
            client_1.Start();
            client_2.Start();
        }

        public static void Display_thread_start ()
        {
            Console.WriteLine("Thread starting.");
            new Client_management("127.0.0.1");
        }

        public static void Display_thread_start_2()
        {
            Console.WriteLine("Thread starting.");
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/
            new Client_management("127.0.0.2");
        }
    }
}

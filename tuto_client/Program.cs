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
            Thread client_3 = new Thread(new ThreadStart(Display_thread_start_3));

            client_1.Start();
            client_2.Start();
            client_3.Start();
        }

        public static void Display_thread_start ()
        {
            Console.WriteLine("Thread 1 starting.");
            new Client_management("127.0.0.1");
        }

        public static void Display_thread_start_2()
        {
            Console.WriteLine("Thread 2 starting.");
            new Client_management("127.0.0.2");
        }

        public static void Display_thread_start_3()
        {
            Console.WriteLine("Thread 3 starting.");
            new Client_management("127.0.0.3");
        }
    }
}

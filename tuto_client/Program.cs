using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            Thread client_1 = new Thread(new ThreadStart(display_thread_start));
            Thread client_2 = new Thread(new ThreadStart(display_thread_start_2));
            
            client_1.Start();
            client_2.Start();
        }

        public static void display_thread_start ()
        {
            Console.WriteLine("Thread starting.");
            new Client_management("127.0.0.1");
        }

        public static void display_thread_start_2()
        {
            Console.WriteLine("Thread starting.");
            new Client_management("127.0.0.2");
        }
    }
}

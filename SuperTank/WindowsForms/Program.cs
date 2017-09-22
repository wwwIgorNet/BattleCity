using SuperTank.Audio;
using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTank.WindowsForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += Application_ThreadException;
            
            Application.Run(new GameForm());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }
    }
}

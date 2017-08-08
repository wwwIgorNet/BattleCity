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
            GameForm formRender = new GameForm();

            ServiceHost host = null;
            ThreadPool.QueueUserWorkItem((s) =>
            {
                host = new ServiceHost(formRender);
                host.CloseTimeout = TimeSpan.FromMilliseconds(0);
                host.AddServiceEndpoint(typeof(IRender), new NetTcpBinding(), "net.tcp://localhost:9090/IRender");
                host.Open();
            });

            ChannelFactory<IRender> factory = null;
            ThreadPool.QueueUserWorkItem((s) =>
            {
                factory = new ChannelFactory<IRender>(new NetTcpBinding(), "net.tcp://localhost:9090/IRender");
                IRender render = factory.CreateChannel();

                Game game = new Game(render);
                game.Start();
            });
            //game.Start();
            Application.Run(formRender);

            factory.Close();
            host.Close();
        }
    }
}

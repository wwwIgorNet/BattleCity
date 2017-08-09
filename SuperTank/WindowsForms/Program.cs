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
            SceneView sceneView = new SceneView();
            GameForm formRender = new GameForm(sceneView);

            //ServiceHost host = null;
            //host = new ServiceHost(formRender);
            //host.CloseTimeout = TimeSpan.FromMilliseconds(0);
            //host.AddServiceEndpoint(typeof(IRender), new NetTcpBinding(), "net.tcp://localhost:9090/IRender");
            //host.Open();

            //ChannelFactory<IRender> factory = null;
            //Game game = null;
            //ThreadPool.QueueUserWorkItem((s) =>
            //{
            //    factory = new ChannelFactory<IRender>(new NetTcpBinding(), "net.tcp://localhost:9090/IRender");
            //    IRender render = factory.CreateChannel();

            //    game = new Game(render);
            //    game.Start();
            //});

            ThreadPool.QueueUserWorkItem((s) =>
            {
                Game game = new Game(sceneView);
                game.Start();
            });


            Application.Run(formRender);
            
            //game.Stop();
            //host.Abort();
        }
    }
}

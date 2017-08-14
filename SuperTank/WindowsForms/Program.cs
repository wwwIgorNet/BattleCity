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
            SceneView sceneView = new SceneView();
            GameForm formRender = new GameForm(sceneView);
            SoundGame soundGame = new SoundGame();

            ServiceHost hostSound = new ServiceHost(soundGame);
            hostSound.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSound.AddServiceEndpoint(typeof(ISoundGame), new NetTcpBinding(), "net.tcp://localhost:9090/ISoundGame");
            hostSound.Open();


            ServiceHost hostSceneView = new ServiceHost(sceneView);
            hostSceneView.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSceneView.AddServiceEndpoint(typeof(IRender), new NetTcpBinding(), "net.tcp://localhost:9090/IRender");
            hostSceneView.Open();

            Game game = null;
            ThreadPool.QueueUserWorkItem((s) =>
            {
                ChannelFactory<IRender> factoryRender = new ChannelFactory<IRender>(new NetTcpBinding(), "net.tcp://localhost:9090/IRender");
                IRender render = factoryRender.CreateChannel();

                ChannelFactory<ISoundGame> factorySound  = new ChannelFactory<ISoundGame>(new NetTcpBinding(), "net.tcp://localhost:9090/ISoundGame");
                ISoundGame ISoundGame = factorySound.CreateChannel();

                game = new Game(render);
                game.Start();
            });

            //ThreadPool.QueueUserWorkItem((s) =>
            //{
            //    Game game = new Game(sceneView);
            //    game.Start();
            //});


            Application.Run(formRender);

            game.Stop();
            hostSceneView.Close();
            hostSound.Close();
        }
    }
}

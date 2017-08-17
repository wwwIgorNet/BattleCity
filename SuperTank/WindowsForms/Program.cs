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
        private static ChannelFactory<IRender> factoryRender;
        private static ChannelFactory<ISoundGame> factorySound;
        private static ServiceHost hostSound;
        private static ServiceHost hostSceneView;
        private static Game game;
        private static SoundGame soundGame;

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
            soundGame = new SoundGame();

            hostSound = new ServiceHost(soundGame);
            hostSound.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSound.AddServiceEndpoint(typeof(ISoundGame), new NetTcpBinding(), "net.tcp://localhost:9090/ISoundGame");
            hostSound.Open();


            hostSceneView = new ServiceHost(sceneView);
            hostSceneView.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSceneView.AddServiceEndpoint(typeof(IRender), new NetTcpBinding(), "net.tcp://localhost:9090/IRender");
            hostSceneView.Open();

            ThreadPool.QueueUserWorkItem((s) =>
            {
                factoryRender = new ChannelFactory<IRender>(new NetTcpBinding(), "net.tcp://localhost:9090/IRender");
                IRender render = factoryRender.CreateChannel();

                factorySound  = new ChannelFactory<ISoundGame>(new NetTcpBinding(), "net.tcp://localhost:9090/ISoundGame");
                ISoundGame sound= factorySound.CreateChannel();

                game = new Game(render, sound);
                game.Start();
            });

            formRender.FormClosing += FormRender_FormClosing;
            Application.Run(formRender);
        }

        private static bool wcfClose;
        private static void FormRender_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!wcfClose)
            {
                e.Cancel = true;
                soundGame.Dispose();
                game.Dispose();
                ThreadPool.QueueUserWorkItem(s =>
                {
                    factorySound.Close();
                    factoryRender.Close();
                    hostSound.Close();
                    hostSceneView.Close();
                    wcfClose = true;

                    ((Form)sender).Invoke((MethodInvoker)delegate ()
                    {
                        ((Form)sender).Close();
                    });
                });
            }
        }
    }
}

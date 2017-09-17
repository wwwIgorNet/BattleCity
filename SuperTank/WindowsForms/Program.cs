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
        private static ChannelFactory<IKeyboard> factoryKeyboard;
        private static ServiceHost hostSound;
        private static ServiceHost hostSceneView;
        private static ServiceHost hostGameInfo;
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
            soundGame = new SoundGame();
            GameForm formRender = new GameForm(sceneView, soundGame);
            formRender.FormClosing += FormRender_FormClosing;

            hostSound = new ServiceHost(soundGame);
            hostSound.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSound.AddServiceEndpoint(typeof(ISoundGame), new NetTcpBinding(), "net.tcp://localhost:9090/ISoundGame");
            hostSound.Open();


            hostSceneView = new ServiceHost(sceneView);
            hostSceneView.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSceneView.AddServiceEndpoint(typeof(IRender), new NetTcpBinding(), "net.tcp://localhost:9090/IRender");
            hostSceneView.Open();

            hostGameInfo = new ServiceHost(formRender);
            hostGameInfo.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostGameInfo.AddServiceEndpoint(typeof(IGameInfo), new NetTcpBinding(), "net.tcp://localhost:9090/IGameInfo");
            hostGameInfo.Open();

            ThreadPool.QueueUserWorkItem((s) =>
            {  
                game = new Game();
                game.Start();
            });

            factoryKeyboard = new ChannelFactory<IKeyboard>(new NetTcpBinding(), "net.tcp://localhost:9090/IKeyboard");
            formRender.Keyboard = factoryKeyboard.CreateChannel();
            
            Application.Run(formRender);
        }

        private static bool wcfClose;
        private static void FormRender_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!wcfClose)
            {
                e.Cancel = true;
                soundGame.Dispose();

                ThreadPool.QueueUserWorkItem(s =>
                {
                    game.Stop();
                    game.CloseFactory();

                    factoryKeyboard.Close();
                    hostSound.Close();
                    hostSceneView.Close();
                    hostGameInfo.Close();

                    game.CloseHost();

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

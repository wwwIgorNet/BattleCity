﻿using SuperTank.Audio;
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
        private static ChannelFactory<IKeyboard> factoryKeyboard;
        private static ChannelFactory<IGameInfo> factoryGameInfo;
        private static ServiceHost hostKeyboard;
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
                Keyboard keyboard = new Keyboard();
                hostKeyboard = new ServiceHost(keyboard);
                hostKeyboard.CloseTimeout = TimeSpan.FromMilliseconds(0);
                hostKeyboard.AddServiceEndpoint(typeof(IKeyboard), new NetTcpBinding(), "net.tcp://localhost:9090/IKeyboard");
                hostKeyboard.Open();

                factoryRender = new ChannelFactory<IRender>(new NetTcpBinding(), "net.tcp://localhost:9090/IRender");
                IRender render = factoryRender.CreateChannel();

                factorySound  = new ChannelFactory<ISoundGame>(new NetTcpBinding(), "net.tcp://localhost:9090/ISoundGame");
                ISoundGame sound= factorySound.CreateChannel();


                factoryGameInfo = new ChannelFactory<IGameInfo>(new NetTcpBinding(), "net.tcp://localhost:9090/IGameInfo");
                IGameInfo gameInfo = factoryGameInfo.CreateChannel();


                render.Init();
                game = new Game(render, sound, keyboard, gameInfo);
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
                game.Dispose();
                ThreadPool.QueueUserWorkItem(s =>
                {
                    factoryKeyboard.Close();
                    factorySound.Close();
                    factoryRender.Close();
                    factoryGameInfo.Close();
                    hostKeyboard.Close();
                    hostSound.Close();
                    hostSceneView.Close();
                    hostGameInfo.Close();
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

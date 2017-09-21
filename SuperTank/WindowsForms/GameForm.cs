using SuperTank.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using SuperTank.Audio;
using System.Threading;

namespace SuperTank.WindowsForms
{
    public partial class GameForm : Form
    {
        private Game game;

        private ServiceHost hostSound;
        private ServiceHost hostSceneView;
        private ServiceHost hostGameInfo;
        private GameOver gameOver = new GameOver();
        private StartScren startScren = new StartScren();

        private static ChannelFactory<IKeyboard> factoryKeyboard;

        private SoundGame soundGame;
        private SceneView sceneView;
        private ScrenGame screnGame;
        private static IViewSound viewSound;

        private bool wcfClose;
        private bool isGameStop = true;
        private bool isStartScrin = true;

        public GameForm()
        {
            InitializeComponent();

            soundGame = new SoundGame();

            GameForm.viewSound = soundGame;
            this.ClientSize = new Size(ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);
            this.FormClosing += GameForm_FormClosing;

            gameOver.Size = this.ClientSize;
            startScren.Size = this.ClientSize;

            startScren.Start();
            Controls.Add(startScren);
        }

        private void StartNewGame()
        {
            isStartScrin = false;
            sceneView = new SceneView();
            screnGame = new ScrenGame(sceneView, () =>
            {
                Controls.Remove(screnGame);
                Controls.Add(gameOver);
                gameOver.Invalidate();
                ThreadPool.QueueUserWorkItem((s) =>
                {
                    StopGame();
                    Thread.Sleep(ConfigurationView.TimeScrenGameOver);
                    Invoke(new Action(() => {
                        Controls.Remove(gameOver);
                        startScren.Start();
                        Controls.Add(startScren);
                        isStartScrin = true;
                    }));
                });
            });

            this.OpenHost();
            Controls.Remove(startScren);
            Controls.Add(screnGame);

            ThreadPool.QueueUserWorkItem((o) =>
            {
                ThreadPool.QueueUserWorkItem((s) =>
                {
                    game = new Game();
                    game.Start();
                    isGameStop = false;
                });

                this.OpenChenalFactory();
            });
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!wcfClose)
            {
                e.Cancel = true;
                soundGame.Dispose();

                ThreadPool.QueueUserWorkItem(s =>
                {
                    StopGame();

                    wcfClose = true;

                    ((Form)sender).Invoke((MethodInvoker)delegate ()
                    {
                        ((Form)sender).Close();
                    });
                });
            }
        }

        private void StopGame()
        {
            if (isGameStop) return;

            isGameStop = true;
            game.Stop();
            Thread.Sleep(300);
            game.CloseFactory();
            this.CloseChenalFactory();
            game.CloseHost();
            this.CloseHost();
        }

        public void OpenHost()
        {
            hostSound = new ServiceHost(soundGame);
            hostSound.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSound.AddServiceEndpoint(typeof(ISoundGame), new NetTcpBinding(), "net.tcp://localhost:9090/ISoundGame");
            hostSound.Open();

            hostSceneView = new ServiceHost(sceneView);
            hostSceneView.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSceneView.AddServiceEndpoint(typeof(IRender), new NetTcpBinding(), "net.tcp://localhost:9090/IRender");
            hostSceneView.Open();

            hostGameInfo = new ServiceHost(screnGame);
            hostGameInfo.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostGameInfo.AddServiceEndpoint(typeof(IGameInfo), new NetTcpBinding(), "net.tcp://localhost:9090/IGameInfo");
            hostGameInfo.Open();
        }
        public void OpenChenalFactory()
        {
            factoryKeyboard = new ChannelFactory<IKeyboard>(new NetTcpBinding(), "net.tcp://localhost:9090/IKeyboard");
            Keyboard = factoryKeyboard.CreateChannel();
        }
        public void CloseChenalFactory()
        {
            factoryKeyboard.Close();
        }
        public void CloseHost()
        {
            hostSound.Close();
            hostSceneView.Close();
            hostGameInfo.Close();
        }


        public static IViewSound Sound { get { return viewSound; } }

        public IKeyboard Keyboard { get; set; }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!isGameStop)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                    case Keys.Right:
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Space:
                    case Keys.Enter:
                    case Keys.Escape:
                        Keyboard.KeyDown(e.KeyCode);
                        break;
                }
            }
            else if (isStartScrin)
            {
                if(e.KeyCode == Keys.Enter)
                {
                    if (startScren.IndexMenu == 0)
                    {
                        StartNewGame();
                    }
                }
                else if(e.KeyCode == Keys.Up)
                {
                    startScren.MenuUp();
                }
                else if(e.KeyCode == Keys.Down)
                {
                    startScren.MenuDown();
                }
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (!isGameStop)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                    case Keys.Right:
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Space:
                    case Keys.Enter:
                    case Keys.Escape:
                        Keyboard.KeyUp(e.KeyCode);
                        break;
                }
            }
        }
    }
}

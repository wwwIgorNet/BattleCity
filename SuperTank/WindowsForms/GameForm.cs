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
using System.IO;
using System.Net;
using SuperTank.Comunication;

namespace SuperTank.WindowsForms
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public partial class GameForm : Form, ITwoComputer
    {
        private Game game;

        private ServiceHost hostSound;
        private ServiceHost hostSceneView;
        private ServiceHost hostGameInfo;
        private ServiceHost hostTwoComputer;
        private GameOver gameOver = new GameOver();
        private StartScren startScren;
        private ScrenRecord screnRecord = null;
        private ScrenConstructor screnConstructor;

        private static ChannelFactory<IKeyboard> factoryKeyboard;

        private SoundGame soundGame;
        private SceneView sceneView;
        private ScrenGame screnGame;
        private static IViewSound viewSound;

        private DialogIP dialogIP;

        private bool wcfClose;
        private bool isGameStop = true;
        private bool isStartScren = true;
        private bool constructorHasMap;

        public GameForm()
        {
            InitializeComponent();

            soundGame = new SoundGame();

            GameForm.viewSound = soundGame;
            this.ClientSize = new Size(ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);
            this.FormClosing += GameForm_FormClosing;

            gameOver.Size = this.ClientSize;
            startScren = new StartScren(this.ClientSize);

            screnConstructor = new ScrenConstructor();
            screnConstructor.Size = this.ClientSize;

            startScren.Start();
            Controls.Add(startScren);
        }

        private void StartNewGame(IPAddress ipIIPlayer)
        {
            isStartScren = false;
            sceneView = new SceneView();
            screnGame = new ScrenGame(sceneView, this.GameOver);

            this.OpenHost();
            Controls.Remove(startScren);
            Controls.Add(screnGame);

            ThreadPool.QueueUserWorkItem((o) =>
            {
                ThreadPool.QueueUserWorkItem((s) =>
                {
                    game = new Game();
                    if (constructorHasMap) game.Start(screnConstructor.GetMap());
                    else game.Start(ipIIPlayer);
                    isGameStop = false;
                });

                this.OpenChenalFactory();
            });
        }

        private void GameOver()
        {
            ThreadPool.QueueUserWorkItem((s) =>
            {
                constructorHasMap = false;
                int maxPoints = 0;
                Thread.Sleep(ConfigurationView.DelayScrenPoints);
                Invoke(new Action(() =>
                {
                    Controls.Remove(screnGame);
                    Controls.Add(gameOver);
                    gameOver.Invalidate();
                }));
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    maxPoints = int.Parse(File.ReadAllText(ConfigurationView.MaxPointsPath));
                    if (screnGame.CountPoints > maxPoints)
                    {
                        File.WriteAllText(ConfigurationView.MaxPointsPath, screnGame.CountPoints.ToString());
                    }
                });

                Thread.Sleep(ConfigurationView.TimeScrenGameOver);

                if (screnGame.CountPoints > maxPoints)
                {
                    Invoke(new Action(() =>
                    {
                        Controls.Remove(gameOver);
                        screnRecord = new ScrenRecord(screnGame.CountPoints);
                        screnRecord.Size = ClientSize;
                        viewSound.HighScore();
                        Controls.Add(screnRecord);
                        screnRecord.Start();
                        screnRecord.Invalidate();
                    }));
                    Thread.Sleep(ConfigurationView.DelayScrenRecord);
                    Invoke(new Action(() =>
                    {
                        Controls.Remove(screnRecord);
                    }));
                }

                StopGame();
                Invoke(new Action(() =>
                {
                    Controls.Remove(gameOver);
                    startScren.Start();
                    Controls.Add(startScren);
                    isStartScren = true;
                }));
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

            if (game != null)
            {
                game.Stop();
                Thread.Sleep(300);
                game.CloseFactory();
                this.CloseChenalFactory();
                game.CloseHost();
                game = null;
            }
            this.CloseHost();
        }

        public void OpenHost()
        {
            hostSound = new ServiceHost(soundGame);
            hostSound.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSound.AddServiceEndpoint(typeof(ISoundGame), new NetNamedPipeBinding(), "net.pipe://localhost/ISoundGame");
            hostSound.Open();

            hostSceneView = new ServiceHost(sceneView);
            hostSceneView.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSceneView.AddServiceEndpoint(typeof(IRender), new NetNamedPipeBinding(), "net.pipe://localhost/IRender");
            hostSceneView.Open();

            hostGameInfo = new ServiceHost(screnGame);
            hostGameInfo.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostGameInfo.AddServiceEndpoint(typeof(IGameInfo), new NetNamedPipeBinding(), "net.pipe://localhost/IGameInfo");
            hostGameInfo.Open();
        }
        public void OpenChenalFactory()
        {
            factoryKeyboard = new ChannelFactory<IKeyboard>(new NetNamedPipeBinding(), "net.pipe://localhost/IKeyboard");
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


        private void StopConstructor()
        {
            isStartScren = true;
            screnConstructor.Stop();
            Controls.Remove(screnConstructor);
            Controls.Add(startScren);
            constructorHasMap = true;
        }
        private void StartConstructor()
        {
            isStartScren = false;
            screnConstructor.Start();
            Controls.Remove(startScren);
            Controls.Add(screnConstructor);
        }

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
            else if (screnConstructor.IsActiv)
            {
                screnConstructor.KeyDown(e.KeyCode);
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
            else if (isStartScren)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (startScren.IndexMenu == 0)
                    {
                        StartNewGame(null);
                    }
                    else if (startScren.IndexMenu == 1)
                    {
                        ConfigureGameTwoPlayer();
                    }
                    else if (startScren.IndexMenu == 2)
                    {
                        StartConstructor();
                    }
                }
                else if (e.KeyCode == Keys.Up)
                {
                    startScren.MenuCursorUp();
                }
                else if (e.KeyCode == Keys.Down)
                {
                    startScren.MenuCursorDown();
                }
            }
            else if (screnConstructor.IsActiv)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    StopConstructor();
                }
                screnConstructor.KeyUp(e.KeyCode);
            }
        }

        private void ConfigureGameTwoPlayer()
        {
                dialogIP = new DialogIP();
                if (DialogResult.OK == dialogIP.ShowDialog())
                {
                if (dialogIP.NewGame)
                {
                    hostTwoComputer = new ServiceHost(this);
                    hostTwoComputer.CloseTimeout = TimeSpan.FromMilliseconds(0);
                    hostTwoComputer.AddServiceEndpoint(typeof(ITwoComputer), new NetTcpBinding(), "net.tcp://" + dialogIP.GameIP + ":9092/ITwoComputer");
                    hostTwoComputer.Open();
                }
                else if (dialogIP.JoinGame)
                {
                    JoinToGame(dialogIP);
                }
            }
        }

        private void JoinToGame(DialogIP dialogIP)
        {
            try
            {
                ChannelFactory<ITwoComputer> factoryTwoComputer = new ChannelFactory<ITwoComputer>(new NetTcpBinding(), "net.tcp://" + dialogIP.GameIP + ":9092/ITwoComputer");

                sceneView = new SceneView();
                screnGame = new ScrenGame(sceneView, this.GameOver);
                this.OpenTCPHost(dialogIP.GameIP);

                factoryTwoComputer.CreateChannel().IsStartedTwoComp();
                factoryTwoComputer.BeginClose(null, null);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                CloseHost();
                return;
            }


            isStartScren = false;
            factoryKeyboard = new ChannelFactory<IKeyboard>(new NetTcpBinding(), "net.tcp://" + dialogIP.GameIP + ":9091/IKeyboard");
            factoryKeyboard.Open(TimeSpan.FromMilliseconds(500));
            Keyboard = factoryKeyboard.CreateChannel();


            Controls.Remove(startScren);
            Controls.Add(screnGame);
            isGameStop = false;
        }

        private void OpenTCPHost(IPAddress ipIIPlayer)
        {
            string ipAddress = ipIIPlayer.ToString();
            hostSound = new ServiceHost(soundGame);
            hostSound.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSound.AddServiceEndpoint(typeof(ISoundGame), new NetTcpBinding(), "net.tcp://" + ipAddress + ":9090/ISoundGame");
            hostSound.Open();

            hostSceneView = new ServiceHost(sceneView);
            hostSceneView.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSceneView.AddServiceEndpoint(typeof(IRender), new NetTcpBinding(), "net.tcp://" + ipAddress + ":9090/IRender");
            hostSceneView.Open();

            hostGameInfo = new ServiceHost(screnGame);
            hostGameInfo.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostGameInfo.AddServiceEndpoint(typeof(IGameInfo), new NetTcpBinding(), "net.tcp://" + ipAddress + ":9090/IGameInfo");
            hostGameInfo.Open();
        }

        public void IsStartedTwoComp()
        {
            hostTwoComputer.BeginClose(null, null);
            StartNewGame(dialogIP.GameIP);
        }
    }
}

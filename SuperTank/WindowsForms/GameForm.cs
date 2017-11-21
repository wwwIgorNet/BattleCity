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
using SuperTank.FH;

namespace SuperTank.WindowsForms
{
    /// <summary>
    /// Main window
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public partial class GameForm : Form, ITwoComputer
    {
        private readonly int portChannelKeyboard = 9091;
        private readonly int portTwoComputer = 9092;
        private readonly int portHostRender = 9090;
        private readonly int portHostSound = 9090;
        private readonly int portHostInfo = 9090;

        private IPAddress ipSecondPlayer;
        private IPAddress ipFirstPlayer;
        private Game game;
        
        private ServiceHost hostTwoComputer;
        private GameOver gameOver = new GameOver();
        private StartScren startScren;
        private ScrenRecord screnRecord = null;
        private ScrenConstructor screnConstructor;
        private ScrenScore screnScore;

        private SoundGame soundGame;
        private SceneScene sceneView;
        private ScrenGame screnGame;
        private static IViewSound viewSound;

        private bool wcfClose;
        private bool isGameStop = true;
        private bool isStartScren = true;
        private bool waitForConectToGame = false;
        private bool constructorHasMap;
        private Owner ownerPlayer;

        private Action closeHost = () => { };
        private Action closeChannel = () => { };

        public GameForm()
        {
            InitializeComponent();

            soundGame = new SoundGame();

            GameForm.viewSound = soundGame;
            this.ClientSize = new Size(ConfigurationWinForms.WindowClientWidth, ConfigurationWinForms.WindowClientHeight);
            this.FormClosing += GameForm_FormClosing;

            gameOver.Size = this.ClientSize;
            startScren = new StartScren(this.ClientSize);

            screnConstructor = new ScrenConstructor();
            screnConstructor.Size = this.ClientSize;

            startScren.Start();
            Controls.Add(startScren);
        }

        public static IViewSound Sound { get { return viewSound; } }
        public IKeyboard Keyboard { get; set; }

        public void StartedTwoComp()
        {
            hostTwoComputer.BeginClose(null, null);
            StartNewGame(ipSecondPlayer);
            waitForConectToGame = false;
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

            if (waitForConectToGame) return;

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
                        ownerPlayer = SuperTank.Owner.IPlayer;
                    }
                    else if (startScren.IndexMenu == 1)
                    {
                        FirewallHelper.Test();
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

        private void StartNewGame(IPAddress ipSecondPlayer)
        {
            isStartScren = false;
            sceneView = new SceneScene();
            LevelInfo levelInfo = null;
            if (ipSecondPlayer != null)
            {
                levelInfo = new LevelInfoTwoPlayer();
                screnScore = new ScrenScoreTwoPlayers();
            }
            else
            {
                levelInfo = new LevelInfo();
                screnScore = new ScrenScore();
            }
            screnGame = new ScrenGame(sceneView, levelInfo, screnScore, this.GameOver);

            this.OpenHost();
            Controls.Remove(startScren);
            Controls.Add(screnGame);

            ThreadPool.QueueUserWorkItem((o) =>
            {
                ThreadPool.QueueUserWorkItem((s) =>
                {
                    game = new Game();
                    if (constructorHasMap) game.Start(screnConstructor.GetMap(), ipFirstPlayer, ipSecondPlayer);
                    else game.Start(ipFirstPlayer, ipSecondPlayer);
                    isGameStop = false;
                });

                this.OpenChannel();
                ownerPlayer = SuperTank.Owner.IPlayer;
            });
        }
        private void GameOver()
        {
            ThreadPool.QueueUserWorkItem((s) =>
            {
                constructorHasMap = false;
                int maxPointsInFile = 0;
                int maxPointsInGame = 0;
                Thread.Sleep(screnScore.DelayScrenPoints);
                Invoke(new Action(() =>
                {
                    Controls.Remove(screnGame);
                    Controls.Add(gameOver);
                    gameOver.Invalidate();
                }));
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    maxPointsInFile = int.Parse(File.ReadAllText(ConfigurationWinForms.MaxPointsPath));
                    if (ownerPlayer == SuperTank.Owner.IIPlayer) maxPointsInGame = screnGame.CountPointsIIPlayer;
                    else maxPointsInGame = screnGame.CountPointsIPlayer;
                    if (maxPointsInGame > maxPointsInFile)
                    {
                        File.WriteAllText(ConfigurationWinForms.MaxPointsPath, maxPointsInGame.ToString());
                    }
                });

                Thread.Sleep(ConfigurationWinForms.TimeScrenGameOver);

                if (maxPointsInGame > maxPointsInFile)
                {
                    Invoke(new Action(() =>
                    {
                        Controls.Remove(gameOver);
                        screnRecord = new ScrenRecord(maxPointsInGame);
                        screnRecord.Size = ClientSize;
                        viewSound.HighScore();
                        Controls.Add(screnRecord);
                        screnRecord.Start();
                        screnRecord.Invalidate();
                    }));
                    Thread.Sleep(ConfigurationWinForms.DelayScrenRecord);
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
        private void StopGame()
        {
            if (isGameStop) return;

            isGameStop = true;

            if (game != null)
            {
                game.Stop();
                Thread.Sleep(300);
                game.CloseChannelFactory(); 
                this.CloseChannelFactory();
                game.CloseHost();
                game = null;
            }
            this.CloseHost();
        }
        private void StartConstructor()
        {
            isStartScren = false;
            screnConstructor.Start();
            Controls.Remove(startScren);
            Controls.Add(screnConstructor);
        }
        private void StopConstructor()
        {
            isStartScren = true;
            screnConstructor.Stop();
            Controls.Remove(screnConstructor);
            Controls.Add(startScren);
            constructorHasMap = true;
        }
        private void ConfigureGameTwoPlayer()
        {
            DialogIP dialogIP = new DialogIP();
            if (DialogResult.OK == dialogIP.ShowDialog())
            {
                if (dialogIP.NewGame)
                {
                    ipSecondPlayer = dialogIP.IPSecondComputer;
                    ipFirstPlayer = dialogIP.MyIP;
                    hostTwoComputer = new ServiceHost(this);
                    hostTwoComputer.CloseTimeout = TimeSpan.FromMilliseconds(0);
                    string conStr = "net.tcp://" + dialogIP.MyIP + ":" + portTwoComputer + "/ITwoComputer";
                    hostTwoComputer.AddServiceEndpoint(typeof(ITwoComputer), new NetTcpBinding(SecurityMode.None), conStr);
                    hostTwoComputer.Open();
                    waitForConectToGame = true;
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
                string conStr = "net.tcp://" + dialogIP.IPSecondComputer + ":" + portTwoComputer + "/ITwoComputer";
                ChannelFactory<ITwoComputer> factoryTwoComputer = new ChannelFactory<ITwoComputer>(new NetTcpBinding(SecurityMode.None), conStr);

                sceneView = new SceneScene();
                screnScore = new ScrenScoreTwoPlayers();
                screnGame = new ScrenGame(sceneView, new LevelInfoTwoPlayer(), screnScore, this.GameOver);
                this.OpenHost(dialogIP.MyIP);
                factoryTwoComputer.CreateChannel().StartedTwoComp();

                factoryTwoComputer.Abort();
                ownerPlayer = SuperTank.Owner.IIPlayer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                CloseHost();
                return;
            }

            OpenTCPChannel(dialogIP.IPSecondComputer);

            Controls.Remove(startScren);
            Controls.Add(screnGame);

            isStartScren = false;
            isGameStop = false;
        }

        public void CloseChannelFactory()
        {
            try
            {
                closeChannel.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                closeChannel = () => { };
            }
        }
        public void OpenChannel()
        {
            ChannelFactory<IKeyboard>  factoryKeyboard = new ChannelFactory<IKeyboard>(new NetNamedPipeBinding(), "net.pipe://localhost/IKeyboard");
            Keyboard = factoryKeyboard.CreateChannel();
            closeChannel += () =>
            {
                factoryKeyboard.Close();
            };
        }
        public void OpenTCPChannel(IPAddress ipSecondPlayer)
        {
            string ipAddress = ipSecondPlayer.ToString();
            ChannelFactory<IKeyboard>  factoryKeyboard = new ChannelFactory<IKeyboard>(new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + portChannelKeyboard + "/IKeyboard");
            factoryKeyboard.Open(TimeSpan.FromMilliseconds(1000));
            Keyboard = factoryKeyboard.CreateChannel();
            closeChannel += () =>
            {
                factoryKeyboard.Close();
            };
        }
        public void OpenHost()
        {
            ServiceHost hostSound = new ServiceHost(soundGame);
            hostSound.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSound.AddServiceEndpoint(typeof(ISoundGame), new NetNamedPipeBinding(), "net.pipe://localhost/ISoundGame");
            hostSound.Open();

            ServiceHost hostSceneView = new ServiceHost(sceneView);
            hostSceneView.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSceneView.AddServiceEndpoint(typeof(IRender), new NetNamedPipeBinding(), "net.pipe://localhost/IRender");
            hostSceneView.Open();

            ServiceHost hostGameInfo = new ServiceHost(screnGame);
            hostGameInfo.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostGameInfo.AddServiceEndpoint(typeof(IGameInfo), new NetNamedPipeBinding(), "net.pipe://localhost/IGameInfo");
            hostGameInfo.Open();

            closeHost += () =>
            {
                hostSound.Close();
                hostSceneView.Close();
                hostGameInfo.Close();
            };
        }
        private void OpenHost(IPAddress ipSecondPlayer)
        {
            string ipAddress = ipSecondPlayer.ToString();
            ServiceHost hostSound = new ServiceHost(soundGame);
            hostSound.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSound.AddServiceEndpoint(typeof(ISoundGame), new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + portHostSound + "/ISoundGame");
            hostSound.Open();

            ServiceHost hostSceneView = new ServiceHost(sceneView);
            hostSceneView.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSceneView.AddServiceEndpoint(typeof(IRender), new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + portHostRender + "/IRender");
            hostSceneView.Open();

            ServiceHost hostGameInfo = new ServiceHost(screnGame);
            hostGameInfo.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostGameInfo.AddServiceEndpoint(typeof(IGameInfo), new NetTcpBinding(SecurityMode.None), "net.tcp://" + ipAddress + ":" + portHostInfo + "/IGameInfo");
            hostGameInfo.Open();

            closeHost += () =>
            {
                hostSound.Close();
                hostSceneView.Close();
                hostGameInfo.Close();
            };
        }
        public void CloseHost()
        {
            closeHost.Invoke();
            closeHost = () => { };
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

                    Invoke(new MethodInvoker(((Form)sender).Close));
                });
            }
        }
    }
}

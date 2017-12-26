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
using SuperTank.FH;
using GameLibrary.Lib;
using ViewLibrary.Audio;
using ViewLibrary.Service;
using GameLibrary.Service;

namespace SuperTank.WindowsForms
{
    /// <summary>
    /// Main window
    /// </summary>
    public partial class GameForm : Form, IDisposable
    {
        private IPAddress ipSecondPlayer;
        private IPAddress ipFirstPlayer;
        private Game game;

        private GameOver gameOver = new GameOver();
        private StartScren startScren;
        private ScrenRecord screnRecord = null;
        private ScrenConstructor screnConstructor;
        private ScrenScore screnScore;

        private SoundGame soundGame;
        private SceneScene sceneView;
        private ScrenGame screnGame;
        private static IViewSound viewSound;
        private float incrementVolume = ConfigurationWinForms.VolumeIncrement;
        private IGameService proxyGameService;
        
        private bool isGameStop = true;
        private bool isStartScren = true;
        private bool constructorHasMap;
        private Owner ownerPlayer;

        private Action closeChannelFactory = () => { };
        private bool waitForConectToGame = false;

        public GameForm()
        {
            InitializeComponent();

            soundGame = new SoundGame();
            soundGame.OpenMedia(ConfigurationWinForms.SoundPath);

            GameForm.viewSound = soundGame;
            this.ClientSize = new Size(ConfigurationWinForms.WindowClientWidth, ConfigurationWinForms.WindowClientHeight);
            this.FormClosing += GameForm_FormClosing;

            gameOver.Size = this.ClientSize;
            startScren = new StartScren(this.ClientSize);

            screnConstructor = new ScrenConstructor
            {
                Size = this.ClientSize
            };

            ShowStartScren();

            soundGame.Volume = ConfigurationWinForms.VolumeDefoult;
        }

        public static IViewSound Sound { get { return viewSound; } }
        public IKeyboard Keyboard { get; set; }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!isGameStop)
            {
                KeysGame keysGame = KeyConverter(e.KeyCode);
                if (keysGame != KeysGame.None)
                    Keyboard.KeyDown(keysGame);
            }
            else if (screnConstructor.IsActiv)
            {
                screnConstructor.KeyDown(e.KeyCode);
            }

            if (e.KeyCode == Keys.PageUp && soundGame.Volume <= 1 - incrementVolume)
                soundGame.Volume += incrementVolume;

            else if (e.KeyCode == Keys.PageDown && soundGame.Volume >= incrementVolume)
                soundGame.Volume -= incrementVolume;
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (waitForConectToGame) return;

            if (!isGameStop)
            {
                if (e.KeyCode == Keys.P)
                {
                    proxyGameService.PauseGame(!screnGame.IsPause);
                    return;
                }

                KeysGame keysGame = KeyConverter(e.KeyCode);
                if (keysGame != KeysGame.None)
                    Keyboard.KeyUp(keysGame);
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

        private static KeysGame KeyConverter(Keys keys)
        {
            switch (keys)
            {
                case Keys.Left:
                    return KeysGame.Left;
                case Keys.Right:
                    return KeysGame.Right;
                case Keys.Up:
                    return KeysGame.Up;
                case Keys.Down:
                    return KeysGame.Down;
                case Keys.Space:
                    return KeysGame.Space;
                case Keys.Enter:
                    return KeysGame.Enter;
                case Keys.Escape:
                    return KeysGame.Escape;
            }

            return KeysGame.None;
        }


        private async void StartNewGame(IPAddress ipAddress)
        {
            isStartScren = false;
            sceneView = new SceneScene();
            LevelInfo levelInfo = null;
            if (ipAddress != null)
            {
                levelInfo = new LevelInfoTwoPlayer();
                screnScore = new ScrenScoreTwoPlayers();
            }
            else
            {
                levelInfo = new LevelInfo();
                screnScore = new ScrenScore();
            }
            screnGame = new ScrenGame(sceneView, levelInfo, screnScore, this.GameOver, this.StartGame);

            await Task.Run(() =>
            {
                game = new Game();
                if (constructorHasMap) game.Start(screnConstructor.GetMap(), ipFirstPlayer);
                else game.Start(ipFirstPlayer);
                ownerPlayer = SuperTank.Owner.IPlayer;
            });

            InstanceContext context = new InstanceContext(new GameClient(screnGame, sceneView, soundGame));
            DuplexChannelFactory<IGameService> factory =
                new DuplexChannelFactory<IGameService>(context, new NetNamedPipeBinding(),
                    "net.pipe://localhost/GameService");
            proxyGameService = factory.CreateChannel();
            proxyGameService.Connect(SuperTank.Owner.IPlayer);

            closeChannelFactory += () => { factory.Abort(); };

            Keyboard = new KeyboardWrapper(proxyGameService, SuperTank.Owner.IPlayer, Error);
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
            FirewallHelper.Test();

            DialogIP dialogIP = new DialogIP();
            if (DialogResult.OK == dialogIP.ShowDialog())
            {
                if (dialogIP.NewGame)
                {
                    waitForConectToGame = true;
                    ipSecondPlayer = dialogIP.IPSecondComputer;
                    ipFirstPlayer = dialogIP.MyIP;
                    StartNewGame(dialogIP.MyIP);
                }
                else if (dialogIP.JoinGame)
                {
                    waitForConectToGame = true;
                    JoinToGame(dialogIP);
                }
            }
        }
        private void JoinToGame(DialogIP dialogIP)
        {
            try
            {
                sceneView = new SceneScene();
                screnScore = new ScrenScoreTwoPlayers();
                screnGame = new ScrenGame(sceneView, new LevelInfoTwoPlayer(), screnScore, this.GameOver, StartGame);

                InstanceContext context = new InstanceContext(new GameClient(screnGame, sceneView, soundGame));
                DuplexChannelFactory<IGameService> factory =
                    new DuplexChannelFactory<IGameService>(context, new NetTcpBinding(),
                        "net.tcp://" + dialogIP.IPSecondComputer + ":" + ConfigurationWinForms.ServisePort + "/GameService");
                proxyGameService = factory.CreateChannel();
                proxyGameService.Connect(SuperTank.Owner.IIPlayer);

                closeChannelFactory += () => { factory.Abort(); };

                Keyboard = new KeyboardWrapper(proxyGameService, SuperTank.Owner.IIPlayer, Error);
                ownerPlayer = SuperTank.Owner.IIPlayer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                СloseChannelFactory();
                waitForConectToGame = false;
                return;
            }

            isStartScren = false;
        }

        private void StartGame()
        {
            Invoke(new Action(() =>
            {
                isGameStop = false;
                waitForConectToGame = false;
                ShowGameScren();
            }));
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
                    ShowStartScren();
                    isStartScren = true;
                }));
            });
        }
        private async void StopGame()
        {
            ipFirstPlayer = null;
            ipSecondPlayer = null;
            isGameStop = true;
            game?.Stop();
            await Task.Delay(500);
            game?.CloseHost();
            СloseChannelFactory();
        }
        private void СloseChannelFactory()
        {
            closeChannelFactory.Invoke();
            closeChannelFactory += () => { };
        }
        public new void Dispose()
        {
            soundGame.Dispose();
            StopGame();
            base.Dispose();
        }
        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void Error(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            StopGame();

            ShowStartScren();
        }

        private void ShowStartScren()
        {
            this.Controls.Clear();
            this.Controls.Add(startScren);
            startScren.Start();
        }
        private void ShowGameScren()
        {
            this.Controls.Clear();
            this.Controls.Add(screnGame);
        }
    }
}

using SuperTank;
using SuperTank.Audio;
using SuperTank.FH;
using SuperTank.View;
using SuperTankWPF.Model;
using SuperTankWPF.View;
using SuperTankWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SuperTankWPF.Util
{
    class GameMenedger : IDisposable
    {
        private ScreenGameViewModel screenGame;
        private LevelInfoViewModel levelInfo;
        private ScreenSceneViewModel screenScene;
        private MainViewModel mainViewModel;
        private ScreenConstructionViewModel construction;
        private ComunicationTCP comunication;
        private ScreenScoreViewModel screenScore;
        private ScreenLockViewModel screenLock;
        private IViewSound sound;
        private GameInfo gameInfo;
        private DialogIPViewModel dialogIPViewModel;
        private Game game;

        public GameMenedger(ScreenGameViewModel screenGame, MainViewModel mainViewModel, 
            ScreenSceneViewModel screenScene, LevelInfoViewModel levelInfo, 
            ScreenConstructionViewModel construction, ComunicationTCP comunication,
            ScreenScoreViewModel screenScore, ScreenLockViewModel screenLock, IViewSound sound,
            GameInfo iPlayerGameManedger, DialogIPViewModel dialogIPViewModel)
        {
            this.screenGame = screenGame;
            this.mainViewModel = mainViewModel;
            this.screenScene = screenScene;
            this.levelInfo = levelInfo;
            this.construction = construction;
            this.comunication = comunication;
            this.screenScore = screenScore;
            this.screenLock = screenLock;
            this.sound = sound;
            this.gameInfo = iPlayerGameManedger;
            this.dialogIPViewModel = dialogIPViewModel;
        }

        public void IPlayerExecute()
        {
            StartGame(null, null);
            levelInfo.IsTwoPlayer = false;
            screenScore.IsTwoPlayer = false;
        }

        private async void StartGame(IPAddress iPCurrentComputer, IPAddress iPRemoteComputer)
        {
            mainViewModel.ScreenGameVisibility = Visibility.Visible;
            await Task.Run(() =>
            {
                comunication.OpenHost();
                gameInfo.OwnerPlayer = Owner.IPlayer;
                gameInfo.EndOfGame += StopoGame;
            });

            await Task.Run(() =>
            {
                game = new Game();
                if (construction.HasMap)
                    game.Start(construction.GetAndClerMap(), iPCurrentComputer, iPRemoteComputer);
                else
                    game.Start(iPCurrentComputer, iPRemoteComputer);
            });

            ThreadPool.QueueUserWorkItem(s => screenGame.Keyboard = comunication.GetKeyboard());
        }

        public void IIPlayerExecute()
        {
            FirewallHelper.Test();
            dialogIPViewModel.Init();
            DialogIP dialogIP = new DialogIP
            {
                Owner = Application.Current.MainWindow
            };

            if (dialogIP.ShowDialog() == true)
            {
                if (dialogIPViewModel.NewGame)
                {
                    comunication.StartMainComputer(dialogIPViewModel.IPCurrentComputer);
                    screenLock.IsCancelVisible = Visibility.Visible;
                    screenLock.Cancel += () =>
                    {
                        StopoGame(); mainViewModel.ScreenStartVisibility = Visibility.Visible;
                    };
                    mainViewModel.ScreenLockVisibility = Visibility.Visible;
                    comunication.StartedTwoComputer += () =>
                    {
                        StartGame(dialogIPViewModel.IPCurrentComputer, dialogIPViewModel.IPRemoteComputer);
                    };
                }
                else if (dialogIPViewModel.JoinGame)
                {
                    ThreadPool.QueueUserWorkItem(s =>
                    {
                        mainViewModel.ScreenLockVisibility = Visibility.Visible;
                        comunication.StartTwoPlayerComputer(dialogIPViewModel.IPRemoteComputer);
                        comunication.OpenTCPHost(dialogIPViewModel.IPCurrentComputer);
                        screenGame.Keyboard = comunication.GetTCPKeyboard(dialogIPViewModel.IPRemoteComputer);
                        gameInfo.OwnerPlayer = Owner.IIPlayer;
                        gameInfo.EndOfGame += StopoGame;
                        mainViewModel.ScreenGameVisibility = Visibility.Visible;
                    });
                }

                levelInfo.IsTwoPlayer = true;
                screenScore.IsTwoPlayer = true;
            }
        }
        public void ConstructionExecute()
        {
            mainViewModel.ScreenConstructionVisibility = Visibility.Visible;
        }

        public void StopoGame()
        {
            gameInfo.EndOfGame -= StopoGame;
            screenGame.Keyboard = null;
            game?.Stop();
            Thread.Sleep(ConfigurationWPF.TimerInterval * 4);
            comunication?.CloseChannelFactory();
            game?.CloseHost();
            game?.CloseChannelFactory();
            comunication?.CloseHost();

            dialogIPViewModel.Clerar();
            screenGame.Clear();
            screenScene.Clear();
            levelInfo.Cler();
            screenScore.Clear();
            screenLock.Clear();
        }
        public void Dispose()
        {
            StopoGame();
        }
    }
}

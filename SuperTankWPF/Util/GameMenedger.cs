using Microsoft.Practices.ServiceLocation;
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
        private ScrenGameViewModel screnGame;
        private LevelInfoViewModel levelInfo;
        private ScrenSceneViewModel screnScene;
        private MainViewModel mainViewModel;
        private ScrenConstructionViewModel construction;
        private ComunicationTCP comunication;
        private ScrenScoreViewModel screnScore;
        private IViewSound sound;
        private GameInfo iPlayerGameManedger;
        private DialogIPViewModel dialogIPViewModel;
        private Game game;

        public GameMenedger(ScrenGameViewModel screnGame, MainViewModel mainViewModel, 
            ScrenSceneViewModel screnScene, LevelInfoViewModel levelInfo, 
            ScrenConstructionViewModel construction, ComunicationTCP comunication,
            ScrenScoreViewModel screnScore, IViewSound sound,
            GameInfo iPlayerGameManedger, DialogIPViewModel dialogIPViewModel)
        {
            this.screnGame = screnGame;
            this.mainViewModel = mainViewModel;
            this.screnScene = screnScene;
            this.levelInfo = levelInfo;
            this.construction = construction;
            this.comunication = comunication;
            this.screnScore = screnScore;
            this.sound = sound;
            this.iPlayerGameManedger = iPlayerGameManedger;
            this.dialogIPViewModel = dialogIPViewModel;
        }

        public void IPlayerExecute()
        {
            StartGame(null, null);
            levelInfo.IsTwoPlayer = false;
            screnScore.IsTwoPlayer = false;
        }

        private async void StartGame(IPAddress iPCurrentComputer, IPAddress iPRemoteComputer)
        {
            mainViewModel.ScrenGameVisibility = Visibility.Visible;
            await Task.Run(() =>
            {
                comunication.OpenHost();
                iPlayerGameManedger.EndOfGame += StopoGame;
            });

            await Task.Run(() =>
            {
                game = new Game();
                if (construction.HasMap)
                    game.Start(construction.GetAndClerMap(), iPCurrentComputer, iPRemoteComputer);
                else
                    game.Start(iPCurrentComputer, iPRemoteComputer);
            });

            ThreadPool.QueueUserWorkItem(s => screnGame.Keyboard = comunication.GetKeyboard());
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

                    comunication.StartedTwoComputer += () =>
                    {
                        StartGame(dialogIPViewModel.IPCurrentComputer, dialogIPViewModel.IPRemoteComputer);
                    };
                }
                else if (dialogIPViewModel.JoinGame)
                {
                    ThreadPool.QueueUserWorkItem(s =>
                    {
                        comunication.StartTwoPlayerComputer(dialogIPViewModel.IPRemoteComputer);
                        comunication.OpenTCPHost(dialogIPViewModel.IPCurrentComputer);
                        screnGame.Keyboard = comunication.GetTCPKeyboard(dialogIPViewModel.IPRemoteComputer);
                        iPlayerGameManedger.EndOfGame += StopoGame;
                        mainViewModel.ScrenGameVisibility = Visibility.Visible;
                    });
                }

                levelInfo.IsTwoPlayer = true;
                screnScore.IsTwoPlayer = true;
            }
        }
        public void ConstructionExecute()
        {
            mainViewModel.ScrenConstructionVisibility = Visibility.Visible;
        }

        public void StopoGame()
        {
            iPlayerGameManedger.EndOfGame -= StopoGame;
            screnGame.Keyboard = null;
            game?.Stop();
            Thread.Sleep(ConfigurationWPF.TimerInterval * 4);
            comunication?.CloseChannelFactory();
            game?.CloseHost();
            game?.CloseChannelFactory();
            comunication?.CloseHost();

            dialogIPViewModel.Clerar();
            screnGame.Clear();
            screnScene.Clear();
            levelInfo.Cler();
            screnScore.Clear();
        }
        public void Dispose()
        {
            StopoGame();
        }
    }
}

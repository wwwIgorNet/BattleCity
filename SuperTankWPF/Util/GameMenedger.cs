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
        private Comunication comunication;
        private IViewSound sound;
        private IPlayerGameManedger iPlayerGameManedger;
        private DialogIPViewModel dialogIPViewModel;
        private Game game;

        public GameMenedger(ScrenGameViewModel screnGame, MainViewModel mainViewModel, 
            ScrenSceneViewModel screnScene, LevelInfoViewModel levelInfo, 
            ScrenConstructionViewModel construction, Comunication comunication, 
            IViewSound sound, IPlayerGameManedger iPlayerGameManedger,
            DialogIPViewModel dialogIPViewModel)
        {
            this.screnGame = screnGame;
            this.mainViewModel = mainViewModel;
            this.screnScene = screnScene;
            this.levelInfo = levelInfo;
            this.construction = construction;
            this.comunication = comunication;
            this.sound = sound;
            this.iPlayerGameManedger = iPlayerGameManedger;
            this.dialogIPViewModel = dialogIPViewModel;
        }

        public async void IPlayerExecute()
        {
            mainViewModel.ScrenGameVisibility = Visibility.Visible;
            await Task.Run(() =>
            {
                screnGame.Clear();
                comunication.OpenHost();
                screnScene.Clear();
                levelInfo.Cler();
                iPlayerGameManedger.EndOfGame += StpoGame;
            });

            await Task.Run(() =>
            {
                game = new Game();
                if (construction.HasMap)
                {
                    game.Start(construction.GetAndClerMap(), null, null);
                }
                else
                    game.Start(null, null);
            });

            ThreadPool.QueueUserWorkItem(s => screnGame.Keyboard = comunication.OpenChannel());
        }
        public void IIPlayerExecute()
        {
            FirewallHelper.Test();
            dialogIPViewModel.Init();
            DialogIP dialogIP = new DialogIP
            {
                Owner = Application.Current.Windows[0]
            };

            if (dialogIP.ShowDialog() == true)
            {
                if (null == dialogIPViewModel.IPRemoteComputer) return;

                Console.WriteLine(dialogIPViewModel.IPCurrentComputer);
                Console.WriteLine(dialogIPViewModel.IPRemoteComputer);
            }
        }
        public void ConstructionExecute()
        {
            mainViewModel.ScrenConstructionVisibility = Visibility.Visible;
        }

        public void Dispose()
        {
            StpoGame();
        }

        public void StpoGame()
        {
            iPlayerGameManedger.EndOfGame -= StpoGame;
            screnGame.Keyboard = null;
            game?.Stop();
            Thread.Sleep(ConfigurationWPF.TimerInterval * 4);
            comunication?.CloseChannelFactory();
            comunication?.CloseChannelFactory();
            game?.CloseHost();
            game?.CloseChannelFactory();
            comunication?.CloseHost();
        }
    }
}

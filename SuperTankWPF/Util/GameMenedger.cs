using Microsoft.Practices.ServiceLocation;
using SuperTank;
using SuperTank.Audio;
using SuperTank.View;
using SuperTankWPF.Model;
using SuperTankWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SuperTankWPF.Util
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class GameMenedger : IGameInfo
    {
        private ScrenGameViewModel screnGame = ServiceLocator.Current.GetInstance<ScrenGameViewModel>();
        private ScrenSceneViewModel screnScene = ServiceLocator.Current.GetInstance<ScrenSceneViewModel>();
        private LevelInfoViewModel levelInfo = ServiceLocator.Current.GetInstance<LevelInfoViewModel>();
        private ScrenScoreViewModel screnScore = ServiceLocator.Current.GetInstance<ScrenScoreViewModel>();
        private MainViewModel mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
        private Comunication comunication = ServiceLocator.Current.GetInstance<Comunication>();
        private Game game;

        public GameMenedger()
        {
            comunication.SynchronizationContext = SynchronizationContext.Current;
            mainViewModel.StartScrenVisibility = Visibility.Visible;
        }

        public async void IPlayerExecute()
        {
            mainViewModel.ScrenGameVisibility = Visibility.Visible;

            ThreadPool.QueueUserWorkItem(s =>
            {
                screnGame.Clear();
                screnGame.IsShowAnimationNewLevel = true;

                screnScene.Clear();
                levelInfo.Cler();
                comunication.OpenHost();
            });

            await Task.Run(() =>
            {
                game = new Game();
                game.Start(null, null);
            });

            ThreadPool.QueueUserWorkItem((s) => screnGame.Keyboard = comunication.OpenChannel());
        }
        public void IIPlayerExecute()
        {
            MessageBox.Show("Command II PLAYERS");
        }
        public void ConstructionExecute()
        {
            MessageBox.Show("Command CONSTRUCTION");
        }

        public async void EndLevel(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr,
                                        int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr)
        {
            mainViewModel.ScrenScoreVisibility = Visibility.Visible;
            screnScore.Set(level, countPointsIPlayer, destrouTanksIPlaeyr, countPointsIIPlayer, destrouTanksIIPlaeyr);

            await Task.Delay(ConfigurationWPF.GetDelayScrenPoints(destrouTanksIPlaeyr.Values.Sum()));

            if (!screnGame.IsShowGameOver)
            {
                mainViewModel.ScrenGameVisibility = Visibility.Visible;
                screnGame.IsShowAnimationNewLevel = true;
            }
            else
            {
                mainViewModel.ScrenGameOverVisibility = Visibility.Visible;
                comunication.CloseChannelFactory();
                game.CloseHost();
                game.CloseChannelFactory();
                comunication.CloseHost();
                screnGame.Keyboard = null;
                await Task.Delay(ConfigurationGame.TimeGameOver);
                mainViewModel.StartScrenVisibility = Visibility.Visible;
            }
        }

        public async void StartLevel(int level)
        {
            screnGame.Level = level;
            await Task.Delay((int)ConfigurationWPF.DelayScrenLoadLevel.TotalMilliseconds /2);
            levelInfo.Level = level;
        }

        public void GameOver()
        {
            screnGame.IsShowGameOver = true;
        }

        public void SetCountTankEnemy(int count)
        {
            levelInfo.CountTankEnemy = count;
        }

        public async void SetCountTankPlaeyr(int count, Owner owner)
        {
            if(screnGame.IsShowAnimationNewLevel)
                await Task.Delay((int)ConfigurationWPF.DelayScrenLoadLevel.TotalMilliseconds / 2);

            if (owner == Owner.IPlayer) levelInfo.CountTank1Player = count;
            else if (owner == Owner.IIPlayer) levelInfo.CountTank2Player = count;
        }
    }
}

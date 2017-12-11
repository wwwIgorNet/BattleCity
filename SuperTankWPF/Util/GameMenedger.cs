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
        private SynchronizationContext synchronizationContext = SynchronizationContext.Current;
        private MainViewModel mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();

        private System.Threading.AutoResetEvent autoResetEvent = new System.Threading.AutoResetEvent(false);

        public GameMenedger()
        {
            mainViewModel.StartScrenVisibility = Visibility.Visible;
        }

        public async void IPlayerExecute()
        {
            mainViewModel.ScrenGameVisibility = Visibility.Visible;

            await Task.Run(() =>
            {
                screnGame.IsShowAnimationNewLevel = true;
                this.OpenHost();
            });

            await Task.Run(() =>
            {
                Game game = new Game();
                game.Start(null, null);
            });

            ThreadPool.QueueUserWorkItem((s) => OpenChannel());
        }
        public void IIPlayerExecute()
        {
            MessageBox.Show("Command II PLAYERS");
        }
        public void ConstructionExecute()
        {
            MessageBox.Show("Command CONSTRUCTION");
        }

        public void OpenHost()
        {
            synchronizationContext.Post((s) =>
            {
                ServiceHost hostSound = new ServiceHost(ServiceLocator.Current.GetInstance<ISoundGame>());
                hostSound.CloseTimeout = TimeSpan.FromMilliseconds(0);
                hostSound.AddServiceEndpoint(typeof(ISoundGame), new NetNamedPipeBinding(), "net.pipe://localhost/ISoundGame");
                hostSound.Open();

                ServiceHost hostSceneView = new ServiceHost(ServiceLocator.Current.GetInstance<ScrenSceneViewModel>());
                hostSceneView.CloseTimeout = TimeSpan.FromMilliseconds(0);
                hostSceneView.AddServiceEndpoint(typeof(IRender), new NetNamedPipeBinding(), "net.pipe://localhost/IRender");
                hostSceneView.Open();
            }, null);

            ServiceHost hostGameInfo = new ServiceHost(ServiceLocator.Current.GetInstance<GameMenedger>());
            hostGameInfo.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostGameInfo.AddServiceEndpoint(typeof(IGameInfo), new NetNamedPipeBinding(), "net.pipe://localhost/IGameInfo");
            hostGameInfo.Open();
        }
        public void OpenChannel()
        {
            ChannelFactory<IKeyboard> factoryKeyboard = new ChannelFactory<IKeyboard>(new NetNamedPipeBinding(), "net.pipe://localhost/IKeyboard");
            screnGame.Keyboard = factoryKeyboard.CreateChannel();
        }

        public async void EndLevel(int level, int countPointsIPlayer,  Dictionary<TypeUnit, int> destrouTanksIPlaeyr,
                                        int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr)
        {
            mainViewModel.ScrenScoreVisibility = Visibility.Visible;
            screnScore.Set(level, countPointsIPlayer, destrouTanksIPlaeyr, countPointsIIPlayer, destrouTanksIIPlaeyr);

           await Task.Delay(ConfigurationWPF.GetDelayScrenPoints(destrouTanksIPlaeyr.Values.Sum()));

            mainViewModel.ScrenGameVisibility = Visibility.Visible;
            screnGame.IsShowAnimationNewLevel = true;

            autoResetEvent.Set();
        }

        public void StartLevel(int level)
        {
            screnGame.Level = level;
            //levelInfo.Level = level;
            autoResetEvent.Reset();
        }

        public async void GameOver()
        {
            screnGame.IsShowGameOver = true;

            //autoResetEvent.WaitOne();
            await Task.Delay(ConfigurationWPF.TimeGameOver);
            screnScene.Clear();
            screnGame.Clear();
            levelInfo.Cler();
        }

        public void SetCountTankEnemy(int count)
        {
            levelInfo.CountTankEnemy = count;
        }

        public void SetCountTankPlaeyr(int count, Owner owner)
        {
            if (owner == Owner.IPlayer) levelInfo.CountTank1Player = count;
            else if (owner == Owner.IIPlayer) levelInfo.CountTank2Player = count;
        }
    }
}

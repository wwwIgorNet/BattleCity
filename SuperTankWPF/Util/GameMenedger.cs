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
        private LevelInfoViewModel levelInfo = ServiceLocator.Current.GetInstance<LevelInfoViewModel>();
        private ScrenScoreViewModel screnScore = ServiceLocator.Current.GetInstance<ScrenScoreViewModel>();
        IKeyboard Keyboard;

        public async void IPlayerExecute()
        {
            ServiceLocator.Current.GetInstance<MainViewModel>().ScrenGameVisibility = Visibility.Visible;
            
            await Task.Run(() =>
            {
                ServiceLocator.Current.GetInstance<ScrenGameViewModel>().IsShowAnimationNewLevel = true;
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
            ServiceHost hostSound = new ServiceHost(new FeykAudio());
            hostSound.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSound.AddServiceEndpoint(typeof(ISoundGame), new NetNamedPipeBinding(), "net.pipe://localhost/ISoundGame");
            hostSound.Open();

            ServiceHost hostSceneView = new ServiceHost(ServiceLocator.Current.GetInstance<ScrenSceneViewModel>());
            hostSceneView.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostSceneView.AddServiceEndpoint(typeof(IRender), new NetNamedPipeBinding(), "net.pipe://localhost/IRender");
            hostSceneView.Open();

            ServiceHost hostGameInfo = new ServiceHost(this);
            hostGameInfo.CloseTimeout = TimeSpan.FromMilliseconds(0);
            hostGameInfo.AddServiceEndpoint(typeof(IGameInfo), new NetNamedPipeBinding(), "net.pipe://localhost/IGameInfo");
            hostGameInfo.Open();
        }
        public void OpenChannel()
        {
            ChannelFactory<IKeyboard> factoryKeyboard = new ChannelFactory<IKeyboard>(new NetNamedPipeBinding(), "net.pipe://localhost/IKeyboard");
            Keyboard = factoryKeyboard.CreateChannel();
        }

        public void EndLevel(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr, int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr)
        {
            screnScore.Level = level;
            screnScore.Player1.TotalCountPoints = countPointsIIPlayer;
        }

        public void StartLevel(int level)
        {
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

        public void SetCountTankPlaeyr(int count, Owner owner)
        {
            if (owner == Owner.IPlayer) levelInfo.CountTank1Player = count;
            else if (owner == Owner.IIPlayer) levelInfo.CountTank2Player = count;
        }
    }
}

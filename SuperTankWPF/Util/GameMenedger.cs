using Microsoft.Practices.ServiceLocation;
using SuperTank;
using SuperTank.Audio;
using SuperTank.View;
using SuperTankWPF.Model;
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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class GameMenedger : IGameInfo, IDisposable
    {
        private ScrenRecordViewModel screnRecord = ServiceLocator.Current.GetInstance<ScrenRecordViewModel>();
        private ScrenGameViewModel screnGame = ServiceLocator.Current.GetInstance<ScrenGameViewModel>();
        private ScrenSceneViewModel screnScene = ServiceLocator.Current.GetInstance<ScrenSceneViewModel>();
        private LevelInfoViewModel levelInfo = ServiceLocator.Current.GetInstance<LevelInfoViewModel>();
        private ScrenScoreViewModel screnScore = ServiceLocator.Current.GetInstance<ScrenScoreViewModel>();
        private MainViewModel mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
        private ScrenConstructionViewModel construction = ServiceLocator.Current.GetInstance<ScrenConstructionViewModel>();
        private Comunication comunication = ServiceLocator.Current.GetInstance<Comunication>();
        private IViewSound sound = ServiceLocator.Current.GetInstance<IViewSound>();
        private Game game;

        public async void IPlayerExecute()
        {
            mainViewModel.ScrenGameVisibility = Visibility.Visible;

            await Task.Run(() =>
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
                if (construction.HasMap)
                {
                    game.Start(construction.GetAndClerMap(), null, null);
                    construction.Map = null;
                }
                else
                    game.Start(null, null);
            });

            ThreadPool.QueueUserWorkItem(s => screnGame.Keyboard = comunication.OpenChannel());
        }
        public void IIPlayerExecute()
        {
            MessageBox.Show("Command II PLAYERS");
        }
        public void ConstructionExecute()
        {
            mainViewModel.ScrenConstructionVisibility = Visibility.Visible;
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
                StpoGame();
                await Task.Delay(ConfigurationWPF.TimeGameOver);

                int maxPointsPath = int.Parse(File.ReadAllText(ConfigurationWPF.MaxPointsPath));
                if (maxPointsPath < countPointsIPlayer)
                {
                    sound.HighScore();
                    screnRecord.CountPoints = countPointsIPlayer;
                    mainViewModel.ScrenRecordVisibility = Visibility.Visible;
                    File.WriteAllText(ConfigurationWPF.MaxPointsPath, countPointsIPlayer.ToString());
                    await Task.Delay(ConfigurationWPF.DelayScrenRecord);
                }

                mainViewModel.StartScrenVisibility = Visibility.Visible;
            }
        }

        public async void StartLevel(int level)
        {
            screnGame.Level = level;
            sound.LevelStart();
            await Task.Delay((int)ConfigurationWPF.DelayScrenLoadLevel.TotalMilliseconds /2);
            levelInfo.Level = level;
        }

        public void GameOver()
        {
            screnGame.IsShowGameOver = true;
            sound.GameOver();
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

        public void Dispose()
        {
            StpoGame();
        }

        private void StpoGame()
        {
            screnGame.Keyboard = null;
            game?.Stop();
            comunication?.CloseChannelFactory();
            comunication?.CloseChannelFactory();
            game?.CloseHost();
            game?.CloseChannelFactory();
            comunication?.CloseHost();
        }
    }
}

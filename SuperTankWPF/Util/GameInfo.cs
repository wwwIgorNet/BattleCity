using SuperTank;
using SuperTank.Audio;
using SuperTankWPF.Model;
using SuperTankWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperTankWPF.Util
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class GameInfo : IGameInfo
    {
        private MainViewModel mainViewModel;
        private ScreenGameViewModel screnGame;
        private ScreenScoreViewModel screnScore;
        private ScreenRecordViewModel screnRecord;
        private LevelInfoViewModel levelInfo;
        private IViewSound sound;

        public GameInfo(MainViewModel mainViewModel, ScreenGameViewModel screnGame,
            LevelInfoViewModel levelInfo, ScreenScoreViewModel screnScore,
            ScreenRecordViewModel screnRecord, IViewSound sound)
        {
            this.mainViewModel = mainViewModel;
            this.screnGame = screnGame;
            this.levelInfo = levelInfo;
            this.screnScore = screnScore;
            this.screnRecord = screnRecord;
            this.sound = sound;
        }

        public event Action EndOfGame;

        public async void EndLevel(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr,
                                        int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr)
        {
            mainViewModel.ScreenScoreVisibility = Visibility.Visible;
            screnScore.Set(level, countPointsIPlayer, destrouTanksIPlaeyr, countPointsIIPlayer, destrouTanksIIPlaeyr);

            int delay = destrouTanksIPlaeyr.Values.Sum();
            if (destrouTanksIIPlaeyr != null) delay = Math.Max(delay, destrouTanksIIPlaeyr.Values.Sum());
            await Task.Delay(ConfigurationWPF.GetDelayScrenPoints(delay));

            if (!screnGame.IsShowGameOver)
            {
                mainViewModel.ScreenGameVisibility = Visibility.Visible;
            }
            else
                await GameOver(countPointsIPlayer);
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

        public void SetCountTankPlaeyr(int count, Owner owner)
        {
            if (owner == Owner.IPlayer) levelInfo.CountTank1Player = count;
            else if (owner == Owner.IIPlayer) levelInfo.CountTank2Player = count;
        }

        public async void StartLevel(int level)
        {
            screnGame.IsShowAnimationNewLevel = true;
            screnGame.Level = level;
            sound.LevelStart();
            await Task.Delay((int)ConfigurationWPF.DelayScrenLoadLevel.TotalMilliseconds / 2);
            levelInfo.Level = level;
        }

        private async Task GameOver(int countPointsPlayer)
        {
            mainViewModel.ScreenGameOverVisibility = Visibility.Visible;
            EndOfGame?.Invoke();
            await Task.Delay(ConfigurationWPF.TimeGameOver);

            int maxPointsPath = int.Parse(File.ReadAllText(ConfigurationWPF.MaxPointsPath));
            if (maxPointsPath < countPointsPlayer)
            {
                sound.HighScore();
                screnRecord.CountPoints = countPointsPlayer;
                mainViewModel.ScreenRecordVisibility = Visibility.Visible;
                File.WriteAllText(ConfigurationWPF.MaxPointsPath, countPointsPlayer.ToString());
                await Task.Delay(ConfigurationWPF.DelayScrenRecord);
            }

            mainViewModel.ScreenStartVisibility = Visibility.Visible;
        }
    }
}

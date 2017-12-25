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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SuperTankWPF.Util
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class GameInfo : IGameInfo
    {
        private MainViewModel mainViewModel;
        private ScreenGameViewModel screenGame;
        private ScreenScoreViewModel screenScore;
        private ScreenRecordViewModel screenRecord;
        private LevelInfoViewModel levelInfo;
        private IViewSound sound;

        public GameInfo(MainViewModel mainViewModel, ScreenGameViewModel screenGame,
            LevelInfoViewModel levelInfo, ScreenScoreViewModel screnScore,
            ScreenRecordViewModel screenRecord, IViewSound sound)
        {
            this.mainViewModel = mainViewModel;
            this.screenGame = screenGame;
            this.levelInfo = levelInfo;
            this.screenScore = screnScore;
            this.screenRecord = screenRecord;
            this.sound = sound;
        }

        public event Action EndOfGame;
        public Owner OwnerPlayer { get; set; }


        public async void EndLevel(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr,
                                        int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr)
        {
            mainViewModel.ScreenScoreVisibility = Visibility.Visible;
            screenScore.Set(level, countPointsIPlayer, destrouTanksIPlaeyr, countPointsIIPlayer, destrouTanksIIPlaeyr);

            int delay = destrouTanksIPlaeyr.Values.Sum();
            if (destrouTanksIIPlaeyr != null) delay = Math.Max(delay, destrouTanksIIPlaeyr.Values.Sum());
            await Task.Delay(ConfigurationWPF.GetDelayScrenPoints(delay));

            if (!screenGame.IsShowGameOver)
            {
                mainViewModel.ScreenGameVisibility = Visibility.Visible;
            }
            else
            {
                int countPoints = 0;
                if (OwnerPlayer == Owner.IPlayer)
                    countPoints = countPointsIPlayer;
                else if (OwnerPlayer == Owner.IIPlayer)
                    countPoints = countPointsIIPlayer;

                await GameOver(countPoints);
            }
        }

        public void GameOver()
        {
            screenGame.IsShowGameOver = true;
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
            screenGame.IsShowAnimationNewLevel = true;
            screenGame.Level = level;
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
                screenRecord.CountPoints = countPointsPlayer;
                mainViewModel.ScreenRecordVisibility = Visibility.Visible;
                File.WriteAllText(ConfigurationWPF.MaxPointsPath, countPointsPlayer.ToString());
                await Task.Delay(ConfigurationWPF.DelayScrenRecord);
            }

            if(mainViewModel.ScreenGameOverVisibility == Visibility.Visible || mainViewModel.ScreenRecordVisibility == Visibility.Visible)
            {
                mainViewModel.ScreenStartVisibility = Visibility.Visible;
            }
        }

        public void StartGame()
        {
            mainViewModel.ScreenGameVisibility = Visibility.Visible;
        }

        public void PauseGame(bool isPause)
        {
            screenGame.IsPause = isPause;
        }
    }
}

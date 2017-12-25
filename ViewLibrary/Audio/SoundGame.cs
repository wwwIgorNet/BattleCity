using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using ViewLibrary.Audio;

namespace ViewLibrary.Audio
{
    /// <summary>
    /// All sounds of the game
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SoundGame : ISoundGame, IViewSound, IDisposable
    {
        private enum NameSound
        {
            Stop,
            Move,
            GameStart,
            GameOver,
            Fire,
            BigDetonation,
            DetonationShell,
            Glide,
            DetonationBrickWall,
            DetonationEagle,
            Bonus,
            NewBonus,
            CountTankIncrement,
            HighScore,
            TwoFire
        }

        private readonly Dictionary<NameSound, ISoundPlayer> soundPlayers = new Dictionary<NameSound, ISoundPlayer>();
        private float volume;

        public float Volume
        {
            get => volume;
            set
            {
                volume = value;

                foreach (var sp in soundPlayers)
                    sp.Value.Volume = value;
            }
        }

        public void OpenMedia(string path)
        {
            soundPlayers.Add(NameSound.Move, new GameSoundPlayer(path + "SoundMove.wav"));

            soundPlayers.Add(NameSound.Stop, new GameSoundPlayer(path + "SoundStop.wav"));

            soundPlayers.Add(NameSound.GameStart, new GameSoundPlayer(path + "GameStart.wav"));

            soundPlayers.Add(NameSound.GameOver, new GameSoundPlayer(path + "GameOver.wav"));

            soundPlayers.Add(NameSound.Fire, new GameSoundPlayer(path + "Fire.wav"));

            soundPlayers.Add(NameSound.BigDetonation, new GameSoundPlayer(path + "DetonationShellBig.wav"));

            soundPlayers.Add(NameSound.DetonationShell, new GameSoundPlayer(path + "DetonationShell.wav"));

            soundPlayers.Add(NameSound.Glide, new GameSoundPlayer(path + "Glide.wav"));

            soundPlayers.Add(NameSound.DetonationBrickWall, new GameSoundPlayer(path + "DetonationBrickWall.wav"));

            soundPlayers.Add(NameSound.DetonationEagle, new GameSoundPlayer(path + "DetonationEagle.wav"));

            soundPlayers.Add(NameSound.Bonus, new GameSoundPlayer(path + "Bonus.wav"));

            soundPlayers.Add(NameSound.NewBonus, new GameSoundPlayer(path + "NewBonus.wav"));

            soundPlayers.Add(NameSound.CountTankIncrement, new GameSoundPlayer(path + "CountTankIncrement.wav"));

            soundPlayers.Add(NameSound.HighScore, new GameSoundPlayer(path + "HighScore.wav"));

            soundPlayers.Add(NameSound.TwoFire, new GameSoundPlayer(path + "TwoFire.wav"));

            volume = soundPlayers[NameSound.Move].Volume;
        }

        public void GameOver()
        {
            soundPlayers[NameSound.GameOver].Play();

        }
        public void LevelStart()
        {
            soundPlayers[NameSound.GameStart].Play();
        }
        public void Fire()
        {
            soundPlayers[NameSound.Fire].Play();
        }
        public void TwoFire()
        {
            soundPlayers[NameSound.TwoFire].Play();
        }
        public void DetonationTank()
        {
            soundPlayers[NameSound.BigDetonation].Play();
        }
        public void DetonationShell()
        {
            soundPlayers[NameSound.DetonationShell].Play();
        }
        public void Glide()
        {
            soundPlayers[NameSound.Glide].Play();
        }
        public void Move()
        {
            soundPlayers[NameSound.Stop].Pause();
            soundPlayers[NameSound.Move].Play();
        }
        public void StopSondTank()
        {
            soundPlayers[NameSound.Move].Pause();
            soundPlayers[NameSound.Stop].Play();
        }
        public void Dispose()
        {
            foreach (var sp in soundPlayers)
                ((IDisposable)sp.Value).Dispose();
        }
        public void StopAll()
        {
            foreach (var sp in soundPlayers)
                sp.Value.Pause();
        }
        public void DetonationBrickWall()
        {
            soundPlayers[NameSound.DetonationBrickWall].Play();
        }
        public void TankSoundStopMove()
        {
            soundPlayers[NameSound.Stop].Pause();
            soundPlayers[NameSound.Move].Pause();
        }
        public void DetonationEagle()
        {
            soundPlayers[NameSound.DetonationEagle].Play();
        }
        public void Bonus()
        {
            soundPlayers[NameSound.Bonus].Play();
        }
        public void NewBonus()
        {
            soundPlayers[NameSound.NewBonus].Play();
        }
        public void CountTankIncrement()
        {
            soundPlayers[NameSound.CountTankIncrement].Play();
        }
        public void HighScore()
        {
            soundPlayers[NameSound.HighScore].Play();
        }
        public void HighScoreStop()
        {
            soundPlayers[NameSound.HighScore].Pause();
        }
    }
}
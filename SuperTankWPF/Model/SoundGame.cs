using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SuperTankWPF.Model
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

        private SynchronizationContext synchronizationContext;
        private readonly Dictionary<NameSound, MediaPlayer> mediaPlayers = new Dictionary<NameSound, MediaPlayer>();
        private int countSoundNotLoaded = Enum.GetValues(typeof(NameSound)).Length;

        public SoundGame(SynchronizationContext synchronizationContext)
        {
            this.synchronizationContext = synchronizationContext;
        }

        public static Action IsLoaded;

        public void OpenMedia(string path, UriKind uriKind)
        {
            synchronizationContext.Post(s =>
            {
                foreach (NameSound name in Enum.GetValues(typeof(NameSound)))
                {
                    MediaPlayer mp = new MediaPlayer();
                    mp.MediaOpened += MediaOpened;
                    mediaPlayers.Add(name, mp);
                }

                mediaPlayers[NameSound.Move].Open(new Uri(path + "SoundMove.wav", uriKind));
                mediaPlayers[NameSound.Move].MediaEnded += SoundGame_Move_MediaEnded;

                mediaPlayers[NameSound.Stop].Open(new Uri(path + "SoundStop.wav", uriKind));
                mediaPlayers[NameSound.Stop].MediaEnded += SoundGame_Stop_MediaEnded;

                mediaPlayers[NameSound.GameStart].Open(new Uri(path + "GameStart.wav", uriKind));

                mediaPlayers[NameSound.GameOver].Open(new Uri(path + "GameOver.wav", uriKind));

                mediaPlayers[NameSound.Fire].Open(new Uri(path + "Fire.wav", uriKind));

                mediaPlayers[NameSound.BigDetonation].Open(new Uri(path + "DetonationShellBig.wav", uriKind));

                mediaPlayers[NameSound.DetonationShell].Open(new Uri(path + "DetonationShell.wav", uriKind));

                mediaPlayers[NameSound.Glide].Open(new Uri(path + "Glide.wav", uriKind));

                mediaPlayers[NameSound.DetonationBrickWall].Open(new Uri(path + "DetonationBrickWall.wav", uriKind));

                mediaPlayers[NameSound.DetonationEagle].Open(new Uri(path + "DetonationEagle.wav", uriKind));

                mediaPlayers[NameSound.Bonus].Open(new Uri(path + "Bonus.wav", uriKind));

                mediaPlayers[NameSound.NewBonus].Open(new Uri(path + "NewBonus.wav", uriKind));

                mediaPlayers[NameSound.CountTankIncrement].Open(new Uri(path + "CountTankIncrement.wav", uriKind));

                mediaPlayers[NameSound.HighScore].Open(new Uri(path + "HighScore.wav", uriKind));

                mediaPlayers[NameSound.TwoFire].Open(new Uri(path + "TwoFire.wav", uriKind));
            }, null);
        }

        private void SoundGame_Move_MediaEnded(object sender, EventArgs e)
        {
            mediaPlayers[NameSound.Move].Stop();
            mediaPlayers[NameSound.Move].Play();
        }

        private void SoundGame_Stop_MediaEnded(object sender, EventArgs e)
        {
            mediaPlayers[NameSound.Stop].Stop();
            mediaPlayers[NameSound.Stop].Play();
        }

        private void MediaOpened(object sender, EventArgs e)
        {
            countSoundNotLoaded--;
            if (countSoundNotLoaded == 0)
                IsLoaded?.Invoke();
        }

        public void GameOver()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.GameOver].Stop();
                mediaPlayers[NameSound.GameOver].Play();
            }, null);
        }
        public void LevelStart()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.GameStart].Stop();
                mediaPlayers[NameSound.GameStart].Play();
            }, null);
        }
        public void Fire()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.Fire].Stop();
                mediaPlayers[NameSound.Fire].Play();
            }, null);
        }
        public void TwoFire()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.TwoFire].Stop();
                mediaPlayers[NameSound.TwoFire].Play();
            }, null);
        }
        public void DetonationTank()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.BigDetonation].Stop();
                mediaPlayers[NameSound.BigDetonation].Play();
            }, null);
        }
        public void DetonationShell()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.DetonationShell].Stop();
                mediaPlayers[NameSound.DetonationShell].Play();
            }, null);
        }
        public void Glide()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.Glide].Stop();
                mediaPlayers[NameSound.Glide].Play();
            }, null);
        }
        public void Move()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.Stop].Stop();
                mediaPlayers[NameSound.Move].Play();
            }, null);
        }
        public void Stop()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.Move].Stop();
                mediaPlayers[NameSound.Stop].Play();
            }, null);
        }
        public void Dispose()
        {
            synchronizationContext.Post(s =>
               {
                   foreach (NameSound name in Enum.GetValues(typeof(NameSound)))
                       mediaPlayers[name].Close();
               }, null);
        }
        public void DetonationBrickWall()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.DetonationBrickWall].Stop();
                mediaPlayers[NameSound.DetonationBrickWall].Play();
            }, null);
        }
        public void TankSoundStop()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.Stop].Stop();
                mediaPlayers[NameSound.Move].Stop();
            }, null);
        }
        public void DetonationEagle()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.DetonationEagle].Stop();
                mediaPlayers[NameSound.DetonationEagle].Play();
            }, null);
        }
        public void Bonus()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.Bonus].Stop();
                mediaPlayers[NameSound.Bonus].Play();
            }, null);
        }
        public void NewBonus()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.NewBonus].Stop();
                mediaPlayers[NameSound.NewBonus].Play();
            }, null);
        }
        public void CountTankIncrement()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.CountTankIncrement].Stop();
                mediaPlayers[NameSound.CountTankIncrement].Play();
            }, null);
        }
        public void HighScore()
        {
            synchronizationContext.Post(s =>
            {
                mediaPlayers[NameSound.HighScore].Stop();
                mediaPlayers[NameSound.HighScore].Play();
            }, null);
        }

        public void HighScoreStop()
        {
            mediaPlayers[NameSound.HighScore].Stop();
        }
    }
}
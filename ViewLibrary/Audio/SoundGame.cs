using System;
using SuperTank.View;
using System.Windows.Media;
using System.ServiceModel;
using System.Threading;
using System.Collections.Generic;

namespace SuperTank.Audio
{
    /// <summary>
    /// All sounds of the game
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SoundGame : ISoundGame, IViewSound, IDisposable
    {
        private enum NameSound {
            Stop,
            Move,
            Glide,
            DetonationShell,
            BigDetonation,
            Fire,
            Fire2,
            GameStart,
            GameOver,
            DetonationBrickWall,
            DetonationEagle,
            Bonus,
            NewBonus,
            CountTankIncrement,
            HighScore,
            TwoFire
        }

        private readonly Dictionary<NameSound, MediaPlayer> mediaPlayers = new Dictionary<NameSound, MediaPlayer>();

        private readonly CountdownEvent countdownEvent = new CountdownEvent(Enum.GetValues(typeof(NameSound)).Length - 1);

        public SoundGame()
        {
            OpenMedia(ConfigurationView.SoundPath, UriKind.Relative);
        }
        public SoundGame(string path)
        {
            OpenMedia(path, UriKind.Relative);
        }

        public void OpenMedia(string path, UriKind uriKind)
        {
            ThreadPool.QueueUserWorkItem(s =>
            {
                DateTime start = DateTime.Now;
                countdownEvent.Wait();

                Console.WriteLine(DateTime.Now - start);
            });

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

            //countdownEvent.Wait();
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
            countdownEvent.Signal();
        }

        public void GameOver()
        {
            mediaPlayers[NameSound.GameOver].Stop();
            mediaPlayers[NameSound.GameOver].Play();
        }
        public void LevelStart()
        {
            mediaPlayers[NameSound.GameStart].Stop();
            mediaPlayers[NameSound.GameStart].Play();
        }
        public void Fire()
        {
            mediaPlayers[NameSound.Fire].Stop();
            mediaPlayers[NameSound.Fire].Play();
        }
        public void TwoFire()
        {
            mediaPlayers[NameSound.TwoFire].Stop();
            mediaPlayers[NameSound.TwoFire].Play();
        }
        public void DetonationTank()
        {
            mediaPlayers[NameSound.BigDetonation].Stop();
            mediaPlayers[NameSound.BigDetonation].Play();
        }
        public void DetonationShell()
        {
            mediaPlayers[NameSound.DetonationShell].Stop();
            mediaPlayers[NameSound.DetonationShell].Play();
        }
        public void Glide()
        {
            mediaPlayers[NameSound.Glide].Stop();
            mediaPlayers[NameSound.Glide].Play();
        }
        public void Move()
        {
            mediaPlayers[NameSound.Stop].Stop();
            mediaPlayers[NameSound.Move].Play();
        }
        public void Stop()
        {
            mediaPlayers[NameSound.Move].Stop();
            mediaPlayers[NameSound.Stop].Play();
        }
        public void Dispose()
        {
            foreach (NameSound name in Enum.GetValues(typeof(NameSound)))
                mediaPlayers[name].Close();
        }
        public void DetonationBrickWall()
        {
            mediaPlayers[NameSound.DetonationBrickWall].Stop();
            mediaPlayers[NameSound.DetonationBrickWall].Play();
        }
        public void TankSoundStop()
        {
            mediaPlayers[NameSound.Stop].Stop();
            mediaPlayers[NameSound.Move].Stop();
        }
        public void DetonationEagle()
        {
            mediaPlayers[NameSound.DetonationEagle].Stop();
            mediaPlayers[NameSound.DetonationEagle].Play();
        }
        public void Bonus()
        {
            mediaPlayers[NameSound.Bonus].Stop();
            mediaPlayers[NameSound.Bonus].Play();
        }
        public void NewBonus()
        {
            mediaPlayers[NameSound.NewBonus].Stop();
            mediaPlayers[NameSound.NewBonus].Play();
        }
        public void CountTankIncrement()
        {
            mediaPlayers[NameSound.CountTankIncrement].Stop();
            mediaPlayers[NameSound.CountTankIncrement].Play();
        }
        public void HighScore()
        {
            mediaPlayers[NameSound.HighScore].Stop();
            mediaPlayers[NameSound.HighScore].Play();
        }

        public void HighScoreStop()
        {
            mediaPlayers[NameSound.HighScore].Stop();
        }
    }
}
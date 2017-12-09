using System;
using SuperTank.View;
using System.Windows.Media;
using System.ServiceModel;

namespace SuperTank.Audio
{
    /// <summary>
    /// All sounds of the game
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SoundGame : ISoundGame, IViewSound, IDisposable
    {
        protected readonly MediaPlayer stop = new MediaPlayer();
        protected readonly MediaPlayer move = new MediaPlayer();
        protected readonly MediaPlayer glide = new MediaPlayer();
        protected readonly MediaPlayer detonationShell = new MediaPlayer();
        protected readonly MediaPlayer bigDetonation = new MediaPlayer();
        protected readonly MediaPlayer fire = new MediaPlayer();
        protected readonly MediaPlayer fire2 = new MediaPlayer();
        protected readonly MediaPlayer gameStart = new MediaPlayer();
        protected readonly MediaPlayer gameOver = new MediaPlayer();
        protected readonly MediaPlayer detonationBrickWall = new MediaPlayer();
        protected readonly MediaPlayer detonationEagle = new MediaPlayer();
        protected readonly MediaPlayer bonus = new MediaPlayer();
        protected readonly MediaPlayer newBonus = new MediaPlayer();
        protected readonly MediaPlayer countTankIncrement = new MediaPlayer();
        protected readonly MediaPlayer highScore = new MediaPlayer();
        protected readonly MediaPlayer twoFire = new MediaPlayer();

        public SoundGame()
        {
            OpenMedia(ConfigurationView.SoundPath);
        }
        public SoundGame(string path)
        {
            OpenMedia(path);
        }

        protected virtual void OpenMedia(string path)
        {
            move.Open(new Uri(path + "SoundMove.wav", UriKind.Relative));

            stop.Open(new Uri(path + "SoundStop.wav", UriKind.Relative));

            gameStart.Open(new Uri(path + "GameStart.wav", UriKind.Relative));

            gameOver.Open(new Uri(path + "GameOver.wav", UriKind.Relative));

            fire.Open(new Uri(path + "Fire.wav", UriKind.Relative));

            bigDetonation.Open(new Uri(path + "DetonationShellBig.wav", UriKind.Relative));

            detonationShell.Open(new Uri(path + "DetonationShell.wav", UriKind.Relative));

            glide.Open(new Uri(path + "Glide.wav", UriKind.Relative));

            detonationBrickWall.Open(new Uri(path + "DetonationBrickWall.wav", UriKind.Relative));

            detonationEagle.Open(new Uri(path + "DetonationEagle.wav", UriKind.Relative));

            bonus.Open(new Uri(path + "Bonus.wav", UriKind.Relative));

            newBonus.Open(new Uri(path + "NewBonus.wav", UriKind.Relative));

            countTankIncrement.Open(new Uri(path + "CountTankIncrement.wav", UriKind.Relative));

            highScore.Open(new Uri(path + "HighScore.wav", UriKind.Relative));

            twoFire.Open(new Uri(path + "TwoFire.wav", UriKind.Relative));
        }

        public void GameOver()
        {
            gameOver.Stop();
            gameOver.Play();
        }
        public void LevelStart()
        {
            gameStart.Stop();
            gameStart.Play();
        }
        public void Fire()
        {
            fire.Stop();
            fire.Play();
        }
        public void TwoFire()
        {
            twoFire.Stop();
            twoFire.Play();
        }
        public void DetonationTank()
        {
            bigDetonation.Stop();
            bigDetonation.Play();
        }
        public void DetonationShell()
        {
            detonationShell.Stop();
            detonationShell.Play();
        }
        public void Glide()
        {
            glide.Stop();
            glide.Play();
        }
        public void Move()
        {
            stop.Stop();
            move.Play();
        }
        public void Stop()
        {
            move.Stop();
            stop.Play();
        }
        public void Dispose()
        {
            stop.Close();
            move.Close();
            glide.Close();
            detonationShell.Close();
            bigDetonation.Close();
            fire.Close();
            gameStart.Close();
            gameOver.Close();
            detonationBrickWall.Close();
            highScore.Close();
            twoFire.Close();
        }
        public void DetonationBrickWall()
        {
            detonationBrickWall.Stop();
            detonationBrickWall.Play();
        }
        public void TankSoundStop()
        {
            stop.Stop();
            move.Stop();
        }
        public void DetonationEagle()
        {
            detonationEagle.Stop();
            detonationEagle.Play();
        }
        public void Bonus()
        {
            bonus.Stop();
            bonus.Play();
        }
        public void NewBonus()
        {
            newBonus.Stop();
            newBonus.Play();
        }
        public void CountTankIncrement()
        {
            countTankIncrement.Stop();
            countTankIncrement.Play();
        }
        public void HighScore()
        {
            highScore.Play();
        }
    }
}
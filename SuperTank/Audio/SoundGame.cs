using System;
using SuperTank.View;
using System.Windows.Media;
using System.ServiceModel;

namespace SuperTank.Audio
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class SoundGame : ISoundGame, IViewSound, IDisposable
    {
        private readonly MediaPlayer stop = new MediaPlayer();
        private readonly MediaPlayer move = new MediaPlayer();
        private readonly MediaPlayer glide = new MediaPlayer();
        private readonly MediaPlayer detonationShell = new MediaPlayer();
        private readonly MediaPlayer bigDetonation = new MediaPlayer();
        private readonly MediaPlayer fire = new MediaPlayer();
        private readonly MediaPlayer fire2 = new MediaPlayer();
        private readonly MediaPlayer gameStart = new MediaPlayer();
        private readonly MediaPlayer gameOver = new MediaPlayer();
        private readonly MediaPlayer detonationBrickWall = new MediaPlayer();
        private readonly MediaPlayer detonationEagle = new MediaPlayer();
        private readonly MediaPlayer bonus = new MediaPlayer();
        private readonly MediaPlayer newBonus = new MediaPlayer();
        private readonly MediaPlayer countTankIncrement = new MediaPlayer();
        private readonly MediaPlayer highScore = new MediaPlayer();
        private readonly MediaPlayer twoFire = new MediaPlayer();

        public SoundGame()
        {
            move.Open(new Uri(ConfigurationView.SoundPath + "SoundMove.wav", UriKind.Relative));

            stop.Open(new Uri(ConfigurationView.SoundPath + "SoundStop.wav", UriKind.Relative));

            gameStart.Open(new Uri(ConfigurationView.SoundPath + "GameStart.wav", UriKind.Relative));

            gameOver.Open(new Uri(ConfigurationView.SoundPath + "GameOver.wav", UriKind.Relative));

            fire.Open(new Uri(ConfigurationView.SoundPath + "Fire.wav", UriKind.Relative));

            bigDetonation.Open(new Uri(ConfigurationView.SoundPath + "DetonationShellBig.wav", UriKind.Relative));

            detonationShell.Open(new Uri(ConfigurationView.SoundPath + "DetonationShell.wav", UriKind.Relative));

            glide.Open(new Uri(ConfigurationView.SoundPath + "Glide.wav", UriKind.Relative));

            detonationBrickWall.Open(new Uri(ConfigurationView.SoundPath + "DetonationBrickWall.wav", UriKind.Relative));

            detonationEagle.Open(new Uri(ConfigurationView.SoundPath + "DetonationEagle.wav", UriKind.Relative));

            bonus.Open(new Uri(ConfigurationView.SoundPath + "Bonus.wav", UriKind.Relative));

            newBonus.Open(new Uri(ConfigurationView.SoundPath + "NewBonus.wav", UriKind.Relative));

            countTankIncrement.Open(new Uri(ConfigurationView.SoundPath + "CountTankIncrement.wav", UriKind.Relative));

            highScore.Open(new Uri(ConfigurationView.SoundPath + "HighScore.wav", UriKind.Relative));

            twoFire.Open(new Uri(ConfigurationView.SoundPath + "TwoFire.wav", UriKind.Relative));
        }

        public void GameOver()
        {
            gameOver.Stop();
            gameOver.Play();
        }
        public void GameStart()
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
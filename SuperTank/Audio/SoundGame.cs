using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using SuperTank.View;
using System.Drawing;
using System.Windows.Media;
using System.ServiceModel;

namespace SuperTank.Audio
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class SoundGame : ISoundGame, IDisposable
    {
        private readonly MediaPlayer stop = new MediaPlayer();
        private readonly MediaPlayer move = new MediaPlayer();
        private readonly MediaPlayer glide = new MediaPlayer();
        private readonly MediaPlayer detonationShell = new MediaPlayer();
        private readonly MediaPlayer bigDetonation = new MediaPlayer();
        private readonly MediaPlayer fire = new MediaPlayer();
        private readonly MediaPlayer gameStart = new MediaPlayer();
        private readonly MediaPlayer gameOver = new MediaPlayer();
        private readonly MediaPlayer detonationBrickWall = new MediaPlayer();

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

            fire.MediaEnded += MediaEnded_Stop;
            detonationBrickWall.MediaEnded += MediaEnded_Stop;
            detonationShell.MediaEnded += MediaEnded_Stop;
            glide.MediaEnded += MediaEnded_Stop;
            move.MediaEnded += MediaEnded_Play;
            stop.MediaEnded += MediaEnded_Play;
        }

        private void MediaEnded_Play(object sender, EventArgs e)
        {
            ((MediaPlayer)sender).Play();
        }

        private void MediaEnded_Stop(object sender, EventArgs e)
        {
            ((MediaPlayer)sender).Stop();
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

        public void BigDetonation()
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
        }

        public void DetonationBrickWall()
        {
            detonationBrickWall.Stop();
            detonationBrickWall.Play();
        }
    }
}
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
    class SoundGame : ISoundGame
    {
        public SoundGame()
        {
            move.Open(new Uri(ConfigurationView.SoundPath + "SoundMove.wav", UriKind.Relative));

            stop.Open(new Uri(ConfigurationView.SoundPath + "SoundStop.wav", UriKind.Relative));

            gameStart.Open(new Uri(ConfigurationView.SoundPath + "GameStart.wav", UriKind.Relative));

            gameOver.Open(new Uri(ConfigurationView.SoundPath + "GameOver.wav", UriKind.Relative));

            fire.Open(new Uri(ConfigurationView.SoundPath + "Fire.wav", UriKind.Relative));

            bigDetonation.Open(new Uri(ConfigurationView.SoundPath + "DetonationShellBig.wav", UriKind.Relative));

            detonation.Open(new Uri(ConfigurationView.SoundPath + "Detonation.wav", UriKind.Relative));

            glide.Open(new Uri(ConfigurationView.SoundPath + "Glide.wav", UriKind.Relative));
        }

        private MediaPlayer gameOver = new MediaPlayer();
        public void GameOver()
        {
            gameOver.Stop();
            gameOver.Play();
        }

        private MediaPlayer gameStart = new MediaPlayer();
        public void GameStart()
        {
            gameStart.Stop();
            gameStart.Play();
        }

        private MediaPlayer fire = new MediaPlayer();
        public void Fire()
        {
            fire.Stop();
            fire.Play();
        }

        private MediaPlayer bigDetonation = new MediaPlayer();
        public void BigDetonation()
        {
            bigDetonation.Stop();
            bigDetonation.Play();
        }


        private MediaPlayer detonation = new MediaPlayer();
        public void Detonation()
        {
            detonation.Stop();
            detonation.Play();
        }

        private MediaPlayer glide = new MediaPlayer();
        public void Glide()
        {
            glide.Play();
        }

        private MediaPlayer move = new MediaPlayer();
        ///private bool move;
        public void Move()
        {
            //if (!move)
            //{
            stop.Stop();
            move.Play();
            //    move = true;
            //    stop = false;
            //}
        }

        private MediaPlayer stop = new MediaPlayer();
        //private bool stop;
        public void Stop()
        {
            //if (!stop)
            //{
            move.Stop();
            stop.Play();
                //stop = true;
                //move = false;
            //}
        }
}
}

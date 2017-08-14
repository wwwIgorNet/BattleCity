using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using SuperTank.View;
using System.Drawing;

namespace SuperTank.Audio
{
    class SoundGame
    {
        public static void LoadSound()
        {
            soundMove.Open(new Uri(ConfigurationView.SoundPath + "SoundMove.wav", UriKind.Relative));

            soundStop.Open(new Uri(ConfigurationView.SoundPath + "SoundStop.wav", UriKind.Relative));

            gameStart.Open(new Uri(ConfigurationView.SoundPath + "GameStart.wav", UriKind.Relative));

            gameOver.Open(new Uri(ConfigurationView.SoundPath + "GameOver.wav", UriKind.Relative));

            soundFire.Open(new Uri(ConfigurationView.SoundPath + "Fire.wav", UriKind.Relative));

            soundBigDetonation.Open(new Uri(ConfigurationView.SoundPath + "DetonationShellBig.wav", UriKind.Relative));

            soundDetonation.Open(new Uri(ConfigurationView.SoundPath + "Detonation.wav", UriKind.Relative));

            soundGlide.Open(new Uri(ConfigurationView.SoundPath + "Glide.wav", UriKind.Relative));
        }

        private static System.Windows.Media.MediaPlayer gameOver = new System.Windows.Media.MediaPlayer();
        public static void GameOver()
        {
            gameOver.Stop();
            gameOver.Play();
        }

        private static System.Windows.Media.MediaPlayer gameStart = new System.Windows.Media.MediaPlayer();
        public static void GameStart()
        {
            gameStart.Stop();
            gameStart.Play();
        }

        private static System.Windows.Media.MediaPlayer soundFire = new System.Windows.Media.MediaPlayer();
        public static void SoundFire()
        {
            soundFire.Stop();
            soundFire.Play();
        }

        private static System.Windows.Media.MediaPlayer soundBigDetonation = new System.Windows.Media.MediaPlayer();
        public static void SoundBigDetonation()
        {
            soundBigDetonation.Stop();
            soundBigDetonation.Play();
        }


        private static System.Windows.Media.MediaPlayer soundDetonation = new System.Windows.Media.MediaPlayer();
        public static void SoundDetonation()
        {
            soundDetonation.Stop();
            soundDetonation.Play();
        }

        private static System.Windows.Media.MediaPlayer soundGlide = new System.Windows.Media.MediaPlayer();
        public static void SoundGlide()
        {
            soundGlide.Play();
        }

        private static System.Windows.Media.MediaPlayer soundMove = new System.Windows.Media.MediaPlayer();
        private static bool move;
        public static void SoundMove()
        {
            //if (!move)
            //{
            soundStop.Stop();
            soundMove.Play();
            //    move = true;
            //    stop = false;
            //}
        }

        private static System.Windows.Media.MediaPlayer soundStop = new System.Windows.Media.MediaPlayer();
        private static bool stop;
        public static void SoundStop()
        {
            //if (!stop)
            //{
            soundMove.Stop();
            soundStop.Play();
                //stop = true;
                //move = false;
            //}
        }

    public static void Stop()
    {
        SoundGame.soundMove.Stop();
        SoundGame.soundStop.Stop();
        move = false;
        stop = false;
    }

}
}

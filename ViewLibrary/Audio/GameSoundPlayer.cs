using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewLibrary.Audio
{

    public class GameSoundPlayer : ISoundPlayer, IDisposable
    {
        private WaveFileReader waveFileReader;
        private WaveChannel32 waveChannel32;
        private WaveOut directSoundOut;

        public float Volume
        {
            get { return directSoundOut.Volume; }
            set { directSoundOut.Volume = value; }
        }

        public GameSoundPlayer(string soundFile)
        {
            waveFileReader = new WaveFileReader(soundFile);
            waveChannel32 = new WaveChannel32(waveFileReader);
            directSoundOut = new WaveOut();
            directSoundOut.Init(waveChannel32);
            // buffering
            waveChannel32.Position = waveChannel32.Length - 1;
            directSoundOut.Play();
        }

        public void Dispose()
        {
            waveFileReader.Dispose();
            waveChannel32.Dispose();
            if (directSoundOut.PlaybackState != PlaybackState.Stopped) Stop();
            directSoundOut.Dispose();
        }

        public void Play()
        {
            waveChannel32.Position = 0;
            directSoundOut.Play();
        }

        public void Stop()
        {
            directSoundOut.Stop();
        }

        public void Pause()
        {
            directSoundOut.Pause();
        }
    }
}

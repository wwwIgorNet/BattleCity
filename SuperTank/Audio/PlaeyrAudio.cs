using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Audio
{
    public class PlaeyrAudio
    {
        public int ID { get; set; }
        public bool IsStop
        {
            set
            {
                if (value) SoundGame.SoundStop();
                else SoundGame.SoundMove();
            }
        }
        public bool Glide
        {
            set
            {
                if (value) SoundGame.SoundGlide();
            }
        }

        public void Fire()
        {

        }
        public void Detonatio()
        {

        }

    }
}

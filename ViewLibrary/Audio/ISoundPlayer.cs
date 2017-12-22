using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewLibrary.Audio
{
    public interface ISoundPlayer
    {
        void Play();
        void Stop();
        void Pause();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SuperTankWPF.Util
{
    class AnimationMenedger
    {
        public static List<IItimerGame> Animations { get; } = new List<IItimerGame>();

        public static void Pause(bool isPause)
        {
            foreach (var anim in Animations)
                anim.Pause(isPause);
        }
    }
}

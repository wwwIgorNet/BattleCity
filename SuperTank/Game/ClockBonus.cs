using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class ClockBonus : IUpdatable, IDisposable
    {
        private int iteration;

        public void Update()
        {
            if (iteration < ConfigurationGame.DelayPauseForClockBonus)
            {
                SetIsPause(true);
                iteration++;
            }
            else Dispose();
        }

        private static void SetIsPause(bool isPause)
        {
            for (int i = 0; i < Scene.Tanks.Count; i++)
            {
                if ((Owner)Scene.Tanks[i].Properties[PropertiesType.Owner] == Owner.Enemy)
                {
                    Scene.Tanks[i].IsPause = isPause;
                }
            }
        }

        public void Start()
        {
            Game.Updatable.Add(this);
            SetIsPause(true);
            iteration = 0;
        }

        public void Dispose()
        {
            Game.Updatable.Remove(this);

            SetIsPause(false);
        }
    }
}

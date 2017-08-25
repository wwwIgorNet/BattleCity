using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class ClockBonus : IUpdatable, IDisposable
    {
        private static DateTime startTime;

        public void Update()
        {
            if (DateTime.Now - startTime < TimeSpan.FromSeconds(10))
            {
                SetIsPause(true);
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
            startTime = DateTime.Now;
        }

        public void Dispose()
        {
            Game.Updatable.Remove(this);

            SetIsPause(false);
        }
    }
}

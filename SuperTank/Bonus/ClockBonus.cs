using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class ClockBonus : UpdatableBase
    {
        private static DateTime startTime;

        public override void Update()
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

        public override void Start()
        {
            base.Start();
            SetIsPause(true);
            startTime = DateTime.Now;
        }

        public override void Dispose()
        {
            base.Dispose();
            SetIsPause(false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class HelmetBonus : UpdatableBase
    {
        private Tank tank;
        private static DateTime startTime;
        private TimeSpan timeOfAction;

        public HelmetBonus(Tank tank, int timeOfAction)
        {
            this.tank = tank;
            this.timeOfAction = TimeSpan.FromSeconds(timeOfAction);
        }

        public override void Start()
        {
            base.Start();
            tank.Properties[PropertiesType.IsInvulnerable] = true;
            startTime = DateTime.Now;
        }

        public override void Dispose()
        {
            base.Dispose();
            tank.Properties[PropertiesType.IsInvulnerable] = false;
        }

        public override void Update()
        {
            if (DateTime.Now - startTime > timeOfAction)
                Dispose();
        }
    }
}

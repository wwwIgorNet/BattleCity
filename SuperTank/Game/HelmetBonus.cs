using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class HelmetBonus : IUpdatable, IDisposable
    {
        private Tank tank;
        private static DateTime startTime;
        private TimeSpan timeOfAction;

        public HelmetBonus(Tank tank, int timeOfAction)
        {
            this.tank = tank;
            this.timeOfAction = TimeSpan.FromSeconds(timeOfAction);
        }

        public void Start()
        {
            Game.Updatable.Add(this);
            tank.Properties[PropertiesType.IsInvulnerable] = true;
            startTime = DateTime.Now;
        }

        public void Dispose()
        {
            Game.Updatable.Remove(this);
            tank.Properties[PropertiesType.IsInvulnerable] = false;
        }

        public void Update()
        {
            if (DateTime.Now - startTime > timeOfAction)
                Dispose();
        }
    }
}

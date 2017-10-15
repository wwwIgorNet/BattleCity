using System;

namespace SuperTank
{
    /// <summary>
    /// Bonus helmet, makes the player's tank invulnerable for a certain time
    /// </summary>
    class HelmetBonus : UpdatableBase
    {
        private Tank tank;
        private static DateTime startTime;
        private TimeSpan timeOfAction;
        
        public HelmetBonus(Tank tank, TimeSpan timeOfAction)
        {
            this.tank = tank;
            this.timeOfAction = timeOfAction;
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

using System;

namespace SuperTank
{
    /// <summary>
    /// Clock bonus. Stops all enemy tanks.
    /// </summary>
    class ClockBonus : UpdatableBase
    {
        private static DateTime startTime;
        private TimeSpan timeOfAction;

        public ClockBonus(TimeSpan timeOfAction)
        {
            this.timeOfAction = timeOfAction;
        }

        public override void Update()
        {
            if (DateTime.Now - startTime < ConfigurationGame.TimeClockBonus)
            {
                SetPause(true);
            }
            else Dispose();
        }
        public override void Start()
        {
            startTime = DateTime.Now;
            base.Start();
            SetPause(true);
        }
        public override void Dispose()
        {
            base.Dispose();
            SetPause(false);
        }

        /// <summary>
        /// Set the IsPause properties of enemy tanks
        /// </summary>
        /// <param name="isPause">Boolean value for all enemy tanks</param>
        private static void SetPause(bool isPause)
        {
            for (int i = 0; i < Scene.Tanks.Count; i++)
            {
                if ((Owner)Scene.Tanks[i].Properties[PropertiesType.Owner] == Owner.Enemy
                    && Scene.Tanks[i].IsPause != isPause)
                {
                    Scene.Tanks[i].IsPause = isPause;
                }
            }
        }
    }
}

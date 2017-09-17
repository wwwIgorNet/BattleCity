using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class Star : UpdatableUnit
    {
        private Tank tank;
        private int delay;

        public Star(int id, int x, int y, int width, int height, TypeUnit type, Tank tank) : base(id, x, y, width, height, type)
        {
            this.tank = tank;
            this.Properties[PropertiesType.Owner] = tank.Properties[PropertiesType.Owner];
        }

        public override void Start()
        {
            base.Start();
            AddToScene();
            Scene.Stars.Add(this);
        }

        public override void Update()
        {
            if (delay == ConfigurationGame.TimeAppearanceOfTank)
                Dispose();
            else delay++;
        }

        public override void Dispose()
        {
            base.Dispose();
            Scene.Stars.Remove(this);
            tank.Start();
            tank.IsParking = !tank.IsParking;
        }
    }
}

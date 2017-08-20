using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class Star : Unit, IUpdatable
    {
        private Tank tank;
        private int delay;

        public Star(int id, int x, int y, int width, int height, TypeUnit type, Tank tank) : base(id, x, y, width, height, type)
        {
            this.tank = tank;
            this.Properties[PropertiesType.Owner] = tank.Properties[PropertiesType.Owner];
        }

        public void Start()
        {
            AddToScene();
            Game.Updatable.Add(this);
            Scene.Tanks.Add(this);
        }

        public void Update()
        {
            if (delay == ConfigurationGame.TimeAppearanceOfTank)
                Dispose();
            else delay++;
        }

        public override void Dispose()
        {
            base.Dispose();
            Game.Updatable.Remove(this);
            Scene.Tanks.Remove(this);
            tank.Start();
            tank.Properties[PropertiesType.IsParking] = !(bool)tank.Properties[PropertiesType.IsParking];
        }
    }
}

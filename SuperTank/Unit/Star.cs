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
        }

        public Star(int x, int y, int width, int height, TypeUnit type, Tank tank) : base(x, y, width, height, type)
        {
            this.tank = tank;
        }

        public Star(TypeUnit type, Tank tank) : this(tank.X, tank.Y, tank.Width, tank.Height, type, tank)
        { }

        public void Start()
        {
            AddToScene();
            Game.Updatable.Add(this);
        }

        public void Update()
        {
            if (delay == ConfigurationGame.TimeAppearanceOfTank)
                Dispose();
            else delay++;
        }

        public override void Dispose()
        {
            Console.WriteLine("Dispose " + tank.ID);
            base.Dispose();
            Game.Updatable.Remove(this);
            tank.Start();
            tank.Properties[PropertiesType.IsParking] = !(bool)tank.Properties[PropertiesType.IsParking];
        }
    }
}

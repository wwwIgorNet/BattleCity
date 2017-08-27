using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class Points : Unit, IUpdatable
    {
        private DateTime starTime;

        public Points(int id, int x, int y, int width, int height, TypeUnit type, int points) : base(id, x, y, width, height, type)
        {
            Properties[PropertiesType.Points] = points;
        }

        public void Start()
        {
            Game.Updatable.Add(this);
            AddToScene();
            starTime = DateTime.Now;
        }

        public void Update()
        {
            if (DateTime.Now - starTime > TimeSpan.FromSeconds(1))
                Dispose();
        }

        public override void Dispose()
        {
            base.Dispose();
            Game.Updatable.Remove(this);
        }
    }
}

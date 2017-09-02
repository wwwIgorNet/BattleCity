using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class Points : UpdatableUnit
    {
        private DateTime starTime;

        public Points(int id, int x, int y, int width, int height, TypeUnit type, int points) : base(id, x, y, width, height, type)
        {
            Properties[PropertiesType.Points] = points;
        }

        public override void Start()
        {
            base.Start();
            AddToScene();
            starTime = DateTime.Now;
        }

        public override void Update()
        {
            if (DateTime.Now - starTime > TimeSpan.FromSeconds(1))
                Dispose();
        }
    }
}

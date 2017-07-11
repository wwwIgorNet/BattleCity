using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    class Shell : Unit, IUpdatable
    {
        private IColision colision;

        public Shell(int x, int y, int width, int height, Direction direction, TypeObjGame type, int velosity) : base(x, y, width, height, direction, type, velosity)
        {
            Level.UpdateObjects.Add(this);
        }

        public IColision Colision { set { colision = value; } }

        public void Update()
        {
            base.Move();
            if(colision != null)
                colision.Update();
        }

        public override void Dispose()
        {
            base.Dispose();
            Level.UpdateObjects.Remove(this);
        }
    }
}

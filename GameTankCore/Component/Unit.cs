using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    public class Unit : ObjGame
    {
        private ICannon Cannon { get; set; }
        private Direction direction;
        private int velosity;

        public Unit(int x, int y, int width, int height, Direction direction, TypeObjGame type, int velosity) : base(x, y, width, height, type)
        {
            this.direction = direction;
            this.velosity = velosity;
        }

        public Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        public int Velosity
        {
            get { return velosity; }
            set { velosity = value; }
        }

        public virtual void Move()
        {
            Move(Velosity);
        }

        public void Move(int velosity)
        {
            switch (direction)
            {
                case Direction.Up:
                    Y -= velosity;
                    break;
                case Direction.Down:
                    Y += velosity;
                    break;
                case Direction.Left:
                    X -= velosity;
                    break;
                case Direction.Right:
                    X += velosity;
                    break;
            }
        }
    }
}

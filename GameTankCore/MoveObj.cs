using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    public class MoveObj : ObjGame
    {
        private ICannon Cannon { get; set; }
        private Direction direction;

        public Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public MoveObj(int x, int y, int width, int height, Direction direction) : base(x, y, width, height)
        {
            this.direction = direction;
        }

        public void Move(int offset)
        {
            switch (direction)
            {
                case Direction.Up:
                    Y -= offset;
                    break;
                case Direction.Down:
                    Y += offset;
                    break;
                case Direction.Left:
                    X -= offset;
                    break;
                case Direction.Right:
                    X += offset;
                    break;
            }
        }
    }
}

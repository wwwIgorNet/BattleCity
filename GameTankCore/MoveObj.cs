using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    class MoveObj : ObjGame, IMoveble
    {
        private ICannon Cannon { get; set; }
        private Direction direction;

        public Direction Direction { get { return direction; } }

        public MoveObj(int x, int y, int width, int height, Direction direction) : base(x, y, width, height)
        {
            this.direction = direction;
        }

        public void MoveUp(int offset)
        {
            direction = Direction.Up;
            Y -= offset;
        }
        public void MoveDown(int offset)
        {
            direction = Direction.Down;
            Y += offset;
        }
        public void MoveLeft(int offset)
        {
            direction = Direction.Left;
            X -= offset;
        }
        public void MoveRight(int offset)
        {
            direction = Direction.Right;
            X += offset;
        }
    }
}

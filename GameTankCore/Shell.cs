using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    class Shell : IUpdatable
    {
        private int velocity;
        private MoveObj movable;

        public Shell(MoveObj movable, int velocity)
        {
            this.velocity = velocity;
            this.movable = movable;
        }

        public void Update()
        {
            switch (movable.Direction)
            {
                case Direction.Up:
                    movable.Direction = Direction.Up;
                    break;
                case Direction.Down:
                    movable.Direction = Direction.Down;
                    break;
                case Direction.Left:
                    movable.Direction = Direction.Left;
                    break;
                case Direction.Right:
                    movable.Direction = Direction.Right;
                    break;
            }
            movable.Move(4);
        }
    }
}

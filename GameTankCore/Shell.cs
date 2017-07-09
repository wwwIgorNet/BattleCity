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
        private IMoveble movable;

        public Shell(IMoveble movable, int velocity)
        {
            this.velocity = velocity;
            this.movable = movable;
        }

        public void Update()
        {
            switch (movable.Direction)
            {
                case Direction.Up:
                    movable.MoveUp(velocity);
                    break;
                case Direction.Down:
                    movable.MoveDown(velocity);
                    break;
                case Direction.Left:
                    movable.MoveLeft(velocity);
                    break;
                case Direction.Right:
                    movable.MoveRight(velocity);
                    break;
            }
        }
    }
}

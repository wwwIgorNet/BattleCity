using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Command
{
    class Move : ICommand
    {
        private Direction direction;
        private Unit unit;
        private int velosity;

        public Move(Unit unit, int velosity, Direction startDirection)
        {
            this.unit = unit;
            this.velosity = velosity;
            this.direction = startDirection;
        }

        public Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public virtual void Execute()
        {
            switch (direction)
            {
                case Direction.Up:
                    unit.Y -= velosity;
                    break;
                case Direction.Right:
                    unit.X += velosity;
                    break;
                case Direction.Down:
                    unit.Y += velosity;
                    break;
                case Direction.Left:
                    unit.X -= velosity;
                    break;
            }
        }
    }
}

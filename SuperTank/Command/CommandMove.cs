using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Command
{
    abstract class CommandMove : BaseDirectionCommand
    {
        public CommandMove(Unit unit)
            : base(unit)
        {
        }

        public int Velosity
        {
            get { return (int)Unit.Properties[PropertiesType.Velosity]; }
            set { Unit.Properties[PropertiesType.Velosity] = value; }
        }

        public virtual void Move(int spead)
        {
            switch (Direction)
            {
                case Direction.Up:
                    Unit.Y -= spead;
                    break;
                case Direction.Right:
                    Unit.X += spead;
                    break;
                case Direction.Down:
                    Unit.Y += spead;
                    break;
                case Direction.Left:
                    Unit.X -= spead;
                    break;
            }
        }
    }
}

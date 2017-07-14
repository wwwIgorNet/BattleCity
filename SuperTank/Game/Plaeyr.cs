using SuperTank.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class Plaeyr : IPlaeyr
    {
        private Unit unit;

        public Plaeyr(Unit unit)
        {
            this.unit = unit;
        }

        public Unit Unit { get { return unit; } }

        public void Update()
        {
            Direction carentDirection;
            if (Keyboard.Right)
                carentDirection = Direction.Right;
            else if (Keyboard.Left)
                carentDirection = Direction.Left;
            else if (Keyboard.Up)
                carentDirection = Direction.Up;
            else if (Keyboard.Down)
                carentDirection = Direction.Down;
            else
            {
                unit.Execute(TypeCommand.Stop);
                return;
            }

            if ((Direction)Unit.Properties[PropertiesType.Direction] != carentDirection)
            {

                switch (carentDirection)
                {
                    case Direction.Up:
                        unit.Execute(TypeCommand.TurnUp);
                        break;
                    case Direction.Right:
                        unit.Execute(TypeCommand.TurnRight);
                        break;
                    case Direction.Down:
                        unit.Execute(TypeCommand.TurnDown);
                        break;
                    case Direction.Left:
                        unit.Execute(TypeCommand.TurnLeft);
                        break;
                }
            }
            else
                unit.Execute(TypeCommand.Move);
        }
    }
}

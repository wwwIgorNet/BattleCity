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
        private Unit tank;

        public Unit Tank
        {
            get { return tank; }
            set
            {
                value.Properties[PropertiesType.Owner] = Owner.Plaeyr;
                tank = value;
            }
        }

        public void Update()
        {
            if (Keyboard.Space)
                Tank.Execute(TypeCommand.Fire);

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
                tank.Execute(TypeCommand.Stop);
                return;
            }

            if ((Direction)Tank.Properties[PropertiesType.Direction] != carentDirection)
            {

                switch (carentDirection)
                {
                    case Direction.Up:
                        tank.Execute(TypeCommand.TurnUp);
                        break;
                    case Direction.Right:
                        tank.Execute(TypeCommand.TurnRight);
                        break;
                    case Direction.Down:
                        tank.Execute(TypeCommand.TurnDown);
                        break;
                    case Direction.Left:
                        tank.Execute(TypeCommand.TurnLeft);
                        break;
                }
            }
            else
                tank.Execute(TypeCommand.Move);
        }
    }
}

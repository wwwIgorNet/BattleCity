using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class PlaeyrDriver : IDriver
    {
        private Tank tank;

        public Tank Tank
        {
            get { return tank; }
            set
            {
                tank = value;
                tank.Properties[PropertiesType.Owner] = Owner.Plaeyr;
            }
        }

        private Direction TankDirection
        {
            get { return (Direction)Tank.Properties[PropertiesType.Direction]; }
        }

        public void Update()
        {
            if ((bool)Tank.Properties[PropertiesType.Detonation])
            {
                Game.Updatable.Remove(this);
                return;
            }

            if (Keyboard.Space)
                tank.Fire();

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
                tank.Stop();
                return;
            }

            // если направление изменилось поворачиваем пока не изменится направление
            if (TankDirection != carentDirection)
                tank.Turn(carentDirection);
            else tank.Move();
        }
    }
}

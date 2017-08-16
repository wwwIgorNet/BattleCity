using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class EnemyDriver : IDriver
    {
        private Tank tank;
        private static Random random = new Random();
        private int i = 0, j = 0;
        Direction carentDirection;

        public Tank Tank
        {
            get { return tank; }
            set
            {
                tank = value;
                tank.Properties[PropertiesType.Owner] = Owner.Enemy;
                i = random.Next(0, 50);
                j = random.Next(0, 100);
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

            if (j == 0)
            {
                j = random.Next(0, 100);
                tank.Fire();
            }
            else j--;

            if (i == 0)
            {
                i = random.Next(6, 50);
                carentDirection = (Direction)random.Next(0, 4);
            }
            else i--;

            // если направление изменилось поворачиваем пока не изменится направление
            if (TankDirection != carentDirection)
                tank.Turn(carentDirection);
            else tank.Move();
        }
    }
}

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
        private int iterationUpdateMove = 0;
        private int iterationUpdateFire = 0;
        Direction carentDirection;

        public Tank Tank
        {
            get { return tank; }
            set
            {
                tank = value;
                tank.Properties[PropertiesType.Owner] = Owner.Enemy;
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

            if (iterationUpdateFire == 0)
            {
                iterationUpdateFire = random.Next(0, 100);
                tank.Fire();
            }
            else iterationUpdateFire--;

            if (iterationUpdateMove == 0)
            {
                iterationUpdateMove = random.Next(6, 50);
                carentDirection = (Direction)random.Next(0, 4);
            }
            else iterationUpdateMove--;

            // если направление изменилось поворачиваем пока не изменится направление
            if (TankDirection != carentDirection)
                tank.Turn(carentDirection);
            else tank.Move();
        }
    }
}

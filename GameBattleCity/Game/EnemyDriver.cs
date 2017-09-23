namespace SuperTank
{
    class EnemyDriver : IDriver
    {
        private Tank tank;
        private int iterationUpdateMove = 0;
        private int iterationUpdateFire = 0;
        Direction carentDirection;

        public EnemyDriver()
        {
            iterationUpdateFire = LevelManager.Random.Next(0, 100);
        }

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
                LevelManager.Updatable.Remove(this);
                return;
            }

            if (iterationUpdateFire == 0)
            {
                iterationUpdateFire = LevelManager.Random.Next(0, 100);
                tank.TryFire();
            }
            else iterationUpdateFire--;

            if (iterationUpdateMove == 0)
            {
                iterationUpdateMove = LevelManager.Random.Next(6, 50);
                carentDirection = (Direction)LevelManager.Random.Next(0, 4);
            }
            else iterationUpdateMove--;

            // если направление изменилось поворачиваем пока не изменится направление
            if (TankDirection != carentDirection)
                tank.Turn(carentDirection);
            else tank.Move();
        }
    }
}

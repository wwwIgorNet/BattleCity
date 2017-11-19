namespace SuperTank
{
    /// <summary>
    /// Driver of the player's tank
    /// </summary>
    class PlayerDriver : IDriver
    {
        private IKeyboard keyboard;
        private Tank tank;

        public PlayerDriver(IKeyboard keyboard)
        {
            this.keyboard = keyboard;
        }

        public Tank Tank
        {
            get { return tank; }
            set
            {
                tank = value;
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

            if (keyboard.Space)
                tank.TryFire();

            Direction carentDirection;
            if (keyboard.Right)
                carentDirection = Direction.Right;
            else if (keyboard.Left)
                carentDirection = Direction.Left;
            else if (keyboard.Up)
                carentDirection = Direction.Up;
            else if (keyboard.Down)
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

namespace SuperTank
{
    /// <summary>
    /// Basic for moving objects
    /// </summary>
    public abstract class MovableUnit : UpdatableUnit
    {
        public MovableUnit(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction) : base(id, x, y, width, height, type)
        {
            Velosity = velosity;
            Direction = direction;
        }

        public int Velosity
        {
            get { return (int)Properties[PropertiesType.Velosity]; }
            set { Properties[PropertiesType.Velosity] = value; }
        }
        public Direction Direction
        {
            get { return (Direction)Properties[PropertiesType.Direction]; }
            set { Properties[PropertiesType.Direction] = value; }
        }

        public void Move(int spead)
        {
            switch (Direction)
            {
                case Direction.Up:
                    Y -= spead;
                    break;
                case Direction.Right:
                    X += spead;
                    break;
                case Direction.Down:
                    Y += spead;
                    break;
                case Direction.Left:
                    X -= spead;
                    break;
            }
        }
    }
}

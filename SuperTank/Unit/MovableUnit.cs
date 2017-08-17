using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public abstract class MovableUnit : Unit
    {
        public MovableUnit(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction) : base(id, x, y, width, height, type)
        {
            Velosity = velosity;
            Direction = direction;
        }

        public MovableUnit(int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction) : base(x, y, width, height, type)
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

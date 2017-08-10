using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Command
{
    class CommandMoveTank : CommandMove
    {
        private IScene scene;

        public CommandMoveTank(Unit unit, IScene scene)
            : base(unit)
        {
            this.scene = scene;
            IsParcing = true;
        }

        private void Move(ref Rectangle rect, int spead)
        {
            switch (Direction)
            {
                case Direction.Up:
                    rect.Y -= spead;
                    break;
                case Direction.Right:
                    rect.X += spead;
                    break;
                case Direction.Down:
                    rect.Y += spead;
                    break;
                case Direction.Left:
                    rect.X -= spead;
                    break;
            }
        }

        private bool IsParcing
        {
            get { return (Boolean)Unit.Properties[PropertiesType.IsStop]; }
            set { Unit.Properties[PropertiesType.IsStop] = value; }
        }

        private void MoveColision()
        {
            Rectangle rect = Unit.BoundingBox;
            Move(ref rect, Velosity);

            while (scene.ColisionBoard(rect)) Move(ref rect, -1);

            foreach (Unit item in scene.Units)
            {
                if (item.BoundingBox.IntersectsWith(rect) && !item.Equals(this.Unit))
                {
                    if (item.Type == TypeUnit.BrickWall || item.Type == TypeUnit.ConcreteWall || item.Type == TypeUnit.Water)
                    {
                        do
                        {
                            Move(ref rect, -1);
                        } while (rect.IntersectsWith(item.BoundingBox));
                    }
                }
            }

            switch (Direction)
            {
                case Direction.Up:
                case Direction.Down:
                    Unit.Y = rect.Y;
                    break;
                case Direction.Right:
                case Direction.Left:
                    Unit.X = rect.X;
                    break;
            }
        }

        public override void Execute()
        {
            Unit.Properties[PropertiesType.IsStop] = false;
            MoveColision();
        }
    }
}

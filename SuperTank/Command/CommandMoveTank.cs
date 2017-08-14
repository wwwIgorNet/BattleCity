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

        protected virtual bool IsParcing
        {
            get { return (bool)Unit.Properties[PropertiesType.IsStop]; }
            set { Unit.Properties[PropertiesType.IsStop] = value; }
        }

        protected virtual bool IsGlide
        {
            get { return (bool)Unit.Properties[PropertiesType.Glide]; }
            set { Unit.Properties[PropertiesType.Glide] = value; }
        }

        private void MoveColision()
        {
            Rectangle rect = Unit.BoundingBox;
            Move(ref rect, Velosity);

            List<Unit> colision = ColisionWithUnit(rect);
            Ofset(ref rect, colision);

            Glide(ref rect, colision);

            while (scene.ColisionBoard(rect)) Move(ref rect, -1);

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

        private void Glide(ref Rectangle rect, List<Unit> colision)
        {
            for (int i = 0; i < colision.Count; i++)
            {
                if (colision[i].Type == TypeUnit.Ice && rect.IntersectsWith(colision[i].BoundingBox))
                {
                    IsGlide = true;
                    Move(ref rect, 2);
                    Ofset(ref rect, colision);
                    return;
                }
            }
            IsGlide = false;
        }

        private void Ofset(ref Rectangle rect, List<Unit> colision)
        {
            for (int i = 0; i < colision.Count; i++)
            {
                if (colision[i].Type == TypeUnit.BrickWall || colision[i].Type == TypeUnit.ConcreteWall || colision[i].Type == TypeUnit.Water)
                    while (rect.IntersectsWith(colision[i].BoundingBox))
                        Move(ref rect, -1);
            }
        }

        private List<Unit> ColisionWithUnit(Rectangle rect)
        {
            List<Unit> res = new List<Unit>(2);
            for (int i = 0; i < scene.Units.Count; i++)
                if (rect.IntersectsWith(scene.Units[i].BoundingBox) && !scene.Units[i].Equals(this.Unit))
                    res.Add(scene.Units[i]);

            return res;
        }

        public override void Execute()
        {
            Unit.Properties[PropertiesType.IsStop] = false;
            MoveColision();
        }
    }
}

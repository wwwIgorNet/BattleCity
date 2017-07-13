using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Command
{
    class MoveTank : MoveCommand
    {
        private IScene scene;

        public MoveTank(Unit unit, IScene scene)
            : base(unit)
        {
            this.scene = scene;
        }

        public override void Move(int spead)
        {
            base.Move(spead);
            MoveColision(Velosity);
        }

        private void MoveColision(int spead)
        {
            if (scene.ColisionBoard(Unit))
            {
                base.Move(-spead);
                MoveColision(spead - 1);
            }
            Unit colision = scene.Colision(Unit);
            if (colision != null && colision.Type != TypeUnit.Shell)
            {
                base.Move(-spead);
                Move(spead - 1, colision);
            }
        }

        private void Move(int spead, Unit colision)
        {
            base.Move(spead);
            if (Unit.BoundingBox.IntersectsWith(colision.BoundingBox))
            {
                base.Move(-spead);
                Move(spead - 1, colision);
            }
        }
    }
}

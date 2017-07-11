using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    class ColisionTank : IColision
    {
        private Unit unit;

        public ColisionTank(Unit unit)
        {
            this.unit = unit;
        }

        public void Update()
        {
            MoveColision(unit.Velosity);
        }

        private void MoveColision(int spead)
        {
            if (Board.ColisionBoard(unit))
            {
                unit.Move(-spead);
                MoveColision(spead - 1);
            }
            ObjGame colision = Board.Colision(unit);
            if (colision != null && colision.Type != TypeObjGame.Shell)
            {
                unit.Move(-spead);
                Move(spead - 1, colision);
            }
        }

        private void Move(int spead, ObjGame colision)
        {
            unit.Move(spead);
            if (unit.BoundingBox.IntersectsWith(colision.BoundingBox))
            {
                unit.Move(-spead);
                Move(spead - 1, colision);
            }
        }
    }
}

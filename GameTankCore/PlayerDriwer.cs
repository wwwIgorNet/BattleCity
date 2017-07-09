using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    class PlayerDriwer : IDriwer
    {
        private int velosity;
        private MoveObj movable;

        public PlayerDriwer(MoveObj movable, int velosity)
        {
            this.movable = movable;
            this.velosity = velosity;
        }

        public void Update()
        {

            if (Keyboard.Up)
                movable.Direction = Direction.Up;
            else if (Keyboard.Down)
                movable.Direction = Direction.Down;
            else if (Keyboard.Left)
                movable.Direction = Direction.Left;
            else if (Keyboard.Right)
                movable.Direction = Direction.Right;
            else
                return;

            Move(velosity);
        }

        private void Move(int spead)
        {
            movable.Move(spead);
            if (Board.ColisionBoard(movable))
            {
                movable.Move(-spead);
                Move(spead - 1);
            }
            ObjGame colision = Board.Colision(movable);
            if (colision != null)
            {
                movable.Move(-spead);
                Move(spead - 1, colision);
            }
        }

        private void Move(int spead, ObjGame colision)
        {
            if (movable.BoundingBox.IntersectsWith(colision.BoundingBox))
            {
                movable.Move(-spead);
                Move(spead - 1);
            }
        }
    }
}

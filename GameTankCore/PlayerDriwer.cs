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
        private IMoveble movable;

        public PlayerDriwer(IMoveble movable, int velosity)
        {
            this.movable = movable;
            this.velosity = velosity;
        }

        public void Update()
        {
            if (Keyboard.Up)
                movable.MoveUp(velosity);
            if (Keyboard.Down)
                movable.MoveDown(velosity);
            if (Keyboard.Left)
                movable.MoveLeft(velosity);
            if (Keyboard.Right)
                movable.MoveRight(velosity);
        }
    }
}

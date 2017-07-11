using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    class PlayerDriwer : IDriwer
    {
        private Unit unit;

        public PlayerDriwer(Unit unit)
        {
            this.unit = unit;
        }

        public void Update()
        {
            if (Keyboard.Up)
                unit.Direction = Direction.Up;
            else if (Keyboard.Down)
                unit.Direction = Direction.Down;
            else if (Keyboard.Left)
                unit.Direction = Direction.Left;
            else if (Keyboard.Right)
                unit.Direction = Direction.Right;
            else
                return;

            unit.Move();
        }
    }
}

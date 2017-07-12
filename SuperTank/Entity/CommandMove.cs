using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class CommandMove : ICommand
    {
        private Unit unit;
        private int velosity;

        public CommandMove(Unit unit, int velosity)
        {
            this.unit = unit;
            this.velosity = velosity;
        }

        public void Execute()
        {
            if (Keyboard.Left)
                unit.X -= velosity;
            else if (Keyboard.Right)
                unit.X += velosity;
            else if (Keyboard.Up)
                unit.Y -= velosity;
            else if (Keyboard.Down)
                unit.Y += velosity;
        }
    }
}

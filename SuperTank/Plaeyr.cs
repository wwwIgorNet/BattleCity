using SuperTank.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class Plaeyr
    {
        private Unit unit;

        public Plaeyr(Unit unit)
        {
            this.unit = unit;
        }

        public void Update()
        {
            if (Keyboard.Right)
                unit.Execute(TypeCommand.MoveRight);
            else if (Keyboard.Left)
                unit.Execute(TypeCommand.MoveLeft);
            else if (Keyboard.Up)
                unit.Execute(TypeCommand.MoveUp);
            else if (Keyboard.Down)
                unit.Execute(TypeCommand.MoveDown);
        }
    }
}

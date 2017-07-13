using SuperTank.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class Plaeyr : IPlaeyr
    {
        private Unit unit;

        public Plaeyr(Unit unit)
        {
            this.unit = unit;
        }

        public Unit Unit { get { return unit; } }

        public void Update()
        {
            if (Keyboard.Right)
                unit.Execute(TypeCommand.TurnRight);
            else if (Keyboard.Left)
                unit.Execute(TypeCommand.TurnLeft);
            else if (Keyboard.Up)
                unit.Execute(TypeCommand.TurnUp);
            else if (Keyboard.Down)
                unit.Execute(TypeCommand.TurnDown);
            else
            {
                unit.Execute(TypeCommand.Stop);
                return;
            }
            
            unit.Execute(TypeCommand.Move);
        }
    }
}

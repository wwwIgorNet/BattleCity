using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Command
{
    class MoveTank : Move
    {
        public MoveTank(Unit unit, int velosity, Direction startDirection)
            : base(unit, velosity, startDirection)
        {
        }

        public override void Execute()
        {
            base.Execute();
        }
    }
}

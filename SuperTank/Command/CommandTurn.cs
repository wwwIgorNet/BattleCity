using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Command
{
    class CommandTurn : BaseOffsetCommand
    {
        private Direction dir;

        public CommandTurn(Unit unit, Direction direction) : base(unit)
        {
            dir = direction;
        }

        public override void Execute()
        {
            if (OffsetToBorderTile())
                Direction = dir;
        }
    }
}

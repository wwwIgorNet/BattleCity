using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Command
{
    class CommandStop : BaseOffsetCommand
    {

        public CommandStop(Unit unit) : base(unit)
        {
        }

        public override void Execute()
        {
            if (!(bool)Unit.Properties[PropertiesType.IsStop])
                Unit.Properties[PropertiesType.IsStop] = OffsetToBorderTile();
        }
    }
}

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
            unit.Execute(TypeCommand.Move);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Updatable
{
    class UpdatableShell : IUpdatable
    {
        private Unit unit;

        public UpdatableShell(Unit unit)
        {
            this.unit = unit;
        }

        public void Update()
        {
            unit.Execute(Command.TypeCommand.Move);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperTank.Audio;

namespace SuperTank
{
    class TwoShellTank : TankPlaetr
    {
        public TwoShellTank(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, IDriver driver, TypeUnit typeShell, int velosityShell, ISoundGame soundGame, Plaeyr plaeyr) : base(id, x, y, width, height, type, velosity, direction, driver, typeShell, velosityShell, soundGame, plaeyr)
        {
        }

        protected override void StartNewShell(int x, int y, TypeUnit type, int velosityShell)
        {
            base.StartNewShell(x, y, TypeUnit.SimpleShell, velosityShell);
            base.StartNewShell(x, y, type, velosityShell - 2);
        }
    }
}

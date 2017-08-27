using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperTank.Audio;

namespace SuperTank
{
    class SimpleShell : PlaeyrShell
    {
        public SimpleShell(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, Tank ownerTank, ISoundGame soundGame) : base(id, x, y, width, height, type, velosity, direction, ownerTank, soundGame)
        {
        }

        public override void Dispose()
        {
            base.Dispose();
            Tank.Shell = this;
        }
    }
}
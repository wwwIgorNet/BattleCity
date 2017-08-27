using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperTank.Audio;

namespace SuperTank
{
    class ConcreteWallShell : PlaeyrShell
    {
        public ConcreteWallShell(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, Tank ownerTank, ISoundGame soundGame) : base(id, x, y, width, height, type, velosity, direction, ownerTank, soundGame)
        {
        }

        protected override void Detonation(Unit item, bool removeItem)
        {
            if (item != null && item.Type == TypeUnit.ConcreteWall)
                base.Detonation(item, true);
            else base.Detonation(item, removeItem);
        }
    }
}

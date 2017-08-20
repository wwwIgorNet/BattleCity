using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class PlaeyrShell : Shell
    {
        private ISoundGame soundGame;

        public PlaeyrShell(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, Tank ownerTank, ISoundGame soundGame) : base(id, x, y, width, height, type, velosity, direction, ownerTank)
        {
            this.soundGame = soundGame;
        }

        protected override void Detonation(Unit item, bool removeItem)
        {
            base.Detonation(item, removeItem);
            if (item == null) soundGame.DetonationShell();
            else
            {
                switch (item.Type)
                {//todo
                    case TypeUnit.SmallTankPlaeyr:
                    case TypeUnit.LightTankPlaeyr:
                    case TypeUnit.MediumTankPlaeyr:
                    case TypeUnit.HeavyTankPlaeyr:

                    case TypeUnit.PainTank:
                    case TypeUnit.ArmoredPersonnelCarrierTank:
                    case TypeUnit.QuickFireTank:
                    case TypeUnit.ArmoredTank:
                        soundGame.BigDetonation();
                        break;
                    case TypeUnit.BrickWall:
                        soundGame.DetonationBrickWall();
                        break;
                    case TypeUnit.ConcreteWall:
                        soundGame.DetonationShell();
                        break;
                }
            }
        }
    }
}

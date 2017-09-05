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

        public PlaeyrShell(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, TankPlaetr ownerTank, ISoundGame soundGame) : base(id, x, y, width, height, type, velosity, direction, ownerTank)
        {
            this.soundGame = soundGame;
            OwnerTank = ownerTank;
        }

        protected TankPlaetr OwnerTank { get; private set; }

        protected override void Detonation(Unit item, bool removeItem)
        {
            base.Detonation(item, removeItem);
            if (item == null) soundGame.DetonationShell();
            else
            {
                int points = 0;
                switch (item.Type)
                {//todo
                    case TypeUnit.SmallTankPlaeyr:
                    case TypeUnit.LightTankPlaeyr:
                    case TypeUnit.MediumTankPlaeyr:
                    case TypeUnit.HeavyTankPlaeyr:
                        break;

                    case TypeUnit.PlainTank:
                        points = -200;
                        goto case TypeUnit.QuickFireTank;
                        break;
                    case TypeUnit.ArmoredPersonnelCarrierTank:
                        points = -100;
                        goto case TypeUnit.QuickFireTank;
                        break;
                    case TypeUnit.QuickFireTank:
                        points += 300;
                        FactoryUnit.CreatePoints(item.X, item.Y, TypeUnit.Points, points).Start();
                        OwnerTank.OwnerPlaeyr.Points += points;
                        OwnerTank.OwnerPlaeyr.DestroyedTanks[item.Type]++;
                        soundGame.DetonationTank();
                        break;
                    case TypeUnit.ArmoredTank:
                        if (removeItem)
                        {
                            FactoryUnit.CreatePoints(item.X, item.Y, TypeUnit.Points, 400).Start();
                            OwnerTank.OwnerPlaeyr.Points += 400;
                            OwnerTank.OwnerPlaeyr.DestroyedTanks[item.Type]++;
                            soundGame.DetonationTank();
                        }
                        else soundGame.DetonationShell();
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

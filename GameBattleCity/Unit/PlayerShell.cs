using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class PlayerShell : Shell
    {
        private ISoundGame soundGame;

        public PlayerShell(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, TankPlayer ownerTank, ISoundGame soundGame) : base(id, x, y, width, height, type, velosity, direction, ownerTank)
        {
            this.soundGame = soundGame;
            OwnerTank = ownerTank;
        }

        protected TankPlayer OwnerTank { get; private set; }

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
                        break;

                    case TypeUnit.PlainTank:
                        DestroyTank(item, ConfigurationGame.GetCountPoints(item.Type));
                        break;
                    case TypeUnit.ArmoredPersonnelCarrierTank:
                        DestroyTank(item, ConfigurationGame.GetCountPoints(item.Type));
                        break;
                    case TypeUnit.QuickFireTank:
                        DestroyTank(item, ConfigurationGame.GetCountPoints(item.Type));
                        break;
                    case TypeUnit.ArmoredTank:
                        if (removeItem) DestroyTank(item, ConfigurationGame.GetCountPoints(item.Type));
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

        private void DestroyTank(Unit item, int points)
        {
            FactoryUnit.CreatePoints(item.X, item.Y, TypeUnit.Points, points).Start();
            OwnerTank.OwnerPlaeyr.Points += points;
            OwnerTank.OwnerPlaeyr.DestroyedTanks[item.Type]++;
            soundGame.DetonationTank();
            if ((bool)item.Properties[PropertiesType.IsBonusTank])
                soundGame.NewBonus();
        }
    }
}

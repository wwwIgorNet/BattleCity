using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    static class FactoryUnit
    {
        public static Unit CreateUnit(int x, int y, TypeUnit type)
        {
            return FactoryUnit.CreateUnit(x, y, ConfigurationGame.WidthTile, ConfigurationGame.HeightTile, type);
        }

        public static Unit CreateUnit(int x, int y, int width, int height, TypeUnit type)
        {
            return new Unit(NextID, x, y, width, height, type);
        }

        public static Tank CreateTank(int x, int y, TypeUnit type, Direction direction, IDriver driver)
        {
            Tank tank = null;
            switch (type)
            {
                case TypeUnit.PainTank:
                    tank = new Tank(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, ConfigurationGame.VelostyPlainTank, direction, driver, TypeUnit.Shell, ConfigurationGame.VelosityShellPlainTank);
                    break;
                case TypeUnit.ArmoredPersonnelCarrierTank:
                    tank = new Tank(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, ConfigurationGame.VelosityArmoredPersonnelCarrierTank, direction, driver, TypeUnit.Shell, ConfigurationGame.VelosityShellArmoredPersonnelCarrierTank);
                    break;
                case TypeUnit.QuickFireTank:
                    tank = new Tank(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, ConfigurationGame.VelosityQuickFireTank, direction, driver, TypeUnit.Shell, ConfigurationGame.VelosityShellQuickFireTank);
                    break;
                case TypeUnit.ArmoredTank:
                    tank = new Tank(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, ConfigurationGame.VelosityArmoredTank, direction, driver, TypeUnit.Shell, ConfigurationGame.VelosityShellArmoredTank);
                    tank.Properties[PropertiesType.NumberOfHits] = 0;
                    break;
            }
            return tank;
        }

        public static Bonus CreateBonus(int x, int y, int width, int height, TypeUnit type)
        {
            return new Bonus(NextID, x, y, width, height, type);
        }

        internal static Unit CreateEagle(int x, int y, ISoundGame soundGame)
        {
            Unit eagle = new Eagle(NextID, x, y, ConfigurationGame.WidthTile * 2, ConfigurationGame.HeightTile * 2, TypeUnit.Eagle, soundGame);
            return eagle;
        }

        internal static Tank CreateBonusTank(int x, int y, TypeUnit type, Direction direction, IDriver driver)
        {
            Tank tank = null;
            switch (type)
            {
                case TypeUnit.PainTank:
                    tank = new BonusTank(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, ConfigurationGame.VelostyPlainTank, direction, driver, TypeUnit.Shell, ConfigurationGame.VelosityShellPlainTank);
                    break;
                case TypeUnit.ArmoredPersonnelCarrierTank:
                    tank = new BonusTank(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, ConfigurationGame.VelosityArmoredPersonnelCarrierTank, direction, driver, TypeUnit.Shell, ConfigurationGame.VelosityShellArmoredPersonnelCarrierTank);
                    break;
                case TypeUnit.QuickFireTank:
                    tank = new BonusTank(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, ConfigurationGame.VelosityQuickFireTank, direction, driver, TypeUnit.Shell, ConfigurationGame.VelosityQuickFireTank);
                    break;
                case TypeUnit.ArmoredTank:
                    tank = new BonusTank(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, ConfigurationGame.VelosityArmoredTank, direction, driver, TypeUnit.Shell, ConfigurationGame.VelosityShellArmoredTank);
                    tank.Properties[PropertiesType.NumberOfHits] = 0;
                    break;
            }
            return tank;
        }

        public static Tank CreateTank(int x, int y, TypeUnit type, Direction direction, IDriver driver, ISoundGame soundGame)
        {
            Tank tank = null;
            switch (type)
            {
                case TypeUnit.SmallTankPlaeyr:
                case TypeUnit.LightTankPlaeyr:
                case TypeUnit.MediumTankPlaeyr:
                case TypeUnit.HeavyTankPlaeyr:
                    tank = new TankPlaetr(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, ConfigurationGame.VelosityHeavyTank, direction, driver, TypeUnit.Shell, ConfigurationGame.VelosityShellHeavyTank, soundGame);
                    break;
            }
            return tank;
        }

        public static Star CreateStar(TypeUnit type, Tank tank)
        {
            return new Star(NextID, tank.X, tank.Y, tank.Width, tank.Height, type, tank);
        }

        public static Shell CreateShell(int x, int y, TypeUnit type, Direction direction, int velosityShell, Tank ownerTank)
        {
            return new Shell(NextID, x, y, ConfigurationGame.WidthShell, ConfigurationGame.HeightShell, type, velosityShell, direction, ownerTank);
        }

        public static Shell CreateShell(int x, int y, TypeUnit type, Direction direction, Tank ownerTank, ISoundGame soundGame)
        {
            return new PlaeyrShell(NextID, x, y, ConfigurationGame.WidthShell, ConfigurationGame.HeightShell, type, ConfigurationGame.VelosityShellPlainTank, direction, ownerTank, soundGame);
        }

        public static BigDetonation CreateBigDetonation(Unit unit, TypeUnit type)
        {
            return new BigDetonation(NextID, unit.X, unit.Y, unit.Width, unit.Height, type);
        }

        private static int NextID { get { return Unit.NextID; } }
    }
}

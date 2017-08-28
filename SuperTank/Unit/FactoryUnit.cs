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

        public static Tank CreateTank(int x, int y, TypeUnit type, Direction direction, IDriver driver, bool isBonusTank)
        {
            Dictionary<PropertiesType, object> prop = null;
            int velosity = 0;
            TypeUnit typeShell = 0;
            int velosityShell = 0;
            Tank tank = null;
            switch (type)
            {
                case TypeUnit.PlainTank:
                    velosity = ConfigurationGame.VelostyPlainTank;
                    typeShell = TypeUnit.Shell;
                    velosityShell = ConfigurationGame.VelosityShellPlainTank;
                    break;
                case TypeUnit.ArmoredPersonnelCarrierTank:
                    velosity = ConfigurationGame.VelosityArmoredPersonnelCarrierTank;
                    typeShell = TypeUnit.Shell;
                    velosityShell = ConfigurationGame.VelosityShellArmoredPersonnelCarrierTank;
                    break;
                case TypeUnit.QuickFireTank:
                    velosity = ConfigurationGame.VelosityQuickFireTank;
                    typeShell = TypeUnit.Shell;
                    velosityShell = ConfigurationGame.VelosityShellQuickFireTank;
                    break;
                case TypeUnit.ArmoredTank:
                    velosity = ConfigurationGame.VelosityArmoredTank;
                    typeShell = TypeUnit.Shell;
                    velosityShell = ConfigurationGame.VelosityShellArmoredTank;
                    prop = new Dictionary<PropertiesType, object>() { [PropertiesType.NumberOfHits] = 0 };
                    break;
            }
            if (velosity > 0)
            {
                if (!isBonusTank)
                {
                    tank = new Tank(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, velosity, direction, driver, typeShell, velosityShell);
                }
                else
                {
                    tank = new BonusTank(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, velosity, direction, driver, typeShell, velosityShell);
                }
                if (prop != null)
                {
                    foreach (var item in prop)
                    {
                        tank.Properties[item.Key] = item.Value;
                    }
                }

                return tank;
            }
            return null;
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

        public static TankPlaetr CreateTank(int x, int y, TypeUnit type, Direction direction, IDriver driver, ISoundGame soundGame, Plaeyr plaeyr)
        {
            TankPlaetr tank = null;
            switch (type)
            {
                case TypeUnit.SmallTankPlaeyr:
                    tank = new TankPlaetr(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, ConfigurationGame.VelositySmallTank, direction, driver, TypeUnit.Shell, ConfigurationGame.VelosityShellSmallTank, soundGame, plaeyr);
                    break;
                case TypeUnit.LightTankPlaeyr:
                    tank = new TankPlaetr(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, ConfigurationGame.VelosityLightTank, direction, driver, TypeUnit.Shell, ConfigurationGame.VelosityShellLightTank, soundGame, plaeyr);
                    break;
                case TypeUnit.MediumTankPlaeyr:
                    tank = new TwoShellTank(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, ConfigurationGame.VelosityMediumTank, direction, driver, TypeUnit.Shell, ConfigurationGame.VelosityShellMediumTank, soundGame, plaeyr);
                    break;
                case TypeUnit.HeavyTankPlaeyr:
                    tank = new TwoShellTank(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, ConfigurationGame.VelosityHeavyTank, direction, driver, TypeUnit.ConcreteWallShell, ConfigurationGame.VelosityShellHeavyTank, soundGame, plaeyr);
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
            Shell shell = new Shell(NextID, x, y, ConfigurationGame.WidthShell, ConfigurationGame.HeightShell, type, velosityShell, direction, ownerTank);
            return shell;
        }

        public static Shell CreateShell(int x, int y, TypeUnit type, Direction direction, int velosityShell, TankPlaetr ownerTank, ISoundGame soundGame)
        {
            if(type == TypeUnit.Shell)
                return new PlaeyrShell(NextID, x, y, ConfigurationGame.WidthShell, ConfigurationGame.HeightShell, type, velosityShell, direction, ownerTank, soundGame);
            if(type == TypeUnit.ConcreteWallShell)
                return new ConcreteWallShell(NextID, x, y, ConfigurationGame.WidthShell, ConfigurationGame.HeightShell, type, velosityShell, direction, ownerTank, soundGame);

            return null;
        }

        public static BigDetonation CreateBigDetonation(Unit unit, TypeUnit type)
        {
            return new BigDetonation(NextID, unit.X, unit.Y, unit.Width, unit.Height, type);
        }

        public static Points CreatePoints(int x, int y, TypeUnit type, int points)
        {
            return new Points(NextID, x, y, ConfigurationGame.WidthTile * 2, ConfigurationGame.HeightTile * 2, type, points);
        }

        private static int NextID { get { return Unit.NextID; } }
    }
}

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
                case TypeUnit.ArmoredPersonnelCarrierTank:
                case TypeUnit.QuickFireTank:
                case TypeUnit.ArmoredTank:
                    tank = new Tank(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, ConfigurationGame.VelostyPlainTank, direction, driver, TypeUnit.Shell);
                    break;
            }
            return tank;
        }
        public static Tank CreateTank(int x, int y, TypeUnit type, Direction direction, IDriver driver, ISoundGame soundGame)
        {
            Tank tank = null;
            switch (type)
            {
                case TypeUnit.PainTankPlaeyr:
                case TypeUnit.ArmoredPersonnelCarrierTankPlaeyr:
                case TypeUnit.QuickFireTankPlaeyr:
                case TypeUnit.ArmoredTankPlaeyr:
                    tank = new TankPlaetr(NextID, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, type, ConfigurationGame.VelostyPlainTank, direction, driver, TypeUnit.Shell, soundGame);
                    break;
            }
            return tank;
        }

        public static Star CreateStar(TypeUnit type, Tank tank)
        {
            return new Star(NextID, tank.X, tank.Y, tank.Width, tank.Height, type, tank);
        }

        public static Shell CreateShell(int x, int y, TypeUnit type, Direction direction, Tank ownerTank)
        {
            return new Shell(NextID, x, y, ConfigurationGame.WidthShell, ConfigurationGame.HeightShell, type, ConfigurationGame.VelostyShellPlainTank, direction, ownerTank);
        }

        public static Shell CreateShell(int x, int y, TypeUnit type, Direction direction, Tank ownerTank, ISoundGame soundGame)
        {
            return new PlaeyrShell(NextID, x, y, ConfigurationGame.WidthShell, ConfigurationGame.HeightShell, type, ConfigurationGame.VelostyShellPlainTank, direction, ownerTank, soundGame);
        }

        public static BigDetonation CreateBigDetonation(Unit unit, TypeUnit type)
        {
            return new BigDetonation(NextID, unit.X, unit.Y, unit.Width, unit.Height, type);
        }

        private static int NextID { get { return Unit.NextID; } }
    }
}

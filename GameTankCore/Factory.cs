using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    static class Factory
    {
        public static Tank CreateTank(TypeObjGame type)
        {
            Tank tank = null;
            switch(type)
            {
                case TypeObjGame.PlainUserTank:
                    tank = new Tank(9 * Configuration.WidthTile, Configuration.HeightBoard - Configuration.HeigthTank, Configuration.WidthTank, Configuration.HeigthTank, Direction.Up, type, Configuration.VelostyPlainUserTank);
                    tank.Driver = new PlayerDriwer(tank);
                    tank.Cannon = new Cannon(tank, Configuration.VelostyShellPlainUserTank);
                    tank.Shooter = new PlayerShooter();
                    tank.Colision = new ColisionTank(tank);
                    break;
            }
            return tank;
        }

        public static ObjGame CreateBrickWall(int x, int y)
        {
            ObjGame obj = new ObjGame(x, y, Configuration.WidthTile, Configuration.HeightTile, TypeObjGame.BrickWall);
            return obj;
        }

        public static Shell CreateShell(int x, int y, Direction direction, int velosity)
        {
            Shell shell = new Shell(x, y, Configuration.WidthShell, Configuration.HeightShell, direction, TypeObjGame.Shell, velosity);
            shell.Colision = new ColisionShell(shell);
            return shell;
        }
    }
}

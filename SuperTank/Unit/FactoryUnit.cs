using SuperTank.Command;
using SuperTank.Updatable;
using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class FactoryUnit : IFactoryUnit
    {
        private IScene scene;
        private int prevId = 0;

        public FactoryUnit(IScene scene)
        {
            this.scene = scene;
        }

        public Unit Create(int x, int y,TypeUnit type)
        {
            Unit res = null;
            switch (type)
            {
                case TypeUnit.PlainTank:
                    res = CreatePlainTank(x, y);
                    break;
                case TypeUnit.Shell:
                    res = CreateShell(x, y);
                    break;
                case TypeUnit.BrickWall:
                    res = CreateBrickWall(x, y);
                    break;
            }
            return res;
        }

        private Unit CreateBrickWall(int x, int y)
        {
            Unit res = new Unit(prevId++, x, y, ConfigurationGame.WidthTile, ConfigurationGame.HeightTile, TypeUnit.BrickWall);
            return res;
        }

        private Unit CreateShell(int x, int y)
        {
            Unit shell = new Unit(prevId++, x, y, ConfigurationGame.WidthShell, ConfigurationGame.HeightShell, TypeUnit.Shell);
            return shell;
        }

        private Unit CreatePlainTank(int x, int y)
        {
            Unit tank = new Unit(prevId++, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, TypeUnit.PlainTank);
            tank.Properties[PropertiesType.Velosity] = ConfigurationGame.VelostyPlainTank;
            tank.Properties[PropertiesType.Direction] = Direction.Up;
            tank.Properties[PropertiesType.IsStop] = true;
            tank.Commands.AddCommand(TypeCommand.TurnDown, new CommandTurn(tank, Direction.Down).Execute);
            tank.Commands.AddCommand(TypeCommand.TurnUp, new CommandTurn(tank, Direction.Up).Execute);
            tank.Commands.AddCommand(TypeCommand.TurnLeft, new CommandTurn(tank, Direction.Left).Execute);
            tank.Commands.AddCommand(TypeCommand.TurnRight, new CommandTurn(tank, Direction.Right).Execute);
            tank.Commands.AddCommand(TypeCommand.Stop, new CommandStop(tank).Execute);
            tank.Commands.AddCommand(TypeCommand.Move, new CommandMoveTank(tank, scene).Execute);
            tank.Commands.AddCommand(TypeCommand.Fire, new CommandFire(tank, this, scene, ConfigurationGame.VelostyShellPlainTank).Execute);
            return tank;
        }
    }
}

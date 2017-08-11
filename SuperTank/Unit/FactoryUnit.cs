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
            switch (type)
            {
                case TypeUnit.PlainTank:
                    return CreatePlainTank(x, y);
                case TypeUnit.Shell:
                    return CreateShell(x, y);
                case TypeUnit.BrickWall:
                    return CreateBrickWall(x, y);
                case TypeUnit.ConcreteWall:
                    return CreateConcreteWall(x, y);
                case TypeUnit.Water:
                    return CreateWater(x, y);
                case TypeUnit.Forest:
                    return CreateForest(x, y);
                case TypeUnit.Ice:
                    return CreateIce(x, y);
                case TypeUnit.Eagle:
                    return CreateEagle(x, y);
            }
            return null;
        }

        private Unit CreateEagle(int x, int y)
        {
            return new Unit(prevId++, x, y, ConfigurationGame.WidthTile, ConfigurationGame.HeightTile, TypeUnit.Eagle);
        }

        private Unit CreateIce(int x, int y)
        {
            return new Unit(prevId++, x, y, ConfigurationGame.WidthTile, ConfigurationGame.HeightTile, TypeUnit.Ice);
        }

        private Unit CreateForest(int x, int y)
        {
            return new Unit(prevId++, x, y, ConfigurationGame.WidthTile, ConfigurationGame.HeightTile, TypeUnit.Forest);
        }

        private Unit CreateWater(int x, int y)
        {
            return new Unit(prevId++, x, y, ConfigurationGame.WidthTile, ConfigurationGame.HeightTile, TypeUnit.Water);
        }

        private Unit CreateConcreteWall(int x, int y)
        {
            return new Unit(prevId++, x, y, ConfigurationGame.WidthTile, ConfigurationGame.HeightTile, TypeUnit.ConcreteWall);
        }

        private Unit CreateBrickWall(int x, int y)
        {
            Unit res = new Unit(prevId++, x, y, ConfigurationGame.WidthTile, ConfigurationGame.HeightTile, TypeUnit.BrickWall);
            return res;
        }

        private Unit CreateShell(int x, int y)
        {
            return new Unit(prevId++, x, y, ConfigurationGame.WidthShell, ConfigurationGame.HeightShell, TypeUnit.Shell);
        }

        private Unit CreatePlainTank(int x, int y)
        {
            Unit tank = new Unit(prevId++, x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, TypeUnit.PlainTank);
            tank.Properties[PropertiesType.Velosity] = ConfigurationGame.VelostyPlainTank;
            tank.Properties[PropertiesType.Direction] = Direction.Up;
            tank.Properties[PropertiesType.IsStop] = true;
            tank.Properties[PropertiesType.Glide] = false;
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

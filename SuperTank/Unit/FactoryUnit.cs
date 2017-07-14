using SuperTank.Command;
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
                    break;
                case TypeUnit.BrickWall:
                    res = new Unit(x, y, ConfigurationGame.WidthTile, ConfigurationGame.HeightTile, type);
                    break;
            }
            return res;
        }

        private Unit CreatePlainTank(int x, int y)
        {
            Invoker invoker = new Invoker();
            Unit tank = new Unit(x, y, ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank, TypeUnit.PlainTank);
            tank.Properties[PropertiesType.Velosity] = ConfigurationGame.VelostyPlainTank;
            tank.Properties[PropertiesType.Direction] = Direction.Up;
            invoker.AddCommand(TypeCommand.TurnDown, new CommandTurn(tank, Direction.Down).Execute);
            invoker.AddCommand(TypeCommand.TurnUp, new CommandTurn(tank, Direction.Up).Execute);
            invoker.AddCommand(TypeCommand.TurnLeft, new CommandTurn(tank, Direction.Left).Execute);
            invoker.AddCommand(TypeCommand.TurnRight, new CommandTurn(tank, Direction.Right).Execute);
            invoker.AddCommand(TypeCommand.Stop, new CommandStop(tank).Execute);
            invoker.AddCommand(TypeCommand.Move, new MoveTank(tank, scene).Execute);
            tank.Execute(TypeCommand.TurnUp);
            tank.Commands = invoker;
            return tank;
        }
    }
}

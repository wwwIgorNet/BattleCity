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
                    res = new Unit(x, y, ConfigurationGme.WidthTile, ConfigurationGme.HeightTile, type);
                    break;
            }
            return res;
        }

        private Unit CreatePlainTank(int x, int y)
        {
            Invoker invoker = new Invoker();
            Unit tank = new Unit(x, y, ConfigurationGme.WidthTank, ConfigurationGme.HeigthTank, TypeUnit.PlainTank);
            tank.Properties[PropertiesType.Velosity] = ConfigurationGme.VelostyPlainTank;
            tank.Properties[PropertiesType.Direction] = Direction.Up;
            invoker.AddCommand(TypeCommand.TurnDown, () => tank.Properties[PropertiesType.Direction] = Direction.Down);
            invoker.AddCommand(TypeCommand.TurnUp, 
                () => tank.Properties[PropertiesType.Direction] = Direction.Up);
            invoker.AddCommand(TypeCommand.TurnLeft, () => tank.Properties[PropertiesType.Direction] = Direction.Left);
            invoker.AddCommand(TypeCommand.TurnRight, () => tank.Properties[PropertiesType.Direction] = Direction.Right);
            invoker.AddCommand(TypeCommand.Move, new MoveTank(tank, scene).Execute);
            tank.Execute(TypeCommand.TurnUp);
            tank.Commands = invoker;
            return tank;
        }
    }
}

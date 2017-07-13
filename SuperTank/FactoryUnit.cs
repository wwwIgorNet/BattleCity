using SuperTank.Command;
using SuperTank.Interface;
using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class FactoryUnit : IFactoryUnit
    {
        public Unit Create(TypeUnit type)
        {
            Unit res = null;
            if (type == TypeUnit.PlainTank)
            {
                Invoker invoker = new Invoker();
                Unit tank = new Unit(20, 300, Configuration.WidthTank, Configuration.HeigthTank, TypeUnit.PlainTank, invoker);
                tank.Properties[PropertiesType.Velosity] = Configuration.VelostyPlainTank;
                invoker.AddCommand(TypeCommand.TurnDown, () => tank.Properties[PropertiesType.Direction] = Direction.Down);
                invoker.AddCommand(TypeCommand.TurnUp, () => tank.Properties[PropertiesType.Direction] = Direction.Up);
                invoker.AddCommand(TypeCommand.TurnLeft, () => tank.Properties[PropertiesType.Direction] = Direction.Left);
                invoker.AddCommand(TypeCommand.TurnRight, () => tank.Properties[PropertiesType.Direction] = Direction.Right);
                invoker.AddCommand(TypeCommand.Move, new MoveTank(tank).Execute);
                tank.Execute(TypeCommand.TurnUp);
                res = tank;
            }
            return res;
        }
    }
}

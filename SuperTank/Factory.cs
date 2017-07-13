using SuperTank.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class Factory : IFactory
    {
        private IRender render;

        public Factory(IRender render)
        {
            this.render = render;
        }

        public Unit CreateUnit(TypeUnit type)
        {
            Unit res = null;
            if (type == TypeUnit.PlainTank)
            {
                Invoker invoker = new Invoker();
                res = new Unit(20, 300, Configuration.WidthTank, Configuration.HeigthTank, TypeUnit.PlainTank, invoker);
                invoker.AddCommand(TypeCommand.MoveUp, new MoveTank(res, Configuration.VelostyPlainTank, Direction.Up));
                invoker.AddCommand(TypeCommand.MoveDown, new MoveTank(res, Configuration.VelostyPlainTank, Direction.Down));
                invoker.AddCommand(TypeCommand.MoveLeft, new MoveTank(res, Configuration.VelostyPlainTank, Direction.Left));
                invoker.AddCommand(TypeCommand.MoveRight, new MoveTank(res, Configuration.VelostyPlainTank, Direction.Right));
                render.Drowable.Add(new ViewUnit(res, Images.PlainTankUp));
            }
            return res;
        }
    }
}

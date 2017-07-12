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
                invoker.AddCommand(TypeCommand.Move, new CommandMove(res, 3));
                render.Drowable.Add(new ViewUnit(res, Images.PlainTankUp));
            }
            return res;
        }
    }
}

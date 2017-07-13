using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperTank.View;
using SuperTank.Command;
using System.Drawing;

namespace SuperTank
{
    class FactoryViewUnit : IFactoryViewUnit
    {
        public BaseView Create(Unit unit)
        {
            BaseView res = null;
            switch (unit.Type)
            {
                case TypeUnit.PlainTank:
                    Dictionary<Direction, Image> imges = new Dictionary<Direction, Image>
                    {
                        {Direction.Up, Images.PlainTankUp },
                        {Direction.Down, Images.PlainTankDown },
                        {Direction.Left, Images.PlainTankLeft },
                        {Direction.Right, Images.PlainTankRight }
                    };
                    res = new ViewDirectionUnit(unit, imges);
                    break;
                case TypeUnit.Shell:
                    break;
                case TypeUnit.BrickWall:
                    res = new ViewUnit(unit, Images.BrickWall);
                    break;
                default:
                    return null;
            }
            return res;
        }
    }
}

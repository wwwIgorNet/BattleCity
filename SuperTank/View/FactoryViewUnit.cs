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
            Dictionary<Direction, Image> images;
            switch (unit.Type)
            {
                case TypeUnit.PlainTank:
                    images = new Dictionary<Direction, Image>
                    {
                        {Direction.Up, Images.PlainTankUp },
                        {Direction.Down, Images.PlainTankDown },
                        {Direction.Left, Images.PlainTankLeft },
                        {Direction.Right, Images.PlainTankRight }
                    };
                    res = new ViewDirectionUnit(unit, images);
                    break;
                case TypeUnit.Shell:
                    images = new Dictionary<Direction, Image>
                    {
                        {Direction.Up, Images.ShellUp },
                        {Direction.Down, Images.ShellDown },
                        {Direction.Left, Images.ShellLeft },
                        {Direction.Right, Images.ShellRight }
                    };
                    res = new ViewDirectionUnit(unit, images);
                    break;
                case TypeUnit.BrickWall:
                    res = new ViewUnit(unit, Images.BrickWall);
                    break;
            }
            return res;
        }
    }
}

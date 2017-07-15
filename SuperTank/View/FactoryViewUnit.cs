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
        public BaseView Create(int id, float x, float y, TypeUnit typeUnit)
        {
            BaseView res = null;
            switch (typeUnit)
            {
                case TypeUnit.PlainTank:
                    res = CreateViewPlainTank(id, x, y);
                    break;
                case TypeUnit.Shell:
                    res = CreateViewShell(id, x, y);
                    break;
                case TypeUnit.BrickWall:
                    res = new ViewUnit(id, x, y, ConfigurationView.WidthTile, ConfigurationView.HeightTile, 0, Images.BrickWall);
                    break;
            }
            return res;
        }

        private BaseView CreateViewShell(int id, float x, float y)
        {
            Dictionary<Direction, Image>  images = new Dictionary<Direction, Image>
                    {
                        {Direction.Up, Images.ShellUp },
                        {Direction.Down, Images.ShellDown },
                        {Direction.Left, Images.ShellLeft },
                        {Direction.Right, Images.ShellRight }
                    };
            return new ViewDirectionUnit(id, x, y, ConfigurationView.WidthShell, ConfigurationView.HeightShell, 0, images);
        }
        private BaseView CreateViewPlainTank(int id, float x, float y)
        {
            Dictionary<Direction, Image> images = new Dictionary<Direction, Image>
                    {
                        {Direction.Up, Images.PlainTankUp },
                        {Direction.Down, Images.PlainTankDown },
                        {Direction.Left, Images.PlainTankLeft },
                        {Direction.Right, Images.PlainTankRight }
                    };
            return new ViewDirectionUnit(id, x, y, ConfigurationView.WidthTank, ConfigurationView.HeigthTank, 0, images);
        }
    }
}

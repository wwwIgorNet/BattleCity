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

            Image[] detonation = new Image[]
            {
                Images.ShellDetonation1,
                Images.ShellDetonation1,
                Images.ShellDetonation2,
                Images.ShellDetonation2,
                Images.ShellDetonation3,
                Images.ShellDetonation3,
                Images.ShellDetonation4,
                Images.ShellDetonation4
            };
            return new ViewShell(id, x, y, ConfigurationView.WidthShell, ConfigurationView.HeightShell, 0, images, detonation);
        }
        private BaseView CreateViewPlainTank(int id, float x, float y)
        {
            Dictionary<Direction, Image[]> images = new Dictionary<Direction, Image[]>
                    {
                        {Direction.Up, new []
                        { Images.PlainTankUp, Images.PlainTankUp2 } },
                        {Direction.Down, new []
                        { Images.PlainTankDown, Images.PlainTankDown2 } },
                        {Direction.Left, new [] 
                        { Images.PlainTankLeft, Images.PlainTankLeft2 } },
                        {Direction.Right,new []
                        { Images.PlainTankRight, Images.PlainTankRight2 } }
                    };
            return new ViewAnimationTankUnit(id, x, y, ConfigurationView.WidthTank, ConfigurationView.HeigthTank, 0, images, 2);
        }
    }
}

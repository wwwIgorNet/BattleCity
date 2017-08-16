using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperTank.View;
using System.Drawing;

namespace SuperTank
{
    class FactoryViewUnit : IFactoryViewUnit
    {
        public BaseView Create(int id, float x, float y, TypeUnit typeUnit, Dictionary<PropertiesType, object> properties)
        {
            switch (typeUnit)
            {
                case TypeUnit.Star:
                    return CreateStar(id, x, y);
                case TypeUnit.PainTank:
                    return CreateViewPlainTank(id, x, y, properties);
                case TypeUnit.Shell:
                    return CreateViewShell(id, x, y, properties);
                case TypeUnit.BrickWall:
                    return new ViewUnit(id, x, y, ConfigurationView.WidthTile, ConfigurationView.HeightTile, 0, Images.BrickWall);
                case TypeUnit.ConcreteWall:
                    return new ViewUnit(id, x, y, ConfigurationView.WidthTile, ConfigurationView.HeightTile, 0, Images.ConcreteWall);
                case TypeUnit.Water:
                    return CreateViewWater(id, x, y);
                case TypeUnit.Forest:
                    return new ViewUnit(id, x, y, ConfigurationView.WidthTile, ConfigurationView.HeightTile, 1, Images.Forest);
                case TypeUnit.Ice:
                    return new ViewUnit(id, x, y, ConfigurationView.WidthTile, ConfigurationView.HeightTile, -1, Images.Ice);
                case TypeUnit.Eagle:
                    return new ViewUnit(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, -1, Images.Eagle);
            }
            return null;
        }

        private static BaseView CreateStar(int id, float x, float y)
        {
            Image[] images = new Image[]
            {
                        Images.Star1,
                        Images.Star2,
                        Images.Star3,
                        Images.Star4,
                        Images.Star3,
                        Images.Star2,
            };
            return new ViewAnimationUnit(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 0, images, 4);
        }

        private BaseView CreateViewWater(int id, float x, float y)
        {
            Image[] images = { Images.Water_1, Images.Water_2, Images.Water_3 };

            BaseView res = new ViewAnimationUnit(id, x, y, ConfigurationView.WidthTile, ConfigurationView.HeightTile, 0, images, 10);
            return res;
        }

        private BaseView CreateViewShell(int id, float x, float y, Dictionary<PropertiesType, object> properties)
        {
            Image image = null;
            switch ((Direction)properties[PropertiesType.Direction])
            {
                case Direction.Up:
                    image = Images.ShellUp;
                    break;
                case Direction.Right:
                    image = Images.ShellRight;
                    break;
                case Direction.Down:
                    image = Images.ShellDown;
                    break;
                case Direction.Left:
                    image = Images.ShellLeft;
                    break;
            }

            Image[] detonation = new Image[]
            {
                Images.ShellDetonation1,
                Images.ShellDetonation1,
                Images.ShellDetonation1,
                Images.ShellDetonation2,
                Images.ShellDetonation2,
                Images.ShellDetonation3
            };
            BaseView res = new ViewShell(id, x, y, ConfigurationView.WidthShell, ConfigurationView.HeightShell, 2, image, detonation);
            res.Properties[PropertiesType.Direction] = properties[PropertiesType.Direction];
            return res;
        }
        private BaseView CreateViewPlainTank(int id, float x, float y, Dictionary<PropertiesType, object> properties)
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
            BaseView res = new ViewAnimationTankUnit(id, x, y, ConfigurationView.WidthTank, ConfigurationView.HeigthTank, 0, images, 2);
            res.Properties[PropertiesType.Direction] = properties[PropertiesType.Direction];
            res.Properties[PropertiesType.IsParking] = properties[PropertiesType.IsParking];
            return res;
        }
    }
}

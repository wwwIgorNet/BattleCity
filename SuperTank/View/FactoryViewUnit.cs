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
                // todo
                case TypeUnit.Star:
                    return CreateStar(id, x, y);

                case TypeUnit.SmallTankPlaeyr:
                case TypeUnit.LightTankPlaeyr:
                case TypeUnit.MediumTankPlaeyr:
                case TypeUnit.HeavyTankPlaeyr:

                case TypeUnit.PainTank:
                case TypeUnit.ArmoredPersonnelCarrierTank:
                case TypeUnit.QuickFireTank:
                    return CreateTank(id, x, y, properties, Images.GetImgesForTank(typeUnit));
                case TypeUnit.ArmoredTank:
                    BaseView res = new ViewAnimationArmoredTank(id, x, y, ConfigurationView.WidthTank, ConfigurationView.HeigthTank, 0, 2, Images.GetImages(Images.Enemy.ArmoredTank),
                        Images.GetImages(Images.Enemy.ArmoredTankGreen),
                        Images.GetImages(Images.Enemy.ArmoredTankYellow));
                    res.Properties[PropertiesType.Direction] = properties[PropertiesType.Direction];
                    res.Properties[PropertiesType.IsParking] = properties[PropertiesType.IsParking];
                    res.Properties[PropertiesType.NumberOfHits] = properties[PropertiesType.NumberOfHits];
                    return res;
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
                    BaseView eagle = new ViewEagle(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, -1, Images.Eagle, Images.Eagle2);
                    eagle.Properties[PropertiesType.Detonation] = properties[PropertiesType.Detonation];
                    return eagle;
                case TypeUnit.BigDetonation:
                    return CreateBigDetonation(id, x, y);
            }
            return null;
        }

        private BaseView CreateBigDetonation(int id, float x, float y)
        {
            Image[] img = new Image[]
               {
                Images.ShellDetonation1,
                Images.ShellDetonation2,
                Images.ShellDetonation3,
                Images.ShellDetonationBig,
                Images.ShellDetonationBig,
                Images.ShellDetonationBig,
                Images.ShellDetonationBig2,
                Images.ShellDetonationBig2,
                Images.ShellDetonationBig2,
                Images.ShellDetonation3,
                Images.ShellDetonation2,
                Images.ShellDetonation1

               };
            BigDetonationView detonation = new BigDetonationView(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 0, img, 2);
            return detonation;
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

        private static BaseView CreateTank(int id, float x, float y, Dictionary<PropertiesType, object> properties, Dictionary<Direction, Image[]> images)
        {
            BaseView res = new ViewAnimationTank(id, x, y, ConfigurationView.WidthTank, ConfigurationView.HeigthTank, 0, images, 2);
            res.Properties[PropertiesType.Direction] = properties[PropertiesType.Direction];
            res.Properties[PropertiesType.IsParking] = properties[PropertiesType.IsParking];
            return res;
        }
    }
}

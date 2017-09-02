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
            BaseView resView = null;
            switch (typeUnit)
            { // todo
                case TypeUnit.Star:
                    resView = new ViewAnimationUnit(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 0, GetImgForStar(), 4);
                    break;
                case TypeUnit.SmallTankPlaeyr:
                case TypeUnit.LightTankPlaeyr:
                case TypeUnit.MediumTankPlaeyr:
                case TypeUnit.HeavyTankPlaeyr:

                case TypeUnit.PlainTank:
                case TypeUnit.ArmoredPersonnelCarrierTank:
                case TypeUnit.QuickFireTank:
                    if ((bool)properties[PropertiesType.IsBonusTank])
                    {
                        resView = new ViewBonusTank(id, x, y,
                            ConfigurationView.WidthTank,
                            ConfigurationView.HeigthTank,
                            ConfigurationView.ZIndexTank,
                            Images.GetImgesForTank(typeUnit),
                            Images.GetImgesForRedTank(typeUnit));
                    }
                    else
                    {
                        resView = new ViewAnimationTank(id, x, y,
                            ConfigurationView.WidthTank,
                            ConfigurationView.HeigthTank,
                            ConfigurationView.ZIndexTank,
                            Images.GetImgesForTank(typeUnit));
                    }
                    break;
                case TypeUnit.ArmoredTank:
                    if ((bool)properties[PropertiesType.IsBonusTank])
                    {
                        resView = new ViewBonusArmoredTank(id, x, y,
                            ConfigurationView.WidthTank,
                            ConfigurationView.HeigthTank,
                            ConfigurationView.ZIndexTank,
                            Images.GetImages(Images.Enemy.ArmoredTank),
                            Images.GetImages(Images.Enemy.ArmoredTankGreen),
                            Images.GetImages(Images.Enemy.ArmoredTankYellow),
                            Images.GetImages(Images.TankRed.ArmoredTank));
                    }
                    else
                    {
                        resView = new ViewAnimationArmoredTank(id, x, y,
                            ConfigurationView.WidthTank,
                            ConfigurationView.HeigthTank,
                            ConfigurationView.ZIndexTank,
                            Images.GetImages(Images.Enemy.ArmoredTank),
                            Images.GetImages(Images.Enemy.ArmoredTankGreen),
                            Images.GetImages(Images.Enemy.ArmoredTankYellow));
                    }
                    break;


                case TypeUnit.BrickWall:
                    resView = new ViewUnit(id, x, y, ConfigurationView.WidthTile, ConfigurationView.HeightTile, 0, Images.BrickWall);
                    break;
                case TypeUnit.ConcreteWall:
                    resView = new ViewUnit(id, x, y, ConfigurationView.WidthTile, ConfigurationView.HeightTile, 0, Images.ConcreteWall);
                    break;
                case TypeUnit.Water:
                    resView = new ViewAnimationUnit(id, x, y, ConfigurationView.WidthTile, ConfigurationView.HeightTile, 0, GetImgForWoter(), 10);
                    break;
                case TypeUnit.Forest:
                    resView = new ViewUnit(id, x, y, ConfigurationView.WidthTile, ConfigurationView.HeightTile, 10, Images.Forest);
                    break;
                case TypeUnit.Ice:
                    resView = new ViewUnit(id, x, y, ConfigurationView.WidthTile, ConfigurationView.HeightTile, -1, Images.Ice);
                    break;


                case TypeUnit.Eagle:
                    resView = new ViewEagle(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 0, Images.Eagle, Images.Eagle2);
                    break;


                case TypeUnit.Shell:
                case TypeUnit.ConcreteWallShell:
                    resView = new ViewShell(id, x, y, ConfigurationView.WidthShell, ConfigurationView.HeightShell, 8, GetImgForShell((Direction)properties[PropertiesType.Direction]), GetImgForShellDetonation());
                    break;
                case TypeUnit.BigDetonation:
                    resView = new BigDetonationView(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 12, GetImgBigDetonation(), 2);
                    break;


                case TypeUnit.Clock:
                    resView = new ViewUnit(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 12, Images.Bonus.Clock);
                    break;
                case TypeUnit.Grenade:
                    return new ViewUnit(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 12, Images.Bonus.Grenade);
                case TypeUnit.Helmet:
                    resView = new ViewUnit(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 12, Images.Bonus.Helmet);
                    break;
                case TypeUnit.Pistol:
                    resView = new ViewUnit(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 12, Images.Bonus.Pistol);
                    break;
                case TypeUnit.Shovel:
                    resView = new ViewUnit(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 12, Images.Bonus.Shovel);
                    break;
                case TypeUnit.StarMedal:
                    resView = new ViewUnit(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 12, Images.Bonus.StarMedal);
                    break;
                case TypeUnit.Tank:
                    resView = new ViewUnit(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 12, Images.Bonus.Tank);
                    break;

                case TypeUnit.Points:
                    resView = new ViewPonts(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 12);
                    break;
            }
            if (properties != null)
                foreach (var item in properties)
                    resView.Properties.Add(item.Key, item.Value);
            return resView;
        }

        private static Image[] GetImgForStar()
        {
            return new Image[] {
                Images.Star1,
                Images.Star2,
                Images.Star3,
                Images.Star4,
                Images.Star3,
                Images.Star2
            };
        }
        private static Image[] GetImgBigDetonation()
        {
            return new Image[] {
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
        }
        private static Image[] GetImgForWoter()
        {
            return new Image[] {
                Images.Water_1,
                Images.Water_2,
                Images.Water_3
            };
        }
        private static Image[] GetImgForShellDetonation()
        {
            return new Image[]
            {
                Images.ShellDetonation1,
                Images.ShellDetonation1,
                Images.ShellDetonation1,
                Images.ShellDetonation2,
                Images.ShellDetonation2,
                Images.ShellDetonation3
            };
        }
        private static Image GetImgForShell(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return Images.ShellUp;
                case Direction.Right:
                    return Images.ShellRight;
                case Direction.Down:
                    return Images.ShellDown;
                case Direction.Left:
                    return Images.ShellLeft;
            }

            return null;
        }
    }
}

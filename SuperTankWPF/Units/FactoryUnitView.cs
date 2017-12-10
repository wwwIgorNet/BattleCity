using System.Collections.Generic;
using SuperTank.View;
using System.Drawing;
using SuperTankWPF.Units;
using SuperTankWPF;
using SuperTankWPF.Model;
using System.Windows.Media;
using SuperTank;
using System.Windows.Media.Imaging;

namespace SuperTankWPF.Units
{
    /// <summary>
    /// Creates objects to display units
    /// </summary>
    class FactoryUnitView : IFactoryUnitView
    {
        private IImageSourceStor imgStor;

        public FactoryUnitView(IImageSourceStor imgStor)
        {
            this.imgStor = imgStor;
        }

        public UnitView Create(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties)
        {
            UnitView resView = null;
            switch (typeUnit)
            { // todo
                case TypeUnit.Star:
                    resView = new AnimationView(4, GetImgForStar(), true);
                    break;
                case TypeUnit.SmallTankPlaeyr:
                case TypeUnit.LightTankPlaeyr:
                case TypeUnit.MediumTankPlaeyr:
                case TypeUnit.HeavyTankPlaeyr:

                case TypeUnit.PlainTank:
                case TypeUnit.ArmoredPersonnelCarrierTank:
                case TypeUnit.QuickFireTank:
                case TypeUnit.ArmoredTank:
                    resView = new TankView((Direction)properties[PropertiesType.Direction], imgStor.GetImgesForTank(typeUnit), 6) { ZIndex = ConfigurationView.ZIndexTank };
                    break;
                //            if ((Owner)properties[PropertiesType.Owner] == Owner.IIPlayer)
                //            {
                //                resView = new ViewAnimationTank(id, x, y,
                //                   ConfigurationView.WidthTank,
                //                   ConfigurationView.HeightTank,
                //                   ConfigurationView.ZIndexTank,
                //                   Images.GetImgesForTankIIPlayer(typeUnit));
                //            }
                //            else
                //            {
                //                resView = new ViewAnimationTank(id, x, y,
                //                    ConfigurationView.WidthTank,
                //                    ConfigurationView.HeightTank,
                //                    ConfigurationView.ZIndexTank,
                //                    Images.GetImgesForTank(typeUnit));
                //            }
                //            break;

                //resView = new UnitView() { Source = imgStor.Enemy.PlainTank.Down1 };
                //break;
                //            if ((bool)properties[PropertiesType.IsBonusTank])
                //            {
                //                resView = new ViewBonusTank(id, x, y,
                //                    ConfigurationView.WidthTank,
                //                    ConfigurationView.HeightTank,
                //                    ConfigurationView.ZIndexTank,
                //                    Images.GetImgesForTank(typeUnit),
                //                    Images.GetImgesForRedTank(typeUnit));
                //            }
                //            else
                //            {
                //                resView = new ViewAnimationTank(id, x, y,
                //                    ConfigurationView.WidthTank,
                //                    ConfigurationView.HeightTank,
                //ConfigurationView.ZIndexTank,
                //                Images.GetImgesForTank(typeUnit));
                //            }
                //            break;
                //        case TypeUnit.ArmoredTank:
                //            if ((bool)properties[PropertiesType.IsBonusTank])
                //            {
                //                resView = new ViewBonusArmoredTank(id, x, y,
                //                    ConfigurationView.WidthTank,
                //                    ConfigurationView.HeightTank,
                //                    ConfigurationView.ZIndexTank,
                //                    Images.GetImages(Images.Enemy.ArmoredTank),
                //                    Images.GetImages(Images.Enemy.ArmoredTankGreen),
                //                    Images.GetImages(Images.Enemy.ArmoredTankYellow),
                //                    Images.GetImages(Images.TankRed.ArmoredTank));
                //            }
                //            else
                //            {
                //                resView = new ViewAnimationArmoredTank(id, x, y,
                //                    ConfigurationView.WidthTank,
                //                    ConfigurationView.HeightTank,
                //                    ConfigurationView.ZIndexTank,
                //                    Images.GetImages(Images.Enemy.ArmoredTank),
                //                    Images.GetImages(Images.Enemy.ArmoredTankGreen),
                //                    Images.GetImages(Images.Enemy.ArmoredTankYellow));
                //            }
                //            break;

                case TypeUnit.BrickWall:
                    resView = new UnitView() { Source = imgStor.BrickWall };
                    break;
                case TypeUnit.ConcreteWall:
                    resView = new UnitView() { Source = imgStor.ConcreteWall };
                    break;
                case TypeUnit.Water:
                    resView = new UnitView() { Source = imgStor.Water_1 };
                    //resView = new ViewAnimationUnit(id, x, y, 0, GetImgForWoter(), 10);
                    break;
                case TypeUnit.Forest:
                    resView = new UnitView() { Source = imgStor.Forest, ZIndex = 10 };
                    break;
                case TypeUnit.Ice:
                    resView = new UnitView() { Source = imgStor.Ice, ZIndex = -1 };
                    break;


                case TypeUnit.Eagle:
                    resView = new UnitView() { Source = imgStor.Eagle };
                    break;
                //            resView = new ViewEagle(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 0, Images.Eagle, Images.Eagle2);
                //            break;


                case TypeUnit.Shell:
                case TypeUnit.ConcreteWallShell:
                    resView = new UnitView() { Source = GetImgForShell((Direction)properties[PropertiesType.Direction]), ZIndex = 8 };
                    break;

                //        case TypeUnit.ConcreteWallShell:
                //            resView = new ViewShell(id, x, y, ConfigurationView.WidthShell, ConfigurationView.HeightShell, 8, GetImgForShell((Direction)properties[PropertiesType.Direction]), GetImgForShellDetonation());
                //            break;
                //        case TypeUnit.BigDetonation:
                //            resView = new BigDetonationView(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 12, GetImgBigDetonation(), 2);
                //            break;


                case TypeUnit.Clock:
                    resView = CreateBonusView(imgStor.Bonuses.Clock);
                    break;
                case TypeUnit.Grenade:
                    resView = CreateBonusView(imgStor.Bonuses.Grenade);
                    break;
                case TypeUnit.Helmet:
                    resView = CreateBonusView(imgStor.Bonuses.Helmet);
                    break;
                case TypeUnit.Pistol:
                    resView = CreateBonusView(imgStor.Bonuses.Pistol);
                    break;
                case TypeUnit.Shovel:
                    resView = CreateBonusView(imgStor.Bonuses.Shovel);
                    break;
                case TypeUnit.StarMedal:
                    resView = CreateBonusView(imgStor.Bonuses.StarMedal);
                    break;
                case TypeUnit.Tank:
                    resView = CreateBonusView(imgStor.Bonuses.Tank);
                    break;

                case TypeUnit.Points:
                    resView = new PointsView((int)properties[PropertiesType.Points])
                    {
                        Width = ConfigurationWPF.WidthTile * 2,
                        Height = ConfigurationWPF.HeightTile,
                        ZIndex = 12
                    };
                    break;

                default:
                    resView = new UnitView() { Source = imgStor.Invulnerable1 };
                    break;
            }

            resView.ID = id;
            resView.TypeUnit = typeUnit;
            resView.X = x;
            resView.Y = y;

            return resView;
        }

        private UnitView CreateBonusView(ImageSource img)
        {
            return new AnimationView(10, new ImageSource[] { img, null }, true) { ZIndex = 12 };
        }
        private ImageSource[] GetImgForStar()
        {
            return new ImageSource[] {
                imgStor.Star1,
                imgStor.Star2,
                imgStor.Star3,
                imgStor.Star4,
                imgStor.Star3,
                imgStor.Star2
            };
        }
        //private static Image[] GetImgBigDetonation()
        //{
        //    return new Image[] {
        //        Images.ShellDetonation1,
        //        Images.ShellDetonation2,
        //        Images.ShellDetonation3,
        //        Images.ShellDetonationBig,
        //        Images.ShellDetonationBig,
        //        Images.ShellDetonationBig,
        //        Images.ShellDetonationBig2,
        //        Images.ShellDetonationBig2,
        //        Images.ShellDetonationBig2,
        //        Images.ShellDetonation3,
        //        Images.ShellDetonation2,
        //        Images.ShellDetonation1
        //       };
        //}
        //private static Image[] GetImgForWoter()
        //{
        //    return new Image[] {
        //        Images.Water_1,
        //        Images.Water_2,
        //        Images.Water_3
        //    };
        //}
        //private static Image[] GetImgForShellDetonation()
        //{
        //    return new Image[]
        //    {
        //        Images.ShellDetonation1,
        //        Images.ShellDetonation1,
        //        Images.ShellDetonation1,
        //        Images.ShellDetonation2,
        //        Images.ShellDetonation2,
        //        Images.ShellDetonation3
        //    };
        //}
        private ImageSource GetImgForShell(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return imgStor.ShellUp;
                case Direction.Right:
                    return imgStor.ShellRight;
                case Direction.Down:
                    return imgStor.ShellDown;
                case Direction.Left:
                    return imgStor.ShellLeft;
            }

            return null;
        }
    }
}

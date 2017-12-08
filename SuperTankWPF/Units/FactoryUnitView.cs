using System.Collections.Generic;
using SuperTank.View;
using System.Drawing;
using SuperTankWPF.Units;
using SuperTankWPF;
using SuperTankWPF.Model;
using System.Windows.Media;
using SuperTank;

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

        public UnitView Create(float x, float y, TypeUnit typeUnit, Dictionary<PropertiesType, object> properties)
        {
            return null;
        }

        public UnitView Create(UnitViewModel unitViewModel)
        {
            UnitView resView = null;
            switch (unitViewModel.TypeUnit)
            { // todo
                case TypeUnit.Star:
                    resView = new UnitView() { Source = imgStor.Star1 };
                    break;
                case TypeUnit.SmallTankPlaeyr:
                case TypeUnit.LightTankPlaeyr:
                case TypeUnit.MediumTankPlaeyr:
                case TypeUnit.HeavyTankPlaeyr:
                    resView = new UnitView() { Source = imgStor.Plaeyr.SmallTank.Up1 };
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

                case TypeUnit.PlainTank:
                case TypeUnit.ArmoredPersonnelCarrierTank:
                case TypeUnit.QuickFireTank:
                case TypeUnit.ArmoredTank:
                    resView = new UnitView() { Source = imgStor.Enemy.PlainTank.Down1 };
                    break;
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
                //                    ConfigurationView.ZIndexTank,
                //                    Images.GetImgesForTank(typeUnit));
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
                    resView = new UnitView() { Source = GetImgForShell((Direction)unitViewModel.Properties[PropertiesType.Direction]), ZIndex = 8 };
                    break;

                //        case TypeUnit.ConcreteWallShell:
                //            resView = new ViewShell(id, x, y, ConfigurationView.WidthShell, ConfigurationView.HeightShell, 8, GetImgForShell((Direction)properties[PropertiesType.Direction]), GetImgForShellDetonation());
                //            break;
                //        case TypeUnit.BigDetonation:
                //            resView = new BigDetonationView(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 12, GetImgBigDetonation(), 2);
                //            break;


                //        case TypeUnit.Clock:
                //            resView = CreateBonusView(id, x, y, Images.Bonus.Clock);
                //            break;
                //        case TypeUnit.Grenade:
                //            resView = CreateBonusView(id, x, y, Images.Bonus.Grenade);
                //            break;
                //        case TypeUnit.Helmet:
                //            resView = CreateBonusView(id, x, y, Images.Bonus.Helmet);
                //            break;
                //        case TypeUnit.Pistol:
                //            resView = CreateBonusView(id, x, y, Images.Bonus.Pistol);
                //            break;
                //        case TypeUnit.Shovel:
                //            resView = CreateBonusView(id, x, y, Images.Bonus.Shovel);
                //            break;
                //        case TypeUnit.StarMedal:
                //            resView = CreateBonusView(id, x, y, Images.Bonus.StarMedal);
                //            break;
                //        case TypeUnit.Tank:
                //            resView = CreateBonusView(id, x, y, Images.Bonus.Tank);
                //            break;

                //        case TypeUnit.Points:
                //            resView = new ViewPoints(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 12);
                //            break;

                default:
                    resView = new UnitView() { Source = imgStor.Invulnerable1};
                    break;
            }
            //    if (properties != null)
            //        foreach (var item in properties)
            //            resView.Properties.Add(item.Key, item.Value);
            
            resView.DataContext = unitViewModel;
            unitViewModel.View = resView;
            return resView;
        }

        //private static BaseView CreateBonusView(int id, float x, float y, Image img)
        //{
        //    return new ViewAnimationUnit(id, x, y, ConfigurationView.WidthTile * 2, ConfigurationView.HeightTile * 2, 12, new Image[] { img, Images.BlankImage }, 6);
        //}
        //private static Image[] GetImgForStar()
        //{
        //    return new Image[] {
        //        Images.Star1,
        //        Images.Star2,
        //        Images.Star3,
        //        Images.Star4,
        //        Images.Star3,
        //        Images.Star2
        //    };
        //}
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

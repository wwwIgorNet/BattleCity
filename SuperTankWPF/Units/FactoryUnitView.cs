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
            {
                case TypeUnit.Star:
                    resView = new AnimationView(4, GetImgForStar(), true);
                    break;
                case TypeUnit.SmallTankPlaeyr:
                case TypeUnit.LightTankPlaeyr:
                case TypeUnit.MediumTankPlaeyr:
                case TypeUnit.HeavyTankPlaeyr:
                    Dictionary<Direction, ImageSource[]> imgTankPlayer;
                    ImageSource[] Invulnerable = new[] { imgStor.Invulnerable1, imgStor.Invulnerable2 };
                    if ((Owner)properties[PropertiesType.Owner] == Owner.IIPlayer)
                        imgTankPlayer = imgStor.GetImgesForTankIIPlayer(typeUnit);
                    else
                        imgTankPlayer = imgStor.GetImgesForTank(typeUnit);

                    resView = new PlayerTankView((Direction)properties[PropertiesType.Direction], imgTankPlayer, Invulnerable, 6)
                    {
                        IsInvulnerable = (bool)properties[PropertiesType.IsInvulnerable],
                        ZIndex = ConfigurationView.ZIndexTank
                    };
                    break;

                case TypeUnit.PlainTank:
                case TypeUnit.ArmoredPersonnelCarrierTank:
                case TypeUnit.QuickFireTank:
                    if ((bool)properties[PropertiesType.IsBonusTank])
                    {
                        resView = new BonusTankView((Direction)properties[PropertiesType.Direction], imgStor.GetImgesForTank(typeUnit), imgStor.GetImgesForRedTank(typeUnit), 6);
                    }
                    else
                    {
                        resView = new TankView((Direction)properties[PropertiesType.Direction], imgStor.GetImgesForTank(typeUnit), 6);
                    }
                    resView.ZIndex = ConfigurationView.ZIndexTank;
                    break;

                case TypeUnit.ArmoredTank:
                    if ((bool)properties[PropertiesType.IsBonusTank])
                        resView = new BonusArmoredTankView(
                            (Direction)properties[PropertiesType.Direction],
                            imgStor.GetImages(imgStor.Enemy.ArmoredTank),
                            imgStor.GetImages(imgStor.Enemy.ArmoredTankGreen),
                            imgStor.GetImages(imgStor.Enemy.ArmoredTankYellow),
                            imgStor.GetImages(imgStor.TankRed.ArmoredTank), 6);
                    else
                        resView = new ArmoredTankView(
                            (Direction)properties[PropertiesType.Direction],
                            imgStor.GetImages(imgStor.Enemy.ArmoredTank),
                            imgStor.GetImages(imgStor.Enemy.ArmoredTankGreen),
                            imgStor.GetImages(imgStor.Enemy.ArmoredTankYellow), 6);

                    resView.ZIndex = ConfigurationView.ZIndexTank;
                    break;


                case TypeUnit.BrickWall:
                    resView = new UnitView() { Source = imgStor.BrickWall };
                    break;
                case TypeUnit.ConcreteWall:
                    resView = new UnitView() { Source = imgStor.ConcreteWall };
                    break;
                case TypeUnit.Water:
                    resView = new UnitView() { Source = imgStor.Water_1 };
                    resView = new AnimationView(10, GetImgForWoter(), true);
                    break;
                case TypeUnit.Forest:
                    resView = new UnitView() { Source = imgStor.Forest, ZIndex = 10 };
                    break;
                case TypeUnit.Ice:
                    resView = new UnitView() { Source = imgStor.Ice, ZIndex = -1 };
                    break;


                case TypeUnit.Eagle:
                    resView = new EagleView(imgStor.Eagle2) { Source = imgStor.Eagle };
                    break;

                case TypeUnit.Shell:
                case TypeUnit.ConcreteWallShell:
                    resView = new ShellView(1, GetImgForShellDetonation()) { Source = GetImgForShell((Direction)properties[PropertiesType.Direction]), ZIndex = 8 };
                    break;
                case TypeUnit.BigDetonation:
                    resView = new CentrAnimationView(1, GetImgBigDetonation(), true) { ZIndex = 12 };
                    break;


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
        private ImageSource[] GetImgBigDetonation()
        {
            return new ImageSource[] {
                imgStor.ShellDetonation1,
                imgStor.ShellDetonation2,
                imgStor.ShellDetonation3,
                imgStor.ShellDetonationBig,
                imgStor.ShellDetonationBig,
                imgStor.ShellDetonationBig,
                imgStor.ShellDetonationBig2,
                imgStor.ShellDetonationBig2,
                imgStor.ShellDetonationBig2,
                imgStor.ShellDetonation3,
                imgStor.ShellDetonation2,
                imgStor.ShellDetonation1
               };
        }
        private ImageSource[] GetImgForWoter()
        {
            return new ImageSource[] {
                imgStor.Water_1,
                imgStor.Water_2,
                imgStor.Water_3
            };
        }
        private ImageSource[] GetImgForShellDetonation()
        {
            return new ImageSource[]
            {
                imgStor.ShellDetonation1,
                imgStor.ShellDetonation1,
                imgStor.ShellDetonation1,
                imgStor.ShellDetonation2,
                imgStor.ShellDetonation2,
                imgStor.ShellDetonation3
            };
        }
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

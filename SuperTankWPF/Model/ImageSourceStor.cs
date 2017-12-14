using SuperTank;
using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SuperTankWPF.Model
{
    /// <summary>
    /// Caches and simplifies access to images
    /// </summary>
    public class ImageSourceStor : IImageSourceStor
    {
        private TankPlaeyr tankPlaeyr = new TankPlaeyr(@"Tank\Plaeyr\SmallTank\", @"Tank\Plaeyr\LightTank\", @"Tank\Plaeyr\MediumTank\", @"Tank\Plaeyr\HeavyTank\");
        private TankPlaeyr tankPlaeyr2 = new TankPlaeyr(@"Tank\Plaeyr2\SmallTank\", @"Tank\Plaeyr2\LightTank\", @"Tank\Plaeyr2\MediumTank\", @"Tank\Plaeyr2\HeavyTank\");
        private TankEnemyExtend tankEnemy = new TankEnemyExtend(@"Tank\Enemy\PlainTank\", @"Tank\Enemy\ArmoredPersonnelCarrier\", @"Tank\Enemy\QuickFireTank\", @"Tank\Enemy\ArmoredTank\", @"Tank\Enemy\ArmoredTankGreen\", @"Tank\Enemy\ArmoredTankYellow\");
        private TankEnemy tankRed = new TankEnemy(@"Tank\Enemy\Red\PlainTank\", @"Tank\Enemy\Red\ArmoredPersonnelCarrier\", @"Tank\Enemy\Red\QuickFireTank\", @"Tank\Enemy\Red\ArmoredTank\");
        private Bonus bonuses = new Bonus();

#pragma warning disable CS0649
        private ImageSource dashboardInfo;
        private ImageSource dashboardInfoIIPlayer;
        private ImageSource informationTank;

        private readonly string pathForShell = @"Shell\";
        private ImageSource shellUp;
        private ImageSource shellDown;
        private ImageSource shellRight;
        private ImageSource shellLeft;

        private readonly string pathForDetonation = @"Detonation\";
        private ImageSource shellDetonation1;
        private ImageSource shellDetonation2;
        private ImageSource shellDetonation3;
        private ImageSource shellDetonation4;
        private ImageSource shellDetonationBig;
        private ImageSource shellDetonationBig2;

        private ImageSource brickWall;
        private ImageSource concreteWall;
        private ImageSource forest;
        private ImageSource ice;

        private readonly string pathForEagle = @"Eagle\";
        private ImageSource eagle;
        private ImageSource eagle2;

        private readonly string pathForWater = @"Water\";
        private ImageSource water_1;
        private ImageSource water_2;
        private ImageSource water_3;

        private readonly string pathForStar = @"Tank\";
        private ImageSource star1;
        private ImageSource star2;
        private ImageSource star3;
        private ImageSource star4;

        private string pathForInvulnerable = @"Tank\";
        private ImageSource invulnerable1;
        private ImageSource invulnerable2;
#pragma warning restore CS0649

        public Bonus Bonuses { get { return bonuses; } }
        public TankPlaeyr Plaeyr { get { return tankPlaeyr; } }
        public TankPlaeyr Plaeyr2 { get { return tankPlaeyr2; } }
        public TankEnemyExtend Enemy { get { return tankEnemy; } }
        public TankEnemy TankRed { get { return tankRed; } }

        public ImageSource DashboardInfo
        {
            get
            {
                return Validate(dashboardInfo, @"Info\DashboardInfo.png");
            }
        }
        public ImageSource DashboardInfoIIPlayer
        {
            get
            {
                return Validate(dashboardInfoIIPlayer, @"Info\DashboardInfoIIPlayer.png");
            }
        }

        public ImageSource InformationTank
        {
            get
            {
                return Validate(informationTank, @"Info\InformationTank.png");
            }
        }

        public ImageSource ShellUp
        {
            get
            {
                return Validate(shellUp, pathForShell + @"ShellUp.png");
            }
        }
        public ImageSource ShellDown
        {
            get
            {
                return Validate(shellDown, pathForShell + @"ShellDown.png");
            }
        }
        public ImageSource ShellLeft
        {
            get
            {
                return Validate(shellLeft, pathForShell + @"ShellLeft.png");
            }
        }
        public ImageSource ShellRight
        {
            get
            {
                return Validate(shellRight, pathForShell + @"ShellRight.png");
            }
        }

        public ImageSource ShellDetonation1
        {
            get
            {
                return Validate(shellDetonation1, pathForDetonation + @"Detonation1.png");
            }
        }
        public ImageSource ShellDetonation2
        {
            get
            {
                return Validate(shellDetonation2, pathForDetonation + @"Detonation2.png");
            }
        }
        public ImageSource ShellDetonation3
        {
            get
            {
                return Validate(shellDetonation3, pathForDetonation + @"Detonation3.png");
            }
        }
        public ImageSource ShellDetonation4
        {
            get
            {
                return Validate(shellDetonation4, pathForDetonation + @"Detonation4.png");
            }
        }
        public ImageSource ShellDetonationBig
        {
            get
            {
                return Validate(shellDetonationBig, pathForDetonation + @"DetonationBig.png");
            }
        }
        public ImageSource ShellDetonationBig2
        {
            get
            {
                return Validate(shellDetonationBig2, pathForDetonation + @"DetonationBig2.png");
            }
        }

        public ImageSource Water_1
        {
            get
            {
                return Validate(water_1, pathForWater + @"Water_1.png");
            }
        }
        public ImageSource Water_2
        {
            get
            {
                return Validate(water_2, pathForWater + @"Water_2.png");
            }
        }
        public ImageSource Water_3
        {
            get
            {
                return Validate(water_3, pathForWater + @"Water_3.png");
            }
        }

        public ImageSource BrickWall
        {
            get
            {
                return Validate(brickWall, "BrickWall.png");
            }
        }
        public ImageSource Forest
        {
            get
            {
                return Validate(forest, @"Forest.png");
            }
        }
        public ImageSource Ice
        {
            get
            {
                return Validate(ice, @"Ice.png");
            }
        }
        public ImageSource ConcreteWall
        {
            get
            {
                return Validate(concreteWall, @"ConcreteWall.png");
            }
        }

        public ImageSource Eagle
        {
            get
            {
                return Validate(eagle, pathForEagle + "Eagle.png");
            }
        }
        public ImageSource Eagle2
        {
            get
            {
                return Validate(eagle2, pathForEagle + "Eagle2.png");
            }
        }

        public ImageSource Star1
        {
            get
            {
                return Validate(star1, pathForStar + @"Star1.png");
            }
        }
        public ImageSource Star2
        {
            get
            {
                return Validate(star2, pathForStar + @"Star2.png");
            }
        }
        public ImageSource Star3
        {
            get
            {
                return Validate(star3, pathForStar + @"Star2.png");
            }
        }
        public ImageSource Star4
        {
            get
            {
                return Validate(star4, pathForStar + @"Star4.png");
            }
        }


        public ImageSource Invulnerable1
        {
            get
            {
                return Validate(invulnerable1, pathForInvulnerable + @"Invulnerable1.png");
            }
        }
        public ImageSource Invulnerable2
        {
            get
            {
                return Validate(invulnerable2, pathForInvulnerable + @"Invulnerable2.png");
            }
        }


        private static ImageSource Validate(ImageSource img, String imgName)
        {
            try
            {
                return img == null ? img = new BitmapImage(new Uri(ConfigurationWPF.TexturePath + imgName, UriKind.Absolute)) : img;
            }
            catch (Exception ex)
            {
                Console.WriteLine(imgName);
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public Dictionary<Direction, ImageSource[]> GetImgesForTank(TypeUnit type)
        {
            switch (type)
            {
                case TypeUnit.SmallTankPlaeyr:
                    return GetImages(Plaeyr.SmallTank);
                case TypeUnit.LightTankPlaeyr:
                    return GetImages(Plaeyr.LightTank);
                case TypeUnit.MediumTankPlaeyr:
                    return GetImages(Plaeyr.MmediumTank);
                case TypeUnit.HeavyTankPlaeyr:
                    return GetImages(Plaeyr.HeavyTank);

                case TypeUnit.PlainTank:
                    return GetImages(Enemy.PlainTank);
                case TypeUnit.ArmoredPersonnelCarrierTank:
                    return GetImages(Enemy.ArmoredPersonnelCarrierTank);
                case TypeUnit.QuickFireTank:
                    return GetImages(Enemy.QuickFireTank);
                case TypeUnit.ArmoredTank:
                    return GetImages(Enemy.ArmoredTank);
            }
            return null;
        }
        public Dictionary<Direction, ImageSource[]> GetImgesForTankIIPlayer(TypeUnit type)
        {
            switch (type)
            {
                case TypeUnit.SmallTankPlaeyr:
                    return GetImages(Plaeyr2.SmallTank);
                case TypeUnit.LightTankPlaeyr:
                    return GetImages(Plaeyr2.LightTank);
                case TypeUnit.MediumTankPlaeyr:
                    return GetImages(Plaeyr2.MmediumTank);
                case TypeUnit.HeavyTankPlaeyr:
                    return GetImages(Plaeyr2.HeavyTank);
            }
            return null;
        }

        public Dictionary<Direction, ImageSource[]> GetImgesForRedTank(TypeUnit type)
        {
            switch (type)
            {
                case TypeUnit.PlainTank:
                    return GetImages(TankRed.PlainTank);
                case TypeUnit.ArmoredPersonnelCarrierTank:
                    return GetImages(TankRed.ArmoredPersonnelCarrierTank);
                case TypeUnit.QuickFireTank:
                    return GetImages(TankRed.QuickFireTank);
                case TypeUnit.ArmoredTank:
                    return GetImages(TankRed.ArmoredTank);
            }
            return null;
        }

        public Dictionary<Direction, ImageSource[]> GetImages(Tank tank)
        {
            return new Dictionary<Direction, ImageSource[]>
                    {
                        {Direction.Up, new []
                        { tank.Up1, tank.Up2 } },
                        {Direction.Down, new []
                        { tank.Down1, tank.Down2 } },
                        {Direction.Left, new []
                        { tank.Left1, tank.Left2 } },
                        {Direction.Right,new []
                        { tank.Right1, tank.Right2 } }
                    };
        }

        public class TankEnemy
        {
            private readonly Tank plainTank;
            private readonly Tank armoredPersonnelCarrierTank;
            private readonly Tank quickFireTank;
            private readonly Tank armoredTank;

            public TankEnemy(string pathForPlainTank, string pathForArmoredPersonnelCarrierTank, string pathForQuickFireTank, string pathForArmoredTank)
            {
                plainTank = new Tank(pathForPlainTank);
                armoredPersonnelCarrierTank = new Tank(pathForArmoredPersonnelCarrierTank);
                quickFireTank = new Tank(pathForQuickFireTank);
                armoredTank = new Tank(pathForArmoredTank);
            }

            public Tank PlainTank { get { return plainTank; } }
            public Tank ArmoredPersonnelCarrierTank { get { return armoredPersonnelCarrierTank; } }
            public Tank QuickFireTank { get { return quickFireTank; } }
            public Tank ArmoredTank { get { return armoredTank; } }
        }

        public class TankEnemyExtend : TankEnemy
        {
            private readonly Tank armoredTankGreen;
            private readonly Tank armoredTankYellow;

            public TankEnemyExtend(string pathForPlainTank, string pathForArmoredPersonnelCarrierTank, string pathForQuickFireTank, string pathForArmoredTank, string pathForArmoredTankGreen, string pathForArmoredTankYellow) : base(pathForPlainTank, pathForArmoredPersonnelCarrierTank, pathForQuickFireTank, pathForArmoredTank)
            {
                armoredTankGreen = new Tank(pathForArmoredTankGreen);
                armoredTankYellow = new Tank(pathForArmoredTankYellow);
            }

            public Tank ArmoredTankGreen { get { return armoredTankGreen; } }
            public Tank ArmoredTankYellow { get { return armoredTankYellow; } }
        }

        public class Tank
        {
            private readonly string pathForTank;
#pragma warning disable CS0649
            private ImageSource up1;
            private ImageSource up2;
            private ImageSource down1;
            private ImageSource down2;
            private ImageSource right1;
            private ImageSource right2;
            private ImageSource left1;
            private ImageSource left2;
#pragma warning restore CS0649

            public Tank(string pathForTank)
            {
                this.pathForTank = pathForTank;
            }

            public ImageSource Up1
            {
                get
                {
                    return Validate(up1, pathForTank + "Up1.png");
                }
            }
            public ImageSource Up2
            {
                get
                {
                    return Validate(up2, pathForTank + "Up2.png");
                }
            }
            public ImageSource Down1
            {
                get
                {
                    return Validate(down1, pathForTank + "Down1.png");
                }
            }
            public ImageSource Down2
            {
                get
                {
                    return Validate(down2, pathForTank + "Down2.png");
                }
            }
            public ImageSource Left1
            {
                get
                {
                    return Validate(left1, pathForTank + "Left1.png");
                }
            }
            public ImageSource Left2
            {
                get
                {
                    return Validate(left2, pathForTank + "Left2.png");
                }
            }
            public ImageSource Right1
            {
                get
                {
                    return Validate(right1, pathForTank + "Right1.png");
                }
            }
            public ImageSource Right2
            {
                get
                {
                    return Validate(right2, pathForTank + "Right2.png");
                }
            }
        }

        public class TankPlaeyr
        {
            private readonly Tank smallTank;
            private readonly Tank lightTank;
            private readonly Tank mediumTank;
            private readonly Tank heavyTank;

            public TankPlaeyr(string pathForSmallTank, string pathForLightTank, string pathForMediumTank, string pathForheavyTank)
            {
                smallTank = new Tank(pathForSmallTank);
                lightTank = new Tank(pathForLightTank);
                mediumTank = new Tank(pathForMediumTank);
                heavyTank = new Tank(pathForheavyTank);
            }
            public Tank SmallTank { get { return smallTank; } }
            public Tank LightTank { get { return lightTank; } }
            public Tank MmediumTank { get { return mediumTank; } }
            public Tank HeavyTank { get { return heavyTank; } }
        }

        public class Bonus
        {
            private readonly string pathForBonus = @"Bonus\";
#pragma warning disable CS0649
            private ImageSource clock;
            private ImageSource grenade;
            private ImageSource helmet;
            private ImageSource pistol;
            private ImageSource shovel;
            private ImageSource starMedal;
            private ImageSource tank;
#pragma warning restore CS0649

            public ImageSource Clock
            {
                get
                {
                    return Validate(clock, pathForBonus + @"Clock.png");
                }
            }
            public ImageSource Grenade
            {
                get
                {
                    return Validate(grenade, pathForBonus + @"Grenade.png");
                }
            }
            public ImageSource Helmet
            {
                get
                {
                    return Validate(helmet, pathForBonus + @"Helmet.png");
                }
            }
            public ImageSource Pistol
            {
                get
                {
                    return Validate(pistol, pathForBonus + @"Pistol.png");
                }
            }
            public ImageSource Shovel
            {
                get
                {
                    return Validate(shovel, pathForBonus + @"Shovel.png");
                }
            }
            public ImageSource StarMedal
            {
                get
                {
                    return Validate(starMedal, pathForBonus + @"StarMedal.png");
                }
            }
            public ImageSource Tank
            {
                get
                {
                    return Validate(tank, pathForBonus + @"Tank.png");
                }
            }
        }
    }
}

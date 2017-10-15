using System;
using System.Collections.Generic;
using System.Drawing;

namespace SuperTank.View
{
    /// <summary>
    /// Caches and simplifies access to images
    /// </summary>
    static class Images
    {
        private static TankPlaeyr tankPlaeyr = new TankPlaeyr(@"Tank\Plaeyr\SmallTank\", @"Tank\Plaeyr\LightTank\", @"Tank\Plaeyr\MediumTank\", @"Tank\Plaeyr\HeavyTank\");
        private static TankPlaeyr tankPlaeyr2 = new TankPlaeyr(@"Tank\Plaeyr2\SmallTank\", @"Tank\Plaeyr2\LightTank\", @"Tank\Plaeyr2\MediumTank\", @"Tank\Plaeyr2\HeavyTank\");
        private static TankEnemyExtend tankEnemy = new TankEnemyExtend(@"Tank\Enemy\PlainTank\", @"Tank\Enemy\ArmoredPersonnelCarrier\", @"Tank\Enemy\QuickFireTank\", @"Tank\Enemy\ArmoredTank\", @"Tank\Enemy\ArmoredTankGreen\", @"Tank\Enemy\ArmoredTankYellow\");
        private static TankEnemy tankRed = new TankEnemy(@"Tank\Enemy\Red\PlainTank\", @"Tank\Enemy\Red\ArmoredPersonnelCarrier\", @"Tank\Enemy\Red\QuickFireTank\", @"Tank\Enemy\Red\ArmoredTank\");

#pragma warning disable CS0649
        private static Image dashboardInfo;
        private static Image dashboardInfoIIPlayer;
        private static Image informationTank;

        private static readonly string pathForShell = @"Shell\";
        private static Image shellUp;
        private static Image shellDown;
        private static Image shellRight;
        private static Image shellLeft;

        private static readonly string pathForDetonation = @"Detonation\";
        private static Image shellDetonation1;
        private static Image shellDetonation2;
        private static Image shellDetonation3;
        private static Image shellDetonation4;
        private static Image shellDetonationBig;
        private static Image shellDetonationBig2;

        private static Image brickWall;
        private static Image concreteWall;
        private static Image forest;
        private static Image ice;

        private static readonly string pathForEagle = @"Eagle\";
        private static Image eagle;
        private static Image eagle2;

        private static readonly string pathForWater = @"Water\";
        private static Image water_1;
        private static Image water_2;
        private static Image water_3;

        private static readonly string pathForStar = @"Tank\";
        private static Image star1;
        private static Image star2;
        private static Image star3;
        private static Image star4;

        private static string pathForInvulnerable = @"Tank\";
        private static Image invulnerable1;
        private static Image invulnerable2;
#pragma warning restore CS0649

        private static Image blankImage = new Bitmap(1, 1);

        public static Image BlankImage { get { return blankImage; } }

        public static TankPlaeyr Plaeyr { get { return tankPlaeyr; } }
        public static TankPlaeyr Plaeyr2 { get { return tankPlaeyr2; } }
        public static TankEnemyExtend Enemy { get { return tankEnemy; } }
        public static TankEnemy TankRed { get { return tankRed; } }


        public static Image DashboardInfo
        {
            get
            {
                return Validate(dashboardInfo, @"Info\DashboardInfo.png");
            }
        }
        public static Image DashboardInfoIIPlayer
        {
            get
            {
                return Validate(dashboardInfoIIPlayer, @"Info\DashboardInfoIIPlayer.png");
            }
        }

        public static Image InformationTank
        {
            get
            {
                return Validate(informationTank, @"Info\InformationTank.png");
            }
        }

        public static Image ShellUp
        {
            get
            {
                return Validate(shellUp, pathForShell + @"ShellUp.png");
            }
        }
        public static Image ShellDown
        {
            get
            {
                return Validate(shellDown, pathForShell + @"ShellDown.png");
            }
        }
        public static Image ShellLeft
        {
            get
            {
                return Validate(shellLeft, pathForShell + @"ShellLeft.png");
            }
        }
        public static Image ShellRight
        {
            get
            {
                return Validate(shellRight, pathForShell + @"ShellRight.png");
            }
        }

        public static Image ShellDetonation1
        {
            get
            {
                return Validate(shellDetonation1, pathForDetonation + @"Detonation1.png");
            }
        }
        public static Image ShellDetonation2
        {
            get
            {
                return Validate(shellDetonation2, pathForDetonation + @"Detonation2.png");
            }
        }
        public static Image ShellDetonation3
        {
            get
            {
                return Validate(shellDetonation3, pathForDetonation + @"Detonation3.png");
            }
        }
        public static Image ShellDetonation4
        {
            get
            {
                return Validate(shellDetonation4, pathForDetonation + @"Detonation4.png");
            }
        }
        public static Image ShellDetonationBig
        {
            get
            {
                return Validate(shellDetonationBig, pathForDetonation + @"DetonationBig.png");
            }
        }
        public static Image ShellDetonationBig2
        {
            get
            {
                return Validate(shellDetonationBig2, pathForDetonation + @"DetonationBig2.png");
            }
        }

        public static Image Water_1
        {
            get
            {
                return Validate(water_1, pathForWater + @"Water_1.png");
            }
        }
        public static Image Water_2
        {
            get
            {
                return Validate(water_2, pathForWater + @"Water_2.png");
            }
        }
        public static Image Water_3
        {
            get
            {
                return Validate(water_3, pathForWater + @"Water_3.png");
            }
        }

        public static Image BrickWall
        {
            get
            {
                return Validate(brickWall, "BrickWall.png");
            }
        }
        public static Image Forest
        {
            get
            {
                return Validate(forest, @"Forest.png");
            }
        }
        public static Image Ice
        {
            get
            {
                return Validate(ice, @"Ice.png");
            }
        }
        public static Image ConcreteWall
        {
            get
            {
                return Validate(concreteWall, @"ConcreteWall.png");
            }
        }

        public static Image Eagle
        {
            get
            {
                return Validate(eagle, pathForEagle + "Eagle.png");
            }
        }
        public static Image Eagle2
        {
            get
            {
                return Validate(eagle2, pathForEagle + "Eagle2.png");
            }
        }

        public static Image Star1
        {
            get
            {
                return Validate(star1, pathForStar + @"Star1.png");
            }
        }
        public static Image Star2
        {
            get
            {
                return Validate(star2, pathForStar + @"Star2.png");
            }
        }
        public static Image Star3
        {
            get
            {
                return Validate(star3, pathForStar + @"Star2.png");
            }
        }
        public static Image Star4
        {
            get
            {
                return Validate(star4, pathForStar + @"Star4.png");
            }
        }


        public static Image Invulnerable1
        {
            get
            {
                return Validate(invulnerable1, pathForInvulnerable + @"Invulnerable1.png");
            }
        }
        public static Image Invulnerable2
        {
            get
            {
                return Validate(invulnerable2, pathForInvulnerable + @"Invulnerable2.png");
            }
        }


        private static Image Validate(Image img, String imgName)
        {
            try
            {
                return img == null ? img = Image.FromFile(ConfigurationView.TexturePath + imgName) : img;
            }
            catch (Exception ex)
            {
                Console.WriteLine(imgName);
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static Dictionary<Direction, Image[]> GetImgesForTank(TypeUnit type)
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
        public static Dictionary<Direction, Image[]> GetImgesForTankIIPlayer(TypeUnit type)
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

        public static Dictionary<Direction, Image[]> GetImgesForRedTank(TypeUnit type)
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

        public static Dictionary<Direction, Image[]> GetImages(Tank tank)
        {
            return new Dictionary<Direction, Image[]>
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
            private Image up1;
            private Image up2;
            private Image down1;
            private Image down2;
            private Image right1;
            private Image right2;
            private Image left1;
            private Image left2;
#pragma warning restore CS0649

            public Tank(string pathForTank)
            {
                this.pathForTank = pathForTank;
            }

            public Image Up1
            {
                get
                {
                    return Validate(up1, pathForTank + "Up1.png");
                }
            }
            public Image Up2
            {
                get
                {
                    return Validate(up2, pathForTank + "Up2.png");
                }
            }
            public Image Down1
            {
                get
                {
                    return Validate(down1, pathForTank + "Down1.png");
                }
            }
            public Image Down2
            {
                get
                {
                    return Validate(down2, pathForTank + "Down2.png");
                }
            }
            public Image Left1
            {
                get
                {
                    return Validate(left1, pathForTank + "Left1.png");
                }
            }
            public Image Left2
            {
                get
                {
                    return Validate(left2, pathForTank + "Left2.png");
                }
            }
            public Image Right1
            {
                get
                {
                    return Validate(right1, pathForTank + "Right1.png");
                }
            }
            public Image Right2
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

        public static class Bonus
        {
            private static readonly string pathForBonus = @"Bonus\";
#pragma warning disable CS0649
            private static Image clock;
            private static Image grenade;
            private static Image helmet;
            private static Image pistol;
            private static Image shovel;
            private static Image starMedal;
            private static Image tank;
#pragma warning restore CS0649

            public static Image Clock
            {
                get
                {
                    return Validate(clock, pathForBonus + @"Clock.png");
                }
            }
            public static Image Grenade
            {
                get
                {
                    return Validate(grenade, pathForBonus + @"Grenade.png");
                }
            }
            public static Image Helmet
            {
                get
                {
                    return Validate(helmet, pathForBonus + @"Helmet.png");
                }
            }
            public static Image Pistol
            {
                get
                {
                    return Validate(pistol, pathForBonus + @"Pistol.png");
                }
            }
            public static Image Shovel
            {
                get
                {
                    return Validate(shovel, pathForBonus + @"Shovel.png");
                }
            }
            public static Image StarMedal
            {
                get
                {
                    return Validate(starMedal, pathForBonus + @"StarMedal.png");
                }
            }
            public static Image Tank
            {
                get
                {
                    return Validate(tank, pathForBonus + @"Tank.png");
                }
            }
        }
    }
}

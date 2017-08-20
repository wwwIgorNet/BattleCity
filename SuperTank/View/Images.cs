using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.View
{
    static class Images
    {
        public static TankPlaeyr plaeyrTank = new TankPlaeyr(@"Tank\Plaeyr\SmallTank\", "", "", "");


        private static Image shellUp;
        private static Image shellDown;
        private static Image shellRight;
        private static Image shellLeft;

        private static Image shellDetonation1;
        private static Image shellDetonation2;
        private static Image shellDetonation3;
        private static Image shellDetonation4;
        private static Image shellDetonationBig;
        private static Image shellDetonationBig2;

        private static Image brickWall;
        private static Image concreteWall;
        private static Image water_1;
        private static Image water_2;
        private static Image water_3;
        private static Image forest;
        private static Image ice;
        private static Image eagle;
        private static Image star4;
        private static Image star2;
        private static Image star3;


        public static TankPlaeyr Plaeyr { get { return plaeyrTank; } }

        public static Image ShellUp
        {
            get
            {
                return Validate(shellUp, @"Shell\ShellUp.png");
            }
        }
        public static Image ShellDown
        {
            get
            {
                return Validate(shellDown, @"Shell\ShellDown.png");
            }
        }
        public static Image ShellLeft
        {
            get
            {
                return Validate(shellLeft, @"Shell\ShellLeft.png");
            }
        }
        public static Image ShellRight
        {
            get
            {
                return Validate(shellRight, @"Shell\ShellRight.png");
            }
        }
        public static Image ShellDetonation1
        {
            get
            {
                return Validate(shellDetonation1, @"Detonation\Detonation1.png");
            }
        }
        public static Image ShellDetonation2
        {
            get
            {
                return Validate(shellDetonation2, @"Detonation\Detonation2.png");
            }
        }
        public static Image ShellDetonation3
        {
            get
            {
                return Validate(shellDetonation3, @"Detonation\Detonation3.png");
            }
        }
        public static Image ShellDetonation4
        {
            get
            {
                return Validate(shellDetonation4, @"Detonation\Detonation4.png");
            }
        }
        public static Image ShellDetonationBig
        {
            get
            {
                return Validate(shellDetonationBig, @"Detonation\DetonationBig.png");
            }
        }
        public static Image ShellDetonationBig2
        {
            get
            {
                return Validate(shellDetonationBig2, @"Detonation\DetonationBig2.png");
            }
        }
        
        public static Image Water_1
        {
            get
            {
                return Validate(water_1, @"Water\Water_1.png");
            }
        }
        public static Image Water_2
        {
            get
            {
                return Validate(water_2, @"Water\Water_2.png");
            }
        }
        public static Image Water_3
        {
            get
            {
                return Validate(water_3, @"Water\Water_3.png");
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
                return Validate(eagle, @"Eagle\Eagle.png");
            }
        }

        public static Image Star1
        {
            get
            {
                return Validate(star4, @"Tank\Star1.png");
            }
        }
        public static Image Star2
        {
            get
            {
                return Validate(star2, @"Tank\Star2.png");
            }
        }
        public static Image Star3
        {
            get
            {
                return Validate(star3, @"Tank\Star2.png");
            }
        }
        public static Image Star4
        {
            get
            {
                return Validate(star4, @"Tank\Star4.png");
            }
        }

        private static Image Validate(Image img, String imgName)
        {
            try
            {
                return img == null ? img = Image.FromFile(ConfigurationView.TexturePath + imgName) : img;
            }catch (Exception ex)
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
                    return new Dictionary<Direction, Image[]>
                    {
                        {Direction.Up, new []
                        { Plaeyr.SmallTankUp1, Plaeyr.SmallTankUp2 } },
                        {Direction.Down, new []
                        { Plaeyr.SmallTankDown1, Plaeyr.SmallTankDown2 } },
                        {Direction.Left, new []
                        { Plaeyr.SmallTankLeft1, Plaeyr.SmallTankLeft2 } },
                        {Direction.Right,new []
                        { Plaeyr.SmallTankRight1, Plaeyr.SmallTankRight2 } }
                    };
                case TypeUnit.LightTankPlaeyr:
                    break;
                case TypeUnit.MediumTankPlaeyr:
                    break;
                case TypeUnit.HeavyTankPlaeyr:
                    break;

                case TypeUnit.PainTank:
                    return new Dictionary<Direction, Image[]>
                    {
                        {Direction.Up, new []
                        { Enemy.PlainTankUp1, Enemy.PlainTankUp2 } },
                        {Direction.Down, new []
                        { Enemy.PlainTankDown1, Enemy.PlainTankDown2 } },
                        {Direction.Left, new []
                        { Enemy.PlainTankLeft1, Enemy.PlainTankLeft2 } },
                        {Direction.Right,new []
                        { Enemy.PlainTankRight1, Enemy.PlainTankRight2 } }
                    };
                case TypeUnit.ArmoredPersonnelCarrierTank:
                    return new Dictionary<Direction, Image[]>
                    {
                        {Direction.Up, new []
                        { Enemy.ArmoredPersonnelCarrierUp1, Enemy.ArmoredPersonnelCarrierUp2 } },
                        {Direction.Down, new []
                        { Enemy.ArmoredPersonnelCarrierDown1, Enemy.ArmoredPersonnelCarrierDown2 } },
                        {Direction.Left, new []
                        { Enemy.ArmoredPersonnelCarrierLeft1, Enemy.ArmoredPersonnelCarrierLeft2 } },
                        {Direction.Right,new []
                        { Enemy.ArmoredPersonnelCarrierRight1, Enemy.ArmoredPersonnelCarrierRight2 } }
                    };
                case TypeUnit.QuickFireTank:
                    return new Dictionary<Direction, Image[]>
                    {
                        {Direction.Up, new []
                        { Enemy.QuickFireTankUp1, Enemy.QuickFireTankUp2 } },
                        {Direction.Down, new []
                        { Enemy.QuickFireTankDown1, Enemy.QuickFireTankDown2 } },
                        {Direction.Left, new []
                        { Enemy.QuickFireTankLeft1, Enemy.QuickFireTankLeft2 } },
                        {Direction.Right,new []
                        { Enemy.QuickFireTankight1, Enemy.QuickFireTankight2 } }
                    };
                case TypeUnit.ArmoredTank:
                    return new Dictionary<Direction, Image[]>
                    {
                        {Direction.Up, new []
                        { Enemy.ArmoredTankUp1, Enemy.ArmoredTankUp2 } },
                        {Direction.Down, new []
                        { Enemy.ArmoredTankDown1, Enemy.ArmoredTankDown2 } },
                        {Direction.Left, new []
                        { Enemy.ArmoredTankLeft1, Enemy.ArmoredTankLeft2 } },
                        {Direction.Right,new []
                        { Enemy.ArmoredTankight1, Enemy.ArmoredTankight2 } }
                    };
            }
            return null;
        }

        public static class Enemy
        {
            private static readonly string pathForPlainTank = @"Tank\Enemy\PlainTank\";
            private static Image plainTankUp1;
            private static Image plainTankDown1;
            private static Image plainTankRight1;
            private static Image plainTankLeft1;
            private static Image plainTankUp2;
            private static Image plainTankDown2;
            private static Image plainTankRight2;
            private static Image plainTankLeft2;

            private static readonly string pathForArmoredPersonnelCarrierTank = @"Tank\Enemy\ArmoredPersonnelCarrier\";
            private static Image armoredPersonnelCarrierUp1;
            private static Image armoredPersonnelCarrierDown1;
            private static Image armoredPersonnelCarrierRight1;
            private static Image armoredPersonnelCarrierLeft1;
            private static Image armoredPersonnelCarrierUp2;
            private static Image armoredPersonnelCarrierDown2;
            private static Image armoredPersonnelCarrierRight2;
            private static Image armoredPersonnelCarrierLeft2;


            private static readonly string pathForQuickFireTankTank = @"Tank\Enemy\QuickFireTank\";
            private static Image quickFireTankUp1;
            private static Image quickFireTankDown1;
            private static Image quickFireTankight1;
            private static Image quickFireTankLeft1;
            private static Image quickFireTankUp2;
            private static Image quickFireTankDown2;
            private static Image quickFireTankight2;
            private static Image quickFireTankLeft2;


            private static readonly string pathForArmoredTankTank = @"Tank\Enemy\ArmoredTank\";
            private static Image armoredTankUp1;
            private static Image armoredTankDown1;
            private static Image armoredTankight1;
            private static Image armoredTankLeft1;
            private static Image armoredTankUp2;
            private static Image armoredTankDown2;
            private static Image armoredTankight2;
            private static Image armoredTankLeft2;

            public static Image PlainTankUp1
            {
                get
                {
                    return Validate(plainTankUp1, pathForPlainTank + "Up1.png");
                }
            }
            public static Image PlainTankUp2
            {
                get
                {
                    return Validate(plainTankUp2, pathForPlainTank + "Up2.png");
                }
            }
            public static Image PlainTankDown1
            {
                get
                {
                    return Validate(plainTankDown1, pathForPlainTank + "Down1.png");
                }
            }
            public static Image PlainTankDown2
            {
                get
                {
                    return Validate(plainTankDown2, pathForPlainTank + "Down2.png");
                }
            }
            public static Image PlainTankLeft1
            {
                get
                {
                    return Validate(plainTankLeft1, pathForPlainTank + "Left1.png");
                }
            }
            public static Image PlainTankLeft2
            {
                get
                {
                    return Validate(plainTankLeft2, pathForPlainTank + "Left2.png");
                }
            }
            public static Image PlainTankRight1
            {
                get
                {
                    return Validate(plainTankRight1, pathForPlainTank + "Right1.png");
                }
            }
            public static Image PlainTankRight2
            {
                get
                {
                    return Validate(plainTankRight2, pathForPlainTank + "Right2.png");
                }
            }

            public static Image ArmoredPersonnelCarrierUp1
            {
                get
                {
                    return Validate(armoredPersonnelCarrierUp1, pathForArmoredPersonnelCarrierTank + "Up1.png");
                }
            }
            public static Image ArmoredPersonnelCarrierUp2
            {
                get
                {
                    return Validate(armoredPersonnelCarrierUp2, pathForArmoredPersonnelCarrierTank + "Up2.png");
                }
            }
            public static Image ArmoredPersonnelCarrierDown1
            {
                get
                {
                    return Validate(armoredPersonnelCarrierDown1, pathForArmoredPersonnelCarrierTank + "Down1.png");
                }
            }
            public static Image ArmoredPersonnelCarrierDown2
            {
                get
                {
                    return Validate(armoredPersonnelCarrierDown2, pathForArmoredPersonnelCarrierTank + "Down2.png");
                }
            }
            public static Image ArmoredPersonnelCarrierLeft1
            {
                get
                {
                    return Validate(armoredPersonnelCarrierLeft1, pathForArmoredPersonnelCarrierTank + "Left1.png");
                }
            }
            public static Image ArmoredPersonnelCarrierLeft2
            {
                get
                {
                    return Validate(armoredPersonnelCarrierLeft2, pathForArmoredPersonnelCarrierTank + "Left2.png");
                }
            }
            public static Image ArmoredPersonnelCarrierRight1
            {
                get
                {
                    return Validate(armoredPersonnelCarrierRight1, pathForArmoredPersonnelCarrierTank + "Right1.png");
                }
            }
            public static Image ArmoredPersonnelCarrierRight2
            {
                get
                {
                    return Validate(armoredPersonnelCarrierRight2, pathForArmoredPersonnelCarrierTank + "Right2.png");
                }
            }

            public static Image QuickFireTankUp1
            {
                get
                {
                    return Validate(quickFireTankUp1, pathForQuickFireTankTank + "Up1.png");
                }
            }
            public static Image QuickFireTankUp2
            {
                get
                {
                    return Validate(quickFireTankUp2, pathForQuickFireTankTank + "Up2.png");
                }
            }
            public static Image QuickFireTankDown1
            {
                get
                {
                    return Validate(quickFireTankDown1, pathForQuickFireTankTank + "Down1.png");
                }
            }
            public static Image QuickFireTankDown2
            {
                get
                {
                    return Validate(quickFireTankDown2, pathForQuickFireTankTank + "Down2.png");
                }
            }
            public static Image QuickFireTankLeft1
            {
                get
                {
                    return Validate(quickFireTankLeft1, pathForQuickFireTankTank + "Left1.png");
                }
            }
            public static Image QuickFireTankLeft2
            {
                get
                {
                    return Validate(quickFireTankLeft2, pathForQuickFireTankTank + "Left2.png");
                }
            }
            public static Image QuickFireTankight1
            {
                get
                {
                    return Validate(quickFireTankight1, pathForQuickFireTankTank + "Right1.png");
                }
            }
            public static Image QuickFireTankight2
            {
                get
                {
                    return Validate(quickFireTankight2, pathForQuickFireTankTank + "Right2.png");
                }
            }

            public static Image ArmoredTankUp1
            {
                get
                {
                    return Validate(armoredTankUp1, pathForArmoredTankTank + "Up1.png");
                }
            }
            public static Image ArmoredTankUp2
            {
                get
                {
                    return Validate(armoredTankUp2, pathForArmoredTankTank + "Up2.png");
                }
            }
            public static Image ArmoredTankDown1
            {
                get
                {
                    return Validate(armoredTankDown1, pathForArmoredTankTank + "Down1.png");
                }
            }
            public static Image ArmoredTankDown2
            {
                get
                {
                    return Validate(armoredTankDown2, pathForArmoredTankTank + "Down2.png");
                }
            }
            public static Image ArmoredTankLeft1
            {
                get
                {
                    return Validate(armoredTankLeft1, pathForArmoredTankTank + "Left1.png");
                }
            }
            public static Image ArmoredTankLeft2
            {
                get
                {
                    return Validate(armoredTankLeft2, pathForArmoredTankTank + "Left2.png");
                }
            }
            public static Image ArmoredTankight1
            {
                get
                {
                    return Validate(armoredTankight1, pathForArmoredTankTank + "Right1.png");
                }
            }
            public static Image ArmoredTankight2
            {
                get
                {
                    return Validate(armoredTankight2, pathForArmoredTankTank + "Right2.png");
                }
            }
        }

        public class TankPlaeyr
        {
            private readonly string pathForSmallTank;
            private readonly string pathForLightTank;
            private readonly string pathForMediumTank;
            private readonly string pathForHeavyTank;

            private Image smallTankUp1;
            private Image smallTankUp2;
            private Image smallTankDown1;
            private Image smallTankDown2;
            private Image smallTankRight1;
            private Image smallTankRight2;
            private Image smallTankLeft1;
            private Image smallTankLeft2;

            public TankPlaeyr(string pathForSmallTank, string pathForLightTank, string pathForMediumTank, string pathForheavyTank)
            {
                this.pathForSmallTank = pathForSmallTank;
                this.pathForLightTank = pathForLightTank;
                this.pathForMediumTank = pathForMediumTank;
                this.pathForHeavyTank = pathForheavyTank;
            }

            public Image SmallTankUp1
            {
                get
                {
                    return Validate(smallTankUp1, pathForSmallTank + "Up1.png");
                }
            }
            public Image SmallTankUp2
            {
                get
                {
                    return Validate(smallTankUp2, pathForSmallTank + "Up2.png");
                }
            }
            public Image SmallTankDown1
            {
                get
                {
                    return Validate(smallTankDown1, pathForSmallTank + "Down1.png");
                }
            }
            public Image SmallTankDown2
            {
                get
                {
                    return Validate(smallTankDown2, pathForSmallTank + "Down2.png");
                }
            }
            public Image SmallTankLeft1
            {
                get
                {
                    return Validate(smallTankLeft1, pathForSmallTank + "Left1.png");
                }
            }
            public Image SmallTankLeft2
            {
                get
                {
                    return Validate(smallTankLeft2, pathForSmallTank + "Left2.png");
                }
            }
            public Image SmallTankRight1
            {
                get
                {
                    return Validate(smallTankRight1, pathForSmallTank + "Right1.png");
                }
            }
            public Image SmallTankRight2
            {
                get
                {
                    return Validate(smallTankRight2, pathForSmallTank + "Right2.png");
                }
            }
        }
    }
}

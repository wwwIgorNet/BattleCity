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
        private static Image plainTankUp;
        private static Image plainTankDown;
        private static Image plainTankRight;
        private static Image plainTankLeft;
        
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


        public static Image PlainTankUp
        {
            get
            {
                return Validate(plainTankUp, @"Tank\PlainTank\PlainTankUp.png");
            }
        }
        public static Image PlainTankUp2
        {
            get
            {
                return Validate(plainTankUp, @"Tank\PlainTank\PlainTankUp2.png");
            }
        }
        public static Image PlainTankDown
        {
            get
            {
                return Validate(plainTankDown, @"Tank\PlainTank\PlainTankDown.png");
            }
        }
        public static Image PlainTankDown2
        {
            get
            {
                return Validate(plainTankDown, @"Tank\PlainTank\PlainTankDown2.png");
            }
        }
        public static Image PlainTankLeft
        {
            get
            {
                return Validate(plainTankLeft, @"Tank\PlainTank\PlainTankLeft.png");
            }
        }
        public static Image PlainTankLeft2
        {
            get
            {
                return Validate(plainTankLeft, @"Tank\PlainTank\PlainTankLeft2.png");
            }
        }
        public static Image PlainTankRight
        {
            get
            {
                return Validate(plainTankRight, @"Tank\PlainTank\PlainTankRight.png");
            }
        }
        public static Image PlainTankRight2
        {
            get
            {
                return Validate(plainTankRight, @"Tank\PlainTank\PlainTankRight2.png");
            }
        }

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
            return img == null ? img = Image.FromFile(ConfigurationView.TexturePath + imgName) : img;
        }
    }
}

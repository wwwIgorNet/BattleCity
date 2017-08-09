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

        public static Image brickWall;

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
        private static Image concreteWall;
        private static Image water_1;
        private static Image water_2;
        private static Image water_3;
        private static Image forest;
        private static Image ice;
        private static Image eagle;

        public static Image PlainTankUp
        {
            get
            {
                return Validate(plainTankUp, @"Tank\PlainTankUp.png");
            }
        }
        public static Image PlainTankUp2
        {
            get
            {
                return Validate(plainTankUp, @"Tank\PlainTankUp2.png");
            }
        }
        public static Image PlainTankDown
        {
            get
            {
                return Validate(plainTankDown, @"Tank\PlainTankDown.png");
            }
        }
        public static Image PlainTankDown2
        {
            get
            {
                return Validate(plainTankDown, @"Tank\PlainTankDown2.png");
            }
        }
        public static Image PlainTankLeft
        {
            get
            {
                return Validate(plainTankLeft, @"Tank\PlainTankLeft.png");
            }
        }
        public static Image PlainTankLeft2
        {
            get
            {
                return Validate(plainTankLeft, @"Tank\PlainTankLeft2.png");
            }
        }
        public static Image PlainTankRight
        {
            get
            {
                return Validate(plainTankRight, @"Tank\PlainTankRight.png");
            }
        }
        public static Image PlainTankRight2
        {
            get
            {
                return Validate(plainTankRight, @"Tank\PlainTankRight2.png");
            }
        }
        public static Image BrickWall
        {
            get
            {
                return Validate(brickWall, "BrickWall.png");
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
                return Validate(shellDetonation1, @"Shell\Detonation1.png");
            }
        }
        public static Image ShellDetonation2
        {
            get
            {
                return Validate(shellDetonation2, @"Shell\Detonation2.png");
            }
        }
        public static Image ShellDetonation3
        {
            get
            {
                return Validate(shellDetonation3, @"Shell\Detonation3.png");
            }
        }
        public static Image ShellDetonation4
        {
            get
            {
                return Validate(shellDetonation4, @"Shell\Detonation4.png");
            }
        }
        public static Image ShellDetonationBig
        {
            get
            {
                return Validate(shellDetonationBig, @"Shell\DetonationBig.png");
            }
        }
        public static Image ShellDetonationBig2
        {
            get
            {
                return Validate(shellDetonationBig2, @"Shell\DetonationBig2.png");
            }
        }

        public static Image ConcreteWall
        {
            get
            {
                return Validate(concreteWall, @"ConcreteWall.png");
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

        public static Image Eagle
        {
            get
            {
                return Validate(eagle, @"Eagle\Eagle.png");
            }
        }

        private static Image Validate(Image img, String imgName)
        {
            return img == null ? img = Image.FromFile(ConfigurationView.Texture + imgName) : img;
        }
    }
}

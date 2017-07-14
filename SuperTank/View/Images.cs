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

        public static Image PlainTankUp
        {
            get
            {
                return Validate(plainTankUp, "PlainTankUp.png");
            }
        }
        public static Image PlainTankDown
        {
            get
            {
                return Validate(plainTankDown, "PlainTankDown.png");
            }
        }
        public static Image PlainTankLeft
        {
            get
            {
                return Validate(plainTankLeft, "PlainTankLeft.png");
            }
        }
        public static Image PlainTankRight
        {
            get
            {
                return Validate(plainTankRight, "PlainTankRight.png");
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
                return Validate(shellUp, "ShellUp.png");
            }
        }
        public static Image ShellDown
        {
            get
            {
                return Validate(shellDown, "ShellDown.png");
            }
        }


        public static Image ShellLeft
        {
            get
            {
                return Validate(shellLeft, "ShellLeft.png");
            }
        }
        public static Image ShellRight
        {
            get
            {
                return Validate(shellRight, "ShellRight.png");
            }
        }

        private static Image Validate(Image img, String imgName)
        {
            return img == null ? img = Image.FromFile(ConfigurationView.Texture + imgName) : img;
        }
    }
}

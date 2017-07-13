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

        public static Image PlainTankUp
        {
            get
            {
                return plainTankUp == null ? plainTankUp = Image.FromFile(ConfigurationView.Texture + "PlainTankUp.png") : plainTankUp;
            }
        }
        public static Image PlainTankDown
        {
            get
            {
                return plainTankDown == null ? plainTankDown = Image.FromFile(ConfigurationView.Texture + "PlainTankDown.png") : plainTankDown;
            }
        }
        public static Image PlainTankLeft
        {
            get
            {
                return plainTankLeft== null ? plainTankLeft= Image.FromFile(ConfigurationView.Texture + "PlainTankLeft.png") : plainTankLeft;
            }
        }
        public static Image PlainTankRight
        {
            get
            {
                return plainTankRight == null ? plainTankRight = Image.FromFile(ConfigurationView.Texture + "PlainTankRight.png") : plainTankRight;
            }
        }
        public static Image BrickWall
        {
            get
            {
                return brickWall == null ? brickWall = Image.FromFile(ConfigurationView.Texture + "BrickWall.png") : brickWall;
            }
        }
    }
}

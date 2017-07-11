using GameTankCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_tank
{
    class Images
    {
        private static Image plainUserTankUp;
        private static Image plainUserTankDown;
        private static Image plainUserTankLeft;
        private static Image plainUserTankRight;
        private static Image plainUserTankUp_2;
        private static Image plainUserTankDown_2;
        private static Image plainUserTankLeft_2;
        private static Image plainUserTankRight_2;

        private static Image shellUp;
        private static Image shellDown;
        private static Image shellLeft;
        private static Image shellRight;

        private static Image brickWall;

        static Images()
        {
            plainUserTankUp = Image.FromFile(Configuration.Texture + "PlainUserTankUp.png");
            plainUserTankDown = Image.FromFile(Configuration.Texture + "PlainUserTankDown.png");
            plainUserTankLeft = Image.FromFile(Configuration.Texture + "PlainUserTankLeft.png");
            plainUserTankRight = Image.FromFile(Configuration.Texture + "PplainUserTankRight.png");

            plainUserTankUp_2 = Image.FromFile(Configuration.Texture + "PlainTankPlayerUp_2.png");
            plainUserTankDown_2 = Image.FromFile(Configuration.Texture + "PlainTankPlayerDown_2.png");
            plainUserTankLeft_2 = Image.FromFile(Configuration.Texture + "PlainTankPlayerLeft_2.png");
            plainUserTankRight_2 = Image.FromFile(Configuration.Texture + "PlainTankPlayerRight_2.png");

            shellUp = Image.FromFile(Configuration.Texture + "ShellUp.png");
            shellDown = Image.FromFile(Configuration.Texture + "ShellDown.png");
            shellLeft = Image.FromFile(Configuration.Texture + "ShellLeft.png");
            shellRight = Image.FromFile(Configuration.Texture + "ShellRight.png");
            brickWall = Image.FromFile(Configuration.Texture + "BrickWall.png");
        }


        public static Image PlainUserTankUp { get { return plainUserTankUp; } }
        public static Image PlainUserTankDown { get { return plainUserTankDown; } }
        public static Image PlainUserTankLeft { get { return plainUserTankLeft; } }
        public static Image PlainUserTankRight { get { return plainUserTankRight; } }

        public static Image PlainUserTankUp_2 { get { return plainUserTankUp_2; } }
        public static Image PlainUserTankDown_2 { get { return plainUserTankDown_2; } }
        public static Image PlainUserTankLeft_2 { get { return plainUserTankLeft_2; } }
        public static Image PlainUserTankRight_2 { get { return plainUserTankRight_2; } }

        public static Image ShellUp { get { return shellUp; } }
        public static Image ShellDown { get { return shellDown; } }
        public static Image ShellLeft { get { return shellLeft; } }
        public static Image ShellRight { get { return shellRight; } }

        public static Image BrickWall { get { return brickWall; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class ConfigurationBase
    {
        protected ConfigurationBase() { }

        static ConfigurationBase()
        {
            DelayScrenPoints = TimeSpan.FromSeconds(7);
            DelayScrenLoadLevel = TimeSpan.FromSeconds(3);
            TimeGameOver = 3000;
        }

        private static int heightTile = 20;
        private static int widthTile = 20;
        private static int heightTank = 40;
        private static int widthTank = 40;
        private static int heightShell = 7;
        private static int widthShell = 5;
        private static int heightBoard = 26 * 20;
        private static int widthBoard = 26 * 20;
        private static int timerInterval = 20;
        private static int timeDetonation = 6;
        private static int timeAppearanceOfTank = 48;
        private static int timeBigDetonation = 10;
        private static int countLevel = 35;

        public static int TimeDetonation { get { return timeDetonation; } }
        public static int HeightTile { get { return heightTile; } }
        public static int WidthTile { get { return widthTile; } }
        public static int HeigthTank { get { return heightTank; } }
        public static int WidthTank { get { return widthTank; } }
        public static int HeightShell { get { return heightShell; } }
        public static int WidthShell { get { return widthShell; } }
        public static int HeightBoard { get { return heightBoard; } }
        public static int WidthBoard { get { return widthBoard; } }
        public static int TimerInterval { get { return timerInterval; } }
        public static int TimeAppearanceOfTank { get { return timeAppearanceOfTank; } }
        public static int TimeBigDetonation { get { return timeBigDetonation; } }
        public static int CountLevel { get { return countLevel; } }
        public static TimeSpan DelayScrenPoints { get; internal set; }
        public static TimeSpan DelayScrenLoadLevel { get; internal set; }
        public static int TimeGameOver { get; internal set; }

        public static int GetCountPoints(TypeUnit type)
        {
            switch (type)
            {
                case TypeUnit.PlainTank:
                    return 100;
                case TypeUnit.ArmoredPersonnelCarrierTank:
                    return 200;
                case TypeUnit.QuickFireTank:
                    return 300;
                case TypeUnit.ArmoredTank:
                    return 400;
                case TypeUnit.Clock:
                case TypeUnit.Grenade:
                case TypeUnit.Helmet:
                case TypeUnit.Pistol:
                case TypeUnit.Shovel:
                case TypeUnit.StarMedal:
                case TypeUnit.Tank:
                case TypeUnit.Points:
                    return 500;
            }
            return 0;
        }
    }
}

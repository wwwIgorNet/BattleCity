using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class ConfigurationGame : ConfigurationBase
    {
        private static int countLevel = 1;
        private static Point positionEagle = new Point(12 * ConfigurationGame.WidthTile, ConfigurationGame.HeightBoard - ConfigurationGame.HeightTile * 2);
        private static Point startPositionTankPlaeyr = new Point(9 * ConfigurationGame.WidthTile, ConfigurationGame.HeightBoard - ConfigurationGame.HeigthTank);
        private static Point startPositionTankEnemy1 = new Point(0, 0);
        private static Point startPositionTankEnemy2 = new Point(12 * ConfigurationGame.WidthTile, 0);
        private static Point startPositionTankEnemy3 = new Point(ConfigurationGame.WidthBoard - ConfigurationGame.WidthTank, 0);

        private static int velosityPlainTank = 3;
        private static int velosityShellPlainTank = 6;
        private static int velosityArmoredPersonnelCarrierTank = 5;
        private static int velosityShellArmoredPersonnelCarrierTank = 6;
        private static int velosityQuickFireTank = 3;
        private static int velosityShellQuickFireTank = 10;
        private static int velosityArmoredTank = 3;
        private static int velosityShellArmoredTank = 6;
        
        private static int velositySmallTank = 3;
        private static int velosityShellSmallTank = 6;
        private static int velosityHeavyTank = 3;
        private static int velosityShellHeavyTank = 10;
        private static int velosityLightTank = 3;
        private static int velosityShellLightTank = 10;
        private static int velosityMediumTank = 3;
        private static int velosityShellMediumTank = 10;

        static ConfigurationGame()
        {
            DelayAddingTank = 50;
            DelayPauseForClockBonus = 500;
            DelayShovelBonus = 20;
            GlidDelay = 20;
        }

        public static string Maps { get { return @"Content\Maps\"; } }
        public static int CountLevel { get { return countLevel; } }
        public static Point PositionEagle { get { return positionEagle; } }
        public static Point StartPositionTankPlaeyr { get { return startPositionTankPlaeyr; } }
        public static Point StartPositionTankEnemy1 { get { return startPositionTankEnemy1; } }
        public static Point StartPositionTankEnemy2 { get { return startPositionTankEnemy2; } }
        public static Point StartPositionTankEnemy3 { get { return startPositionTankEnemy3; } }

        public static int VelosityHeavyTank { get { return velosityHeavyTank; } }
        public static int VelosityShellHeavyTank { get { return velosityShellHeavyTank; } }
        public static int VelostyPlainTank { get { return velosityPlainTank; } }
        public static int VelosityShellPlainTank { get { return velosityShellPlainTank; } }
        public static int VelosityArmoredPersonnelCarrierTank { get { return velosityArmoredPersonnelCarrierTank; } }
        public static int VelosityShellArmoredPersonnelCarrierTank { get { return velosityShellArmoredPersonnelCarrierTank; } }
        public static int VelosityQuickFireTank { get { return velosityQuickFireTank; } }
        public static int VelosityShellQuickFireTank { get { return velosityShellQuickFireTank; } }

        public static int VelosityArmoredTank { get { return velosityArmoredTank; } }
        public static int VelosityShellArmoredTank { get { return velosityShellArmoredTank; } }
        public static int VelosityShellLightTank { get { return velosityShellLightTank; } }

        public static int DelayAddingTank { get; set; }
        public static int DelayPauseForClockBonus { get; internal set; }
        public static int DelayShovelBonus { get; internal set; }
        public static int VelosityShellMediumTank { get { return velosityShellMediumTank; } }

        public static int VelosityShellSmallTank { get { return velosityShellSmallTank; } }

        public static int VelositySmallTank { get { return velositySmallTank; } }

        public static int VelosityLightTank { get { return velosityLightTank; } }

        public static int VelosityMediumTank { get { return velosityMediumTank; } }

        public static int GlidDelay { get; internal set; }
    }
}
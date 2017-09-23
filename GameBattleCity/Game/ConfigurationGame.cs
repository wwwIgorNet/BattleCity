﻿using System.Drawing;

namespace SuperTank
{
    public class ConfigurationGame : ConfigurationBase
    {

        static ConfigurationGame()
        {
            DelayAddingTank = 50;
            DelayPauseForClockBonus = 500;
            DelayShovelBonus = 20;
            GlidDelay = 20;

            CountTankEnemy = 20;

            PositionEagle = new Point(12 * ConfigurationGame.WidthTile, ConfigurationGame.HeightBoard - ConfigurationGame.HeightTile * 2);
            StartPositionTankPlaeyr = new Point(8 * ConfigurationGame.WidthTile, ConfigurationGame.HeightBoard - ConfigurationGame.HeigthTank);
            StartPositionTankEnemy1 = new Point(0, 0);
            StartPositionTankEnemy2 = new Point(12 * ConfigurationGame.WidthTile, 0);
            StartPositionTankEnemy3 = new Point(ConfigurationGame.WidthBoard - ConfigurationGame.WidthTank, 0);

            VelostyPlainTank = 3;
            VelosityShellPlainTank = 6;
            VelosityArmoredPersonnelCarrierTank = 5;
            VelosityShellArmoredPersonnelCarrierTank = 6;
            VelosityQuickFireTank = 3;
            VelosityShellQuickFireTank = 10;
            VelosityArmoredTank = 3;
            VelosityShellArmoredTank = 6;

            VelositySmallTank = 3;
            VelosityShellSmallTank = 6;
            VelosityLightTank = 3;
            VelosityShellLightTank = 10;
            VelosityMediumTank = 3;
            VelosityShellMediumTank = 10;
            VelosityHeavyTank = 3;
            VelosityShellHeavyTank = 10;
        }

        public static string Maps { get { return @"Content\Maps\"; } }
        public static Point PositionEagle { get; private set; }
        public static Point StartPositionTankPlaeyr { get; private set; }
        public static Point StartPositionTankEnemy1 { get; private set; }
        public static Point StartPositionTankEnemy2 { get; private set; }
        public static Point StartPositionTankEnemy3 { get; private set; }

        public static int VelostyPlainTank { get; private set; }
        public static int VelosityShellPlainTank { get; private set; }
        public static int VelosityArmoredPersonnelCarrierTank { get; private set; }
        public static int VelosityShellArmoredPersonnelCarrierTank { get; private set; }
        public static int VelosityQuickFireTank { get; private set; }
        public static int VelosityShellQuickFireTank { get; private set; }
        public static int VelosityArmoredTank { get; private set; }
        public static int VelosityShellArmoredTank { get; private set; }

        public static int VelositySmallTank { get; private set; }
        public static int VelosityShellSmallTank { get; private set; }
        public static int VelosityLightTank { get; private set; }
        public static int VelosityShellLightTank { get; private set; }
        public static int VelosityMediumTank { get; private set; }
        public static int VelosityShellMediumTank { get; private set; }
        public static int VelosityHeavyTank { get; private set; }
        public static int VelosityShellHeavyTank { get; private set; }

        public static int DelayAddingTank { get; private set; }
        public static int DelayPauseForClockBonus { get; private set; }
        public static int DelayShovelBonus { get; private set; }
        public static int GlidDelay { get; internal set; }
        public static int CountTankEnemy { get; internal set; }
    }
}
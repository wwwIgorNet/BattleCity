using System;

namespace SuperTank
{
    public class ConfigurationBase
    {
        protected ConfigurationBase() { }

        static ConfigurationBase()
        {
            HeightTile = 20;
            WidthTile = 20;
            HeightTank = 40;
            WidthTank = 40;
            HeightShell = 7;
            WidthShell = 5;

            HeightBoard = 26 * HeightTile;
            WidthBoard = 26 * WidthTile;

            TimerInterval = 20;
            TimeAppearanceOfTank = 48;
            TimeDetonation = 6;
            TimeBigDetonation = 10;
            TimeGameOver = 4000;
            DelayScrenPoints = TimeSpan.FromSeconds(6);
            DelayScrenLoadLevel = TimeSpan.FromSeconds(3);

            CountLevel = 35;
        }

        public static int TimeDetonation { get; private  set; }
        public static int HeightTile { get; private set; }
        public static int WidthTile { get; private set; }
        public static int HeightTank { get; private set; }
        public static int WidthTank { get; private set; }
        public static int HeightShell { get; private set; }
        public static int WidthShell { get; private set; }
        public static int HeightBoard { get; private set; }
        public static int WidthBoard { get; private set; }
        public static int TimerInterval { get; private set; }
        public static int TimeAppearanceOfTank { get; private set; }
        public static int TimeBigDetonation { get; private set; }
        public static int CountLevel { get; private set; }
        public static TimeSpan DelayScrenPoints { get; private set; }
        public static TimeSpan DelayScrenLoadLevel { get; private set; }
        public static int TimeGameOver { get; private set; }
        public static char CharBrickWall { get { return '#'; } }
        public static char CharConcreteWall { get { return '@'; } }
        public static char CharWater { get { return '~'; } }
        public static char CharForest { get { return '%'; } }
        public static char CharIce { get { return '-'; } }

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

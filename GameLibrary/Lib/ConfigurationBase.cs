using System;

namespace SuperTank
{
    /// <summary>
    /// Game settings
    /// </summary>
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
            DelayScrenLoadLevel = TimeSpan.FromSeconds(3);

            CountLevel = 35;
        }
        /// <summary>
        /// The detonation time of the projectile
        /// </summary>
        public static int TimeDetonation { get; private  set; }
        /// <summary>
        /// Tile height
        /// </summary>
        public static int HeightTile { get; private set; }
        /// <summary>
        /// Tile width
        /// </summary>
        public static int WidthTile { get; private set; }
        /// <summary>
        /// Height of the tank
        /// </summary>
        public static int HeightTank { get; private set; }
        /// <summary>
        /// Width of the tank
        /// </summary>
        public static int WidthTank { get; private set; }
        /// <summary>
        /// Shell height
        /// </summary>
        public static int HeightShell { get; private set; }
        /// <summary>
        /// Shell width
        /// </summary>
        public static int WidthShell { get; private set; }
        /// <summary>
        /// Height of the playing field
        /// </summary>
        public static int HeightBoard { get; private set; }
        /// <summary>
        /// Width of the playing field
        /// </summary>
        public static int WidthBoard { get; private set; }
        /// <summary>
        /// Time to update the game timer (one game cycle)
        /// </summary>
        public static int TimerInterval { get; private set; }
        /// <summary>
        /// Time the appearance of the tank on the stage (in the form of a star)
        /// </summary>
        public static int TimeAppearanceOfTank { get; private set; }
        /// <summary>
        /// The time of the big bang (tank, eagle)
        /// </summary>
        public static int TimeBigDetonation { get; private set; }
        /// <summary>
        /// Number of levels
        /// </summary>
        public static int CountLevel { get; private set; }
        /// <summary>
        /// Time of loading effect level
        /// </summary>
        public static TimeSpan DelayScrenLoadLevel { get; private set; }
        /// <summary>
        /// Screen saver timeout Game Over
        /// </summary>
        public static int TimeGameOver { get; private set; }
        /// <summary>
        /// The brick wall symbol (on the map)
        /// </summary>
        public static char CharBrickWall { get { return '#'; } }
        /// <summary>
        /// The concrete wall symbol (on the map)
        /// </summary>
        public static char CharConcreteWall { get { return '@'; } }
        /// <summary>
        /// The water symbol (on the map)
        /// </summary>
        public static char CharWater { get { return '~'; } }
        /// <summary>
        /// The forest symbol (on the map)
        /// </summary>
        public static char CharForest { get { return '%'; } }
        /// <summary>
        /// The ice symbol (on the map)
        /// </summary>
        public static char CharIce { get { return '-'; } }
        /// <summary>
        /// The plain tank symbol (on the map)
        /// </summary>
        public static char CharPlainTank { get { return 'P'; } }
        /// <summary>
        /// The armored personnel carrier tank symbol (on the map)
        /// </summary>
        public static char CharArmoredPersonnelCarrierTank { get { return 'A'; } }
        /// <summary>
        /// The quick fire tank symbol (on the map)
        /// </summary>
        public static char CharQuickFireTank { get { return 'R'; } }
        /// <summary>
        /// The armored tank symbol (on the map)
        /// </summary>
        public static char CharArmoredTank { get { return 'B'; } }

        /// <summary>
        /// Returns the number of points
        /// </summary>
        /// <param name="type">Type tank</param>
        /// <returns>Number of points</returns>
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
        /// <summary>
        /// Calculates the time to display the players' screensaver
        /// </summary>
        /// <param name="countTank">Number of tanks</param>
        /// <returns>Time delay</returns>
        public static TimeSpan GetDelayScrenPoints(int countTank)
        {
            return TimeSpan.FromMilliseconds(150 * (countTank + 9) + 1000);
        }
    }
}

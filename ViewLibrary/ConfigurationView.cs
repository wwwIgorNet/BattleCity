using System;
using System.Drawing;
using System.Drawing.Text;

namespace SuperTank.View
{
    /// <summary>
    /// Game display settings
    /// </summary>
    public class ConfigurationView : ConfigurationBase
    {
        static ConfigurationView()
        {
            TimeScrenGameOver = 2000;
            DelayScrenRecord = TimeSpan.FromSeconds(8);
            DelayChangColorBonusTank = 6;

            WindowClientHeight = 28 * ConfigurationBase.HeightTile;
            WindowClientWidth = 31 * ConfigurationBase.WidthTile;
            
            ZIndexTank = 5;

            VolumeDefoult = 0.5f;
            VolumeIncrement = 0.1f;
        }

        /// <summary>
        /// Height of the client area of the window
        /// </summary>
        public static int WindowClientHeight { get; private set; }
        /// <summary>
        /// Width of the client area of the window
        /// </summary>
        public static int WindowClientWidth { get; private set; }
        /// <summary>
        /// Location of textures
        /// </summary>
        public static string TexturePath { get { return @"Content\Textures\"; } }
        /// <summary>
        /// Background color around the playing field(scene)
        /// </summary>
        public static Color BackColor { get { return Color.DimGray; } }
        /// <summary>
        /// Location of sound files
        /// </summary>
        public static string SoundPath { get { return @"Content\Sound\"; } }
        /// <summary>
        /// Location of the file with the maximum number of points
        /// </summary>
        public static string MaxPointsPath { get { return @"Content\MaxPoints"; } }
        /// <summary>
        /// The delay in changing the color of the bonus tank
        /// </summary>
        public static int DelayChangColorBonusTank { get; private set; }
        /// <summary>
        /// Zindex tanks
        /// </summary>
        public static int ZIndexTank { get; private set; }
        /// <summary>
        /// Time display screen game over
        /// </summary>
        public static int TimeScrenGameOver { get; private set; }
        /// <summary>
        /// Time to display the score screen
        /// </summary>
        public static TimeSpan DelayScrenRecord { get; private set; }
        /// <summary>
        /// Volume defoult
        /// </summary>
        public static float VolumeDefoult { get; private set; }
        public static float VolumeIncrement { get; private set; }
    }
}

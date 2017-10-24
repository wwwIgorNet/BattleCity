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
        private static FontFamily fontBattleCity;
        private static FontFamily fontBattleCityInfo;

        static ConfigurationView()
        {
            PrivateFontCollection fonts = new PrivateFontCollection();
            fonts.AddFontFile(@"Content\Font\BattleCityInfo.ttf");
            fonts.AddFontFile(@"Content\Font\BattleCity.ttf");

            fontBattleCity = new FontFamily("BATTLECITIES", fonts);
            fontBattleCityInfo = new FontFamily("BattleCityInfo", fonts);
            //fontBattleCityInfo = fonts.Families[0];

            FontGameOver = new Font(InfoFontFamily, 16);

            TimeScrenGameOver = 2000;
            DelayScrenRecord = TimeSpan.FromSeconds(8);
            DelayChangColorBonusTank = 6;

            WindowClientHeight = 28 * ConfigurationBase.HeightTile;
            WindowClientWidth = 31 * ConfigurationBase.WidthTile;
            
            ZIndexTank = 5;
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
        /// Font family for displaying information
        /// </summary>
        public static FontFamily InfoFontFamily { get { return fontBattleCityInfo; } }
        /// <summary>
        /// Family of fonts in the form of bricks
        /// </summary>
        public static FontFamily FontFamilyBattleCities { get { return fontBattleCity; } }
        /// <summary>
        /// Font for scren game over
        /// </summary>
        public static Font FontGameOver { get; private set; }
        /// <summary>
        /// Time display screen game over
        /// </summary>
        public static int TimeScrenGameOver { get; private set; }
        /// <summary>
        /// Time to display the score screen
        /// </summary>
        public static TimeSpan DelayScrenRecord { get; private set; }
    }
}

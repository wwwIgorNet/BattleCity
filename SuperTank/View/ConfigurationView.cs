using System;
using System.Drawing;
using System.Drawing.Text;

namespace SuperTank.View
{
    public class ConfigurationView : ConfigurationBase
    {
        private static PrivateFontCollection fonts = new PrivateFontCollection();

        static ConfigurationView()
        {
            fonts.AddFontFile(@"Content\Font\BattleCityInfo.ttf");
            fonts.AddFontFile(@"Content\Font\BattleCity.ttf");
            FontGameOver = new Font(InfoFontFamily, 16);

            TimeScrenGameOver = 2000;
            DelayScrenRecord = TimeSpan.FromSeconds(8);
            DelayChangColorBonusTank = 6;

            WindowClientHeight = 28 * ConfigurationBase.HeightTile;
            WindowClientWidth = 31 * ConfigurationBase.WidthTile;
            
            ZIndexTank = 5;
        }

        public static int WindowClientHeight { get; private set; }
        public static int WindowClientWidth { get; private set; }
        public static string TexturePath { get { return @"Content\Textures\"; } }
        public static Color BackColor { get { return Color.DimGray; } }
        public static string SoundPath { get { return @"Content\Sound\"; } }
        public static string MaxPointsPath { get { return @"Content\MaxPoints"; } }
        public static int DelayChangColorBonusTank { get; private set; }
        public static int ZIndexTank { get; private set; }
        public static FontFamily InfoFontFamily { get { return fonts.Families[1]; } }
        public static FontFamily FontFamilyBattleCities { get { return fonts.Families[0]; } }
        public static Font FontGameOver { get; private set; }
        public static int TimeScrenGameOver { get; private set; }
        public static TimeSpan DelayScrenRecord { get; private set; }
    }
}

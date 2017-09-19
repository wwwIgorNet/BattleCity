using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.View
{
    public class ConfigurationView : ConfigurationBase
    {
        private static int windowClientHeight = 28 * 20;
        private static int windowClientWidth = 31 * 20;
        private static int delayChangColorBonusTank = 6;
        private static int zIndexTank = 5;
        private static PrivateFontCollection fonts = new PrivateFontCollection();

        static ConfigurationView()
        {
            fonts.AddFontFile(@"Content\Font\BattleCityInfo.ttf");
            fonts.AddFontFile(@"Content\Font\BattleCity.ttf");
            FontGameOver = new Font(InfoFontFamily, 16);
            TimeScrenGameOver = 4000;
        }

        public static int WindowClientHeight { get { return windowClientHeight; } }
        public static int WindowClientWidth { get { return windowClientWidth; } }
        public static string TexturePath { get { return @"Content\Textures\"; } }
        public static Color BackColor { get { return Color.DimGray; } }
        public static string SoundPath { get { return @"Content\Sound\"; } }

        public static int DelayChangColorBonusTank { get { return delayChangColorBonusTank; } }

        public static int ZIndexTank { get { return zIndexTank; } }

        public static FontFamily InfoFontFamily { get { return fonts.Families[1]; } }
        public static FontFamily FontFamilyBattleCities { get { return fonts.Families[0]; } }

        public static Font FontGameOver { get; internal set; }
        public static int TimeScrenGameOver { get; internal set; }
    }
}

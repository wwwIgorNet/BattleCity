using System;
using System.Drawing;
using System.Drawing.Text;

namespace SuperTank.View
{
    /// <summary>
    /// Game display settings
    /// </summary>
    public class ConfigurationWinForms : ConfigurationView
    {
        private static FontFamily fontBattleCity;
        private static FontFamily fontBattleCityInfo;

        static ConfigurationWinForms()
        {
            PrivateFontCollection fonts = new PrivateFontCollection();
            fonts.AddFontFile(@"Content\Font\BattleCityInfo.ttf");
            fonts.AddFontFile(@"Content\Font\BattleCity.ttf");

            fontBattleCity = new FontFamily("BATTLECITIES", fonts);
            fontBattleCityInfo = new FontFamily("BattleCityInfo", fonts);

            FontGameOver = new Font(InfoFontFamily, 16);
        }
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
    }
}

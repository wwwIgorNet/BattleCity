using System;
using System.Collections.Generic;
using System.Drawing;
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

        public static int WindowClientHeight { get { return windowClientHeight; } }
        public static int WindowClientWidth { get { return windowClientWidth; } }
        public static string TexturePath { get { return @"Content\Textures\"; } }
        public static Color BackColor { get { return SystemColors.ActiveBorder; } }
        public static string SoundPath { get { return @"Content\Sound\"; } }

        public static int DelayChangColorBonusTank { get { return delayChangColorBonusTank; } }

        public static int ZIndexTank { get { return zIndexTank; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.View
{
    public class ConfigurationView : ConfigurationBase
    {
        private static int windowClientHeight = 28 * 20;
        private static int windowClientWidth = 31 * 20;
        private static int board = 20;

        public static int Boaed { get { return board; } }
        public static int WindowClientHeight { get { return windowClientHeight; } }
        public static int WindowClientWidth { get { return windowClientWidth; } }
        public static string Texture { get { return @"Content\Textures\"; } }
    }
}

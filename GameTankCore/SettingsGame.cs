using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTankCore
{
    public static class SettingsGame
    {
        private static int heightTile = 20;
        private static int widthTile = 20;
        private static int heightTank = 40;
        private static int widthTank = 40;
        private static int heightShell = 7;
        private static int widthShell = 5;
        private static int heightBoard = 28 * 20;
        private static int widthBoard = 31 * 20;
        private static int countLevel = 1;
        private static int timerInterval = 20;

        public static int HeightTile { get { return heightTile; } }
        public static int WidthTile { get { return widthTile; } }
        public static int HeigthTank { get { return heightTank; } }
        public static int WidthTank { get { return widthTank; } }
        public static int HeightShell { get { return heightShell; } }
        public static int WidthShell { get { return widthShell; } }
        public static int HeightBoard { get { return heightBoard; } }
        public static int WidthBoard { get { return widthBoard; } }
        /// <summary>
        /// Количество уровней
        /// </summary>
        public static int CountLevel { get { return countLevel; } }
        public static int TimerInterval { get { return timerInterval; } }
        public static string Content { get { return @"Content\Textures\"; } }
    }
}

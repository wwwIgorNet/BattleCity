﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class ConfigurationBase
    {
        protected ConfigurationBase() { }

        private static int heightTile = 20;
        private static int widthTile = 20;
        private static int heightTank = 40;
        private static int widthTank = 40;
        private static int heightShell = 7;
        private static int widthShell = 5;
        private static int heightBoard = 26 * 20;
        private static int widthBoard = 26 * 20;
        private static int windowClientHeight = 28 * 20;
        private static int windowClientWidth = 31 * 20;
        private static int timerInterval = 20;
        private static int velostyPlainTank = 3;
        private static int velostyShellPlainUserTank = 6;

        public static int VelostyPlainTank { get { return velostyPlainTank; } }
        public static int VelostyShellPlainUserTank { get { return velostyShellPlainUserTank; } }
        public static int HeightTile { get { return heightTile; } }
        public static int WidthTile { get { return widthTile; } }
        public static int HeigthTank { get { return heightTank; } }
        public static int WidthTank { get { return widthTank; } }
        public static int HeightShell { get { return heightShell; } }
        public static int WidthShell { get { return widthShell; } }
        public static int HeightBoard { get { return heightBoard; } }
        public static int WidthBoard { get { return widthBoard; } }
        public static int WindowClientHeight { get { return windowClientHeight; } }
        public static int WindowClientWidth { get { return windowClientWidth; } }
        public static int TimerInterval { get { return timerInterval; } }
    }
}
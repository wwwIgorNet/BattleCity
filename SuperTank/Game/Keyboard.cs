using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    /// <summary>
    /// Состояние клавиатуры
    /// </summary>
    public struct Keyboard
    {
        public static bool Enter { get; set; }
        public static bool Escape { get; set; }
        public static bool Left { get; set; }
        public static bool Right { get; set; }
        public static bool Down { get; set; }
        public static bool Up { get; set; }
        public static bool Space { get; set; }
    }
}

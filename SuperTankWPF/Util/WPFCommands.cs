using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SuperTankWPF.Util
{
    static class WPFCommands
    {
        private static RoutedCommand menuEnter;

        static WPFCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.Enter, ModifierKeys.None));
            menuEnter = new RoutedCommand(nameof(MenuEnter), typeof(WPFCommands), inputs);
        }

        public static RoutedCommand MenuEnter { get { return menuEnter; } }
    }
}

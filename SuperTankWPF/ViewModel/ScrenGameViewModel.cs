using GalaSoft.MvvmLight;
using SuperTank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace SuperTankWPF.ViewModel
{
    class ScrenGameViewModel : ObservableObject
    {
        private int level;
        private bool isShowAnimationNewLevel;
        private bool isShowGameOver;

        public int Level
        {
            get { return level; }
            set { Set(nameof(Level), ref level, value); }
        }
        public bool IsShowAnimationNewLevel
        {
            get { return isShowAnimationNewLevel; }
            set { Set(nameof(IsShowAnimationNewLevel), ref isShowAnimationNewLevel, value); }
        }
        public bool IsShowGameOver
        {
            get { return isShowGameOver; }
            set { Set(nameof(IsShowGameOver), ref isShowGameOver, value); }
        }
        public IKeyboard Keyboard { get; set; }

        public void Clear()
        {
            Level = 0;
            IsShowAnimationNewLevel = false;
            IsShowGameOver = false;
        }

        public void KeyDown(Key key)
        {
            Keyboard?.KeyDown(KeyConverter(key));
        }

        public void KeyUp(Key key)
        {
            Keyboard?.KeyUp(KeyConverter(key));
        }

        private Keys KeyConverter(Key key)
        {
            switch (key)
            {
                case Key.Up:
                    return Keys.Up;
                case Key.Down:
                    return Keys.Down;
                case Key.Left:
                    return Keys.Left;
                case Key.Right:
                    return Keys.Right;
                case Key.Space:
                    return Keys.Space;
            }

            return Keys.None;
        }
    }
}

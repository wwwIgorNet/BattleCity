using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GameLibrary.Lib;
using SuperTank;
using SuperTankWPF.Util;
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
    class ScreenGameViewModel : ObservableObject
    {
        private int level;
        private bool isShowAnimationNewLevel;
        private bool isShowGameOver;
        private bool isPause;
        private RelayCommand<bool> commandPause;

        public event Action<bool> ActionPause = isPause => { };

        public RelayCommand<bool> CommandPause
        {
            get
            {
                return commandPause ?? (commandPause = new RelayCommand<bool>(ActionPause));
            }
        }

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
        public bool IsPause
        {
            get { return isPause; }
            set { Set(nameof(IsPause), ref isPause, value); }
        }
        public IKeyboard Keyboard { get; set; }

        public void Clear()
        {
            ActionPause = (isPause) => { };
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

        private KeysGame KeyConverter(Key key)
        {
            switch (key)
            {
                case Key.Up:
                    return KeysGame.Up;
                case Key.Down:
                    return KeysGame.Down;
                case Key.Left:
                    return KeysGame.Left;
                case Key.Right:
                    return KeysGame.Right;
                case Key.Space:
                    return KeysGame.Space;
            }

            return KeysGame.None;
        }
    }
}

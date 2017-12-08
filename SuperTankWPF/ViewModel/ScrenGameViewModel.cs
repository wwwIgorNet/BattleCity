using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public void Clear()
        {
            Level = 0;
            IsShowAnimationNewLevel = false;
            IsShowGameOver = false;
        }
    }
}

using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTankWPF.ViewModel
{
    class ScrenGameViewModel : ObservableObject
    {
        private int level = 6;
        private bool isShowAnimationNewLevel = true;
        private bool isShowGameOver = true;

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
    }
}

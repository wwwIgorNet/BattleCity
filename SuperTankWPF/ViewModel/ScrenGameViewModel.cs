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

        public int Level
        {
            get { return level; }
            set { Set(nameof(Level), ref level, value); }
        }
    }
}

using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTankWPF.ViewModel
{
    class ScrenRecordViewModel : ObservableObject
    {
        private int countPoints = 567854;

        public int CountPoints
        {
            get { return countPoints; }
            set { Set(nameof(CountPoints), ref countPoints, value); }
        }
    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTankWPF.ViewModel
{
    class ScrenConstructionViewModel : ObservableObject
    {
        private char[,] map;

        public char[,] Map
        {
            get { return map; }
            set
            {
                Set(nameof(Map), ref map, value);
            }
        }
    }
}

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
            get => map;
            set
            {
                Set(nameof(Map), ref map, value);
            }
        }

        public bool HasMap { get => map != null; }

        public char[,] GetAndClerMap()
        {
            if (HasMap)
            {
                char[,] res = map;
                map = null;

                return res;
            }
            else return null;
        }
    }
}

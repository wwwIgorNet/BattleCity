using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTankWPF.ViewModel
{
    class ScreenConstructionViewModel : ObservableObject
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

        public bool HasMap { get { return map != null; } }

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

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
        public RelayCommand AddBrickWall = new RelayCommand(() => { });
        public RelayCommand AddConcreteWall = new RelayCommand(() => { });
        public RelayCommand AddForest = new RelayCommand(() => { });
        public RelayCommand AddWater = new RelayCommand(() => { });
        public RelayCommand AddIce = new RelayCommand(() => { });
    }
}

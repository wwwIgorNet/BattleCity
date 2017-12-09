using GalaSoft.MvvmLight;
using SuperTank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperTankWPF.Units
{
    class UnitViewModel : ObservableObject
    {
        private Dictionary<PropertiesType, Object> properties;
        private double x;
        private double y;

        public UnitViewModel(int id, TypeUnit typeUnit)
        {
            this.ID = id;
            TypeUnit = typeUnit;
        }
        
        public int ID { get; }
        public TypeUnit TypeUnit { get; }
        public Dictionary<PropertiesType, Object> Properties { get; set; }

        public double X
        {
            get { return x; }
            set { Set(nameof(X), ref x, value); }
        }
        public double Y
        {
            get { return y; }
            set { Set(nameof(Y), ref y, value); }
        }
    }
}

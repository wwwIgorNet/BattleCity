using GalaSoft.MvvmLight;
using SuperTank;
using SuperTank.View;
using SuperTankWPF.Model;
using SuperTankWPF.Units;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace SuperTankWPF.ViewModel
{
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class ScrenSceneViewModel : ObservableObject, IRender
    {
        public ScrenSceneViewModel()
        {
            t = new Timer(500);
            t.Elapsed += T_Elapsed;
            t.Start();
        }

        Timer t;
        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            Add(2, TypeUnit.Ice, 0, 0, null);
            Add(0, TypeUnit.ConcreteWall, 50, 50, null);
            Add(1, TypeUnit.Forest, 70, 70, null);
            Add(3, TypeUnit.BrickWall, 490, 500, null);

            t.Stop();
        }

        public ObservableCollection<UnitViewModel> Units { get; } = new ObservableCollection<UnitViewModel>();
        
        public void Add(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties)
        {
            Units.Add(new UnitViewModel(id, typeUnit) { X = x, Y = y });
        }

        public void AddRange(List<UnitDataForView> collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                UnitDataForView data = collection[i];
                Add(data.ID, data.TypeUnit, data.X, data.Y, data.Properties);
            }
        }

        public void Clear()
        {
            Units.Clear();
        }

        public void Remove(int id)
        {
            Units.Remove(Units.SingleOrDefault(uvm => uvm.ID == id));
        }

        public void Update(int id, PropertiesType prop, object value)
        {
            try
            {
                UnitViewModel unitViewModel = Units.FirstOrDefault(u => u.ID == id);

                switch (prop)
                {
                    case PropertiesType.X:
                        unitViewModel.X = (int)value;
                        break;
                    case PropertiesType.Y:
                        unitViewModel.Y = (int)value;
                        break;
                    //case PropertiesType.Direction:
                    //case PropertiesType.IsStop:
                    //case PropertiesType.Detonation:
                    //case PropertiesType.Glide:
                    default:
                        unitViewModel.Properties[prop] = value;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

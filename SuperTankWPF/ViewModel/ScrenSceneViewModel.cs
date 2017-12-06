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
            Timer t = new Timer(500);
            t.Elapsed += T_Elapsed;
            t.Start();
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            Add(0, TypeUnit.BrickWall, 50, 50, null);
            Add(1, TypeUnit.Forest, 70, 70, null);
            Add(2, TypeUnit.Ice, 0, 0, null);
        }

        public ObservableCollection<UnitViewModel> Units { get; } = new ObservableCollection<UnitViewModel>();




        //private SuperTank.FactoryViewUnit factoryViewUnit = new SuperTank.FactoryViewUnit();

        public void Add(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties)
        {
            Units.Add(new UnitViewModel(id, typeUnit) { X = x, Y = y });
            //ViewUnit view = factoryViewUnit.Create(id, x, y, typeUnit, properties);
            //board.Children.Add(view);
            //if (view != null) listDrowable.Add(view);
        }

        public void AddRange(List<UnitDataForView> collection)
        {
            //for (int i = 0; i < collection.Count; i++)
            //{
            //    UnitDataForView data = collection[i];
            //    Add(data.ID, data.TypeUnit, data.X, data.Y, data.Properties);
            //}
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {

        }

        public void Update(int id, PropertiesType prop, object value)
        {
            try
            {
                //BaseView viewUpdate = listDrowable.FindByID(id);
                //switch (prop)
                //{
                //    case PropertiesType.X:
                //        viewUpdate.X = (int)value;
                //        break;
                //    case PropertiesType.Y:
                //        viewUpdate.Y = (int)value;
                //        break;
                //    //case PropertiesType.Direction:
                //    //case PropertiesType.IsStop:
                //    //case PropertiesType.Detonation:
                //    //case PropertiesType.Glide:
                //    default:
                //        viewUpdate.Properties[prop] = value;
                //        break;
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

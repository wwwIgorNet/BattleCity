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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class ScrenSceneViewModel : ObservableObject, IRender
    {
        private IFactoryUnitView factoryUnitView;

        public ScrenSceneViewModel(IFactoryUnitView factoryUnitView)
        {
            this.factoryUnitView = factoryUnitView;
        }

        public ObservableCollection<UnitView> Units { get; } = new ObservableCollection<UnitView>();

        public void Add(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties)
        {
            Units.Add(factoryUnitView.Create(id, typeUnit, x, y, properties));
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
            Units.Remove(Units.First(uv => uv.ID == id));
        }

        public void Update(int id, PropertiesType prop, object value)
        {
            try
            {
                UnitView unitView = Units.FirstOrDefault(u => u.ID == id);

                switch (prop)
                {
                    case PropertiesType.X:
                        unitView.X = (int)value;
                        break;
                    case PropertiesType.Y:
                        unitView.Y = (int)value;
                        break;
                    case PropertiesType.Direction:
                        var direction = unitView as IDirection;
                        if (direction != null) direction.Direction = (Direction)value;
                        break;
                    case PropertiesType.IsParking:
                        var isParking = unitView as IParking;
                        if (isParking != null) isParking.IsParking = (bool)value;
                        break;
                    case PropertiesType.Detonation:
                        var detonation = unitView as IDetonation;
                        if (detonation != null) detonation.Detonation = (bool)value;
                        break;
                    case PropertiesType.IsInvulnerable:
                        var isInvulnerable = unitView as IInvulnerable;
                        if (isInvulnerable != null) isInvulnerable.IsInvulnerable = (bool)value;
                        break;
                    case PropertiesType.NumberOfHits:
                        var numberOfHits = unitView as INumberOfHits;
                        if (numberOfHits != null) numberOfHits.NumberOfHits = (int)value;
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

using GalaSoft.MvvmLight;
using GameBattleCity.Lib;
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
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace SuperTankWPF.ViewModel
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class ScreenSceneViewModel : ObservableObject, IRender
    {
        private IFactoryUnitView factoryUnitView;
        private SynchronizationContext synchronizationContext;

        public ScreenSceneViewModel(IFactoryUnitView factoryUnitView, SynchronizationContext synchronizationContext)
        {
            this.factoryUnitView = factoryUnitView;
            this.synchronizationContext = synchronizationContext;
        }

        public ObservableCollection<UnitView> Units { get; } = new ObservableCollection<UnitView>();

        public void Add(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties)
        {
            synchronizationContext.Post(s => Units.Add(factoryUnitView.Create(id, typeUnit, x, y, properties)), null);
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
            for (int i = Units.Count - 1; i >= 0; i--)
                Units.RemoveAt(i);
        }

        public void Remove(int id)
        {
            synchronizationContext.Post(s => Units.Remove(Units.FirstOrDefault(uv => uv.ID == id)), null);
        }

        public void Update(int id, PropertiesType prop, object value)
        {
            synchronizationContext.Post(s =>
            {
                Units.FirstOrDefault(u => u.ID == id)?.Update(prop, value);
            }, null);
        }
    }
}

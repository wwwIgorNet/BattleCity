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
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace SuperTankWPF.ViewModel
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class ScrenSceneViewModel : ObservableObject, IRender
    {
        private IFactoryUnitView factoryUnitView;
        private SynchronizationContext synchronizationContext;

        public ScrenSceneViewModel(IFactoryUnitView factoryUnitView, SynchronizationContext synchronizationContext)
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
            synchronizationContext.Post(s => Units.Remove(Units.First(uv => uv.ID == id)), null);
        }

        public void Update(int id, PropertiesType prop, object value)
        {
            try
            {
                synchronizationContext.Post(s =>
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
                            ((IDirection)unitView).Direction = (Direction)value;
                            break;
                        case PropertiesType.IsParking:
                            ((IParking)unitView).IsParking = (bool)value;
                            break;
                        case PropertiesType.Detonation:
                            IDetonation detonation = unitView as IDetonation;
                            if (detonation != null) detonation.Detonation = (bool)value;
                            break;
                        case PropertiesType.IsInvulnerable:
                            ((IInvulnerable)unitView).IsInvulnerable = (bool)value;
                            break;
                        case PropertiesType.NumberOfHits:
                            ((INumberOfHits)unitView).NumberOfHits = (int)value;
                            break;
                    }
                }, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

using GameBattleCity.Lib;
using System.Collections.Generic;
using System.ServiceModel;

namespace SuperTank.View
{
    public interface IRender
    {
        void Add(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties);
        void Remove(int id);
        void Clear();
        void Update(int id, PropertiesType prop, object value);
        void AddRange(List<UnitDataForView> collection);
    }
}
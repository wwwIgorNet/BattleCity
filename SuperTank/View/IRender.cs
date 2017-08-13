using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace SuperTank.View
{
    [ServiceContract]
    public interface IRender
    {
        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(Direction))]
        void Add(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties);
        [OperationContract(IsOneWay = true)]
        void Remove(int id);
        [OperationContract(IsOneWay = true)]
        void Clear();
        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(Direction))]
        void Update(int id, PropertiesType prop, object value);
        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(Direction))]
        void AddRange(List<UnitDataForView> collection);
    }
}
using System.Collections.Generic;
using System.ServiceModel;

namespace SuperTank.View
{
    /// <summary>
    /// Game field display interface
    /// </summary>
    [ServiceContract]
    public interface IRender
    {
        /// <summary>
        /// Add unit
        /// </summary>
        /// <param name="id">Id unit</param>
        /// <param name="typeUnit">Type unit</param>
        /// <param name="x">Coordinate x unit</param>
        /// <param name="y">Coordinate y unit</param>
        /// <param name="properties">Properties unit</param>
        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(Direction))]
        [ServiceKnownType(typeof(Owner))]
        void Add(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties);
        /// <summary>
        /// Remove unit
        /// </summary>
        /// <param name="id">ID unit</param>
        [OperationContract(IsOneWay = true)]
        void Remove(int id);
        /// <summary>
        /// Remove all units
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void Clear();
        /// <summary>
        /// Update unit
        /// </summary>
        /// <param name="id">ID unit</param>
        /// <param name="prop">Property type</param>
        /// <param name="value">Value property</param>
        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(Direction))]
        void Update(int id, PropertiesType prop, object value);
        /// <summary>
        /// Add renge units
        /// </summary>
        /// <param name="collection">Collectio units</param>
        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(Direction))]
        void AddRange(List<UnitDataForView> collection);
    }
}
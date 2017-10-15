using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SuperTank
{
    /// <summary>
    /// Properties unit for view
    /// </summary>
    [DataContract]
    public class UnitDataForView
    {
        [DataMember]
        private int id;
        [DataMember]
        private TypeUnit typeUnit;
        [DataMember]
        private int x;
        [DataMember]
        private int y;
        [DataMember]
        private Dictionary<PropertiesType, object> properties;

        public UnitDataForView()
        { }

        public UnitDataForView(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties)
        {
            this.id = id;
            this.typeUnit = typeUnit;
            this.x = x;
            this.y = y;
            this.properties = properties;
        }

        /// <summary>
        /// ID unit
        /// </summary>
        public int ID { get { return id; } }
        /// <summary>
        /// Type unit
        /// </summary>
        public TypeUnit TypeUnit { get { return typeUnit; } }
        /// <summary>
        /// Coordinate x unit
        /// </summary>
        public int X { get { return x; } }
        /// <summary>
        /// Coordinate y unit
        /// </summary>
        public int Y { get { return y; } }
        /// <summary>
        /// Other properties unit
        /// </summary>
        public Dictionary<PropertiesType, object> Properties { get { return properties; } }
    }
}

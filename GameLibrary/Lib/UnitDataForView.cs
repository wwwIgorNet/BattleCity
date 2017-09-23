using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SuperTank
{
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

        public int ID { get { return id; } }
        public TypeUnit TypeUnit { get { return typeUnit; } }
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public Dictionary<PropertiesType, object> Properties { get { return properties; } }
    }
}

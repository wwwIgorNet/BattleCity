using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace SuperTank
{
    [DataContract]
    public class Property
    {
        [DataMember]
        private PropertiesType type;
        public PropertiesType Type
        {
            get { return type; }
            set { type = value; }
        }

        [DataMember]
        private object val;
        public object Value
        {
            get { return val; }
            set { val = value; }
        }
    }
}

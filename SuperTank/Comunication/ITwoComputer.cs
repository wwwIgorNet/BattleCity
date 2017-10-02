using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Comunication
{
    [ServiceContract]
    interface ITwoComputer
    {
        [OperationContract(IsOneWay = true)]
        void StartedTwoComp();
    }
}

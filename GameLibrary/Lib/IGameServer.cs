using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace GameLibrary.Lib
{
    //[ServiceContract]
    public interface IGameServer
    {
        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(KeysGame))]
        void KeyDown(KeysGame key);

        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(KeysGame))]
        void KeyUp(KeysGame key);
    }
}

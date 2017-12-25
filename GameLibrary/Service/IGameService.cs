using GameLibrary.Lib;
using SuperTank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace GameLibrary.Service
{
    [ServiceContract(CallbackContract = typeof(IGameClient))]
    public interface IGameService
    {
        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(KeysGame))]
        [ServiceKnownType(typeof(Owner))]
        void KeyDown(KeysGame key, Owner owner);

        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(KeysGame))]
        [ServiceKnownType(typeof(Owner))]
        void KeyUp(KeysGame key, Owner owner);

        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(Owner))]
        void Connect(Owner owner);

        [OperationContract(IsOneWay = true)]
        void PauseGame(bool isPause);
    }
}

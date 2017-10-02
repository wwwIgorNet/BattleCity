using System.Collections.Generic;
using System.ServiceModel;

namespace SuperTank
{
    [ServiceContract]
    public interface IGameInfo
    {
        [OperationContract(IsOneWay = true)]
        void EndLevel(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr, int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr);
        [OperationContract(IsOneWay = true)]
        void StartLevel(int level);
        [OperationContract(IsOneWay = true)]
        void GameOver();
        [OperationContract(IsOneWay = true)]
        void SetCountTankEnemy(int count);
        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(Owner))]
        void SetCountTankPlaeyr(int count, Owner owner);
    }
}

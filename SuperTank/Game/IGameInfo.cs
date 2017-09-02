using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    [ServiceContract]
    public interface IGameInfo
    {
        [OperationContract(IsOneWay = true)]
        void EndLevel(int countPoints, Dictionary<TypeUnit, int> destrouTanksPlaeyr);
        [OperationContract(IsOneWay = true)]
        void StartLevel(int level);
        [OperationContract(IsOneWay = true)]
        void GameOver();

        [OperationContract(IsOneWay = true)]
        void SetCountTankEnemy(int count);
        [OperationContract(IsOneWay = true)]
        void SetCountTankPlaeyr(int count);
    }
}

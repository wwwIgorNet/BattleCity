using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SuperTank;
using GameBattleCity.Lib;

namespace GameLibrary.Lib
{
    [ServiceContract]
    public interface IGameClient
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



        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(Direction))]
        [ServiceKnownType(typeof(TypeBlickWall))]
        [ServiceKnownType(typeof(Owner))]
        void Add(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties);

        [OperationContract(IsOneWay = true)]
        void Remove(int id);

        [OperationContract(IsOneWay = true)]
        void Clear();

        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(Direction))]
        [ServiceKnownType(typeof(TypeBlickWall))]
        void Update(int id, PropertiesType prop, object value);

        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(Direction))]
        void AddRange(List<UnitDataForView> collection);



        [OperationContract(IsOneWay = true)]
        void DetonationTank();

        [OperationContract(IsOneWay = true)]
        void DetonationEagle();

        [OperationContract(IsOneWay = true)]
        void DetonationShell();

        [OperationContract(IsOneWay = true)]
        void Fire();

        [OperationContract(IsOneWay = true)]
        void TwoFire();

        [OperationContract(IsOneWay = true)]
        void Glide();

        [OperationContract(IsOneWay = true)]
        void Move();

        [OperationContract(IsOneWay = true)]
        void Stop();

        [OperationContract(IsOneWay = true)]
        void DetonationBrickWall();

        [OperationContract(IsOneWay = true)]
        void TankSoundStop();

        [OperationContract(IsOneWay = true)]
        void Bonus();

        [OperationContract(IsOneWay = true)]
        void NewBonus();
    }
}

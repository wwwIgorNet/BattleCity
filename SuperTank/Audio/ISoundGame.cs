using System.ServiceModel;

namespace SuperTank.Audio
{
    [ServiceContract]
    public interface ISoundGame
    {
        [OperationContract(IsOneWay = true)]
        void GameOver();
        [OperationContract(IsOneWay = true)]
        void GameStart();
        [OperationContract(IsOneWay = true)]
        void DetonationTank();
        [OperationContract(IsOneWay = true)]
        void DetonationEagle();
        [OperationContract(IsOneWay = true)]
        void DetonationShell();
        [OperationContract(IsOneWay = true)]
        void Fire();
        [OperationContract(IsOneWay = true)]
        void Fire2();
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
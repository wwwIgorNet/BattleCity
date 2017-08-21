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
        void BigDetonation();
        [OperationContract(IsOneWay = true)]
        void DetonationEagle();
        [OperationContract(IsOneWay = true)]
        void DetonationShell();
        [OperationContract(IsOneWay = true)]
        void Fire();
        [OperationContract(IsOneWay = true)]
        void Glide();
        [OperationContract(IsOneWay = true)]
        void Move();
        [OperationContract(IsOneWay = true)]
        void Stop();
        [OperationContract(IsOneWay = true)]
        void DetonationBrickWall();
        [OperationContract(IsOneWay = true)]
        void TankDispouse();
    }
}
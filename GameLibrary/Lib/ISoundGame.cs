using System.ServiceModel;

namespace SuperTank.Audio
{
    /// <summary>
    /// Interface sound
    /// </summary>
    [ServiceContract]
    public interface ISoundGame
    {
        /// <summary>
        /// The sound of a detonation tank
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void DetonationTank();
        /// <summary>
        /// The sound of the eagle's detonation
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void DetonationEagle();
        /// <summary>
        /// The sound of detonation of a projectile
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void DetonationShell();
        /// <summary>
        /// Shot of the tank
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void Fire();
        /// <summary>
        /// Double shot of the tank
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void TwoFire();
        /// <summary>
        /// Sliding the tank over the ice
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void Glide();
        /// <summary>
        /// Movement of the tank
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void Move();
        /// <summary>
        /// The sound of a stopped tank
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void Stop();
        /// <summary>
        /// The sound detonation bric wWall
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void DetonationBrickWall();
        /// <summary>
        /// Stop all sounds of the tank
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void TankSoundStop();
        [OperationContract(IsOneWay = true)]
        void Bonus();
        /// <summary>
        /// The appearance of a new bonus
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void NewBonus();
    }
}
using System.ServiceModel;

namespace SuperTank.Audio
{
    public interface ISoundGame
    {
        void DetonationTank();
        void DetonationEagle();
        void DetonationShell();
        void Fire();
        void TwoFire();
        void Glide();
        void Move();
        void StopSondTank();
        void DetonationBrickWall();
        void TankSoundStopMove();
        void Bonus();
        void NewBonus();
        void StopAll();
    }
}
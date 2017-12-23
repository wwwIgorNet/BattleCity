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
        void Stop();
        void DetonationBrickWall();
        void TankSoundStop();
        void Bonus();
        void NewBonus();
    }
}
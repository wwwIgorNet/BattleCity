using GameLibrary.Lib;
using GameLibrary.Service;
using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBattleCity.Service
{
    public class SoundForServices : ISoundGame
    {
        private IGameClient gameClient;

        public SoundForServices(IGameClient gameClient)
        {
            this.gameClient = gameClient;
        }

        public void Bonus()
        {
            gameClient.Bonus();
        }

        public void DetonationBrickWall()
        {
            gameClient.DetonationBrickWall();
        }

        public void DetonationEagle()
        {
            gameClient.DetonationEagle();
        }

        public void DetonationShell()
        {
            gameClient.DetonationShell();
        }

        public void DetonationTank()
        {
            gameClient.DetonationTank();
        }

        public void Fire()
        {
            gameClient.Fire();
        }

        public void Glide()
        {
            gameClient.Glide();
        }

        public void Move()
        {
            gameClient.Move();
        }

        public void NewBonus()
        {
            gameClient.NewBonus();
        }

        public void Stop()
        {
            gameClient.Stop();
        }

        public void TankSoundStop()
        {
            gameClient.TankSoundStop();
        }

        public void TwoFire()
        {
            gameClient.TwoFire();
        }
    }
}

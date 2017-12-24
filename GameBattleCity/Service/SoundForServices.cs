using GameLibrary.Lib;
using GameLibrary.Service;
using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace GameBattleCity.Service
{
    public class SoundForServices : ISoundGame
    {
        private IContextChannel playerContextChanel;
        private IGameClient gameClient;

        public SoundForServices(OperationContext playerContext)
        {
            this.gameClient = playerContext.GetCallbackChannel<IGameClient>();
            this.playerContextChanel = playerContext.Channel;
        }

        public void Bonus()
        {
            if (playerContextChanel.State == CommunicationState.Opened)
                gameClient.Bonus();
        }

        public void DetonationBrickWall()
        {
            if (playerContextChanel.State == CommunicationState.Opened)
                gameClient.DetonationBrickWall();
        }

        public void DetonationEagle()
        {
            if (playerContextChanel.State == CommunicationState.Opened)
                gameClient.DetonationEagle();
        }

        public void DetonationShell()
        {
            if (playerContextChanel.State == CommunicationState.Opened)
                gameClient.DetonationShell();
        }

        public void DetonationTank()
        {
            if (playerContextChanel.State == CommunicationState.Opened)
                gameClient.DetonationTank();
        }

        public void Fire()
        {
            if (playerContextChanel.State == CommunicationState.Opened)
                gameClient.Fire();
        }

        public void Glide()
        {
            if (playerContextChanel.State == CommunicationState.Opened)
                gameClient.Glide();
        }

        public void Move()
        {
            if (playerContextChanel.State == CommunicationState.Opened)
                gameClient.Move();
        }

        public void NewBonus()
        {
            if (playerContextChanel.State == CommunicationState.Opened)
                gameClient.NewBonus();
        }

        public void StopSondTank()
        {
            if (playerContextChanel.State == CommunicationState.Opened)
                gameClient.Stop();
        }

        public void TankSoundStop()
        {
            if (playerContextChanel.State == CommunicationState.Opened)
                gameClient.TankSoundStop();
        }

        public void TwoFire()
        {
            if (playerContextChanel.State == CommunicationState.Opened)
                gameClient.TwoFire();
        }
    }
}

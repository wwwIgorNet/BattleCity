using GameLibrary.Service;
using SuperTank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static GameBattleCity.Service.ExtendThreadPool;

namespace GameBattleCity.Service
{
    class GameInfoTwoPlayers : IGameInfo
    {
        private IContextChannel IIPlayerContextChannel;
        private IGameClient IPlayer;
        private IGameClient IIPlayer;

        public GameInfoTwoPlayers(OperationContext IPlayerContext, OperationContext IIPlayerContext)
        {
            this.IPlayer = IPlayerContext.GetCallbackChannel<IGameClient>();
            this.IIPlayer = IIPlayerContext.GetCallbackChannel< IGameClient>();
            this.IIPlayerContextChannel = IIPlayerContext.Channel;
        }
        public void EndLevel(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr, int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr)
        {
            InvokWithTryCatch(() =>
            {
                IPlayer.EndLevel(level, countPointsIPlayer, destrouTanksIPlaeyr, countPointsIIPlayer, destrouTanksIIPlaeyr);
                if (IIPlayerContextChannel.State == CommunicationState.Opened)
                    IIPlayer.EndLevel(level, countPointsIPlayer, destrouTanksIPlaeyr, countPointsIIPlayer, destrouTanksIIPlaeyr);
            });
        }

        public void GameOver()
        {
            InvokWithTryCatch(() =>
            {
                IPlayer.GameOver();
                if (IIPlayerContextChannel.State == CommunicationState.Opened)
                    IIPlayer.GameOver();
            });
        }

        public void SetCountTankEnemy(int count)
        {
            InvokWithTryCatch(() =>
            {
                IPlayer.SetCountTankEnemy(count);
                if (IIPlayerContextChannel.State == CommunicationState.Opened)
                    IIPlayer.SetCountTankEnemy(count);
            });
        }

        public void SetCountTankPlaeyr(int count, Owner owner)
        {
            InvokWithTryCatch(() =>
            {
                IPlayer.SetCountTankPlaeyr(count, owner);
                if (IIPlayerContextChannel.State == CommunicationState.Opened)
                    IIPlayer.SetCountTankPlaeyr(count, owner);
            });
        }

        public void StartGame()
        {
            InvokWithTryCatch(() =>
            {
                IPlayer.StartGame();
                if (IIPlayerContextChannel.State == CommunicationState.Opened)
                    IIPlayer.StartGame();
            });
        }

        public void StartLevel(int level)
        {
            InvokWithTryCatch(() =>
            {
                IPlayer.StartLevel(level);
                if (IIPlayerContextChannel.State == CommunicationState.Opened)
                    IIPlayer.StartLevel(level);
            });
        }

        public void PauseGame(bool isPause)
        {
            InvokWithTryCatch(() =>
            {
                IPlayer.PauseGame(isPause);
                if (IIPlayerContextChannel.State == CommunicationState.Opened)
                    IIPlayer.PauseGame(isPause);
            });
        }
    }
}

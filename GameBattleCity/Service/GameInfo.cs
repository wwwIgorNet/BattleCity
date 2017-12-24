using GameLibrary.Lib;
using GameLibrary.Service;
using SuperTank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace GameBattleCity.Service
{
    public class GameInfo : IGameInfo
    {
        private IGameClient gameClient;

        public GameInfo(OperationContext gameClientContext)
        {
            this.gameClient = gameClientContext.GetCallbackChannel<IGameClient>();
        }

        public void EndLevel(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr, int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr)
        {
            gameClient.EndLevel(level, countPointsIPlayer, destrouTanksIPlaeyr, countPointsIIPlayer, destrouTanksIIPlaeyr);
        }

        public void GameOver()
        {
            gameClient.GameOver();
        }

        public void SetCountTankEnemy(int count)
        {
            gameClient.SetCountTankEnemy(count);
        }

        public void SetCountTankPlaeyr(int count, Owner owner)
        {
            gameClient.SetCountTankPlaeyr(count, owner);
        }

        public void StartGame()
        {
            gameClient.StartGame();
        }

        public void StartLevel(int level)
        {
            gameClient.StartLevel(level);
        }
    }
}

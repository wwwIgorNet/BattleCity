using SuperTank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameBattleCity.TwoPlayers
{
    class GameInfoTwoPlayers : IGameInfo
    {
        private IGameInfo IPlayer;
        private IGameInfo IIPlayer;

        public GameInfoTwoPlayers(IGameInfo IPlayer, IGameInfo IIPlayer)
        {
            this.IPlayer = IPlayer;
            this.IIPlayer = IIPlayer;
        }

        public void EndLevel(int level, int countPoints, Dictionary<TypeUnit, int> destrouTanksPlaeyr)
        {
            ThreadPool.QueueUserWorkItem(s =>
            {
                IPlayer.EndLevel(level, countPoints, destrouTanksPlaeyr);
                IIPlayer.EndLevel(level, countPoints, destrouTanksPlaeyr);
            });
        }

        public void GameOver()
        {
            ThreadPool.QueueUserWorkItem(s =>
            {
                IPlayer.GameOver();
                IIPlayer.GameOver();
            });
        }

        public void SetCountTankEnemy(int count)
        {
            ThreadPool.QueueUserWorkItem(s =>
            {
                IPlayer.SetCountTankEnemy(count);
                IIPlayer.SetCountTankEnemy(count);
            });
        }

        public void SetCountTankPlaeyr(int count, Owner owner)
        {
            ThreadPool.QueueUserWorkItem(s =>
            {
                IPlayer.SetCountTankPlaeyr(count, owner);
                IIPlayer.SetCountTankPlaeyr(count, owner);
            });
        }

        public void StartLevel(int level)
        {
            ThreadPool.QueueUserWorkItem(s =>
            {
                IPlayer.StartLevel(level);
                IIPlayer.StartLevel(level);
            });
        }
    }
}

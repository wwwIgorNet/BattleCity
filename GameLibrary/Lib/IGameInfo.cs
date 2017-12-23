using System.Collections.Generic;
using System.ServiceModel;

namespace SuperTank
{
    /// <summary>
    /// Displaying game information
    /// </summary>
    public interface IGameInfo
    {
        void EndLevel(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr, int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr);
        void StartLevel(int level);
        void GameOver();
        void StartGame();
        void SetCountTankEnemy(int count);
        void SetCountTankPlaeyr(int count, Owner owner);
    }
}

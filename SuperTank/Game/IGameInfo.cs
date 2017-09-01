using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public interface IGameInfo
    {
        void EndLevel(int countPoints, Dictionary<TypeUnit, int> destrouTanksPlaeyr);
        void StartLevel(int level);
        void GameOver();

        void SetCountTankEnemy(int count);
        void SetCountTankPlaeyr(int count);
    }
}

using System.Collections.Generic;
using System.ServiceModel;

namespace SuperTank
{
    /// <summary>
    /// Displaying game information
    /// </summary>
    [ServiceContract]
    public interface IGameInfo
    {
        /// <summary>
        /// End of level
        /// </summary>
        /// <param name="level">Level number</param>
        /// <param name="countPointsIPlayer">Number of first player points</param>
        /// <param name="destrouTanksIPlaeyr">The number of destroyed tanks by the first player</param>
        /// <param name="countPointsIIPlayer">Number of points of the second player</param>
        /// <param name="destrouTanksIIPlaeyr">The number of destroyed tanks by the second player</param>
        [OperationContract(IsOneWay = true)]
        void EndLevel(int level, int countPointsIPlayer, Dictionary<TypeUnit, int> destrouTanksIPlaeyr, int countPointsIIPlayer, Dictionary<TypeUnit, int> destrouTanksIIPlaeyr);
        /// <summary>
        /// Start level
        /// </summary>
        /// <param name="level">Level number</param>
        [OperationContract(IsOneWay = true)]
        void StartLevel(int level);
        /// <summary>
        /// Game over
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void GameOver();
        /// <summary>
        /// Number of enemy tanks
        /// </summary>
        /// <param name="count">Number tanks</param>
        [OperationContract(IsOneWay = true)]
        void SetCountTankEnemy(int count);
        /// <summary>
        /// Number of player tanks
        /// </summary>
        /// <param name="count">Number tanks</param>
        /// <param name="owner">Owner (player)</param>
        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(Owner))]
        void SetCountTankPlaeyr(int count, Owner owner);
    }
}

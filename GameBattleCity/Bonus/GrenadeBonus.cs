using SuperTank.Audio;
using System.Collections.Generic;

namespace SuperTank
{
    /// <summary>
    /// Bonus grenade, explodes all enemy tanks on stage
    /// </summary>
    class GrenadeBonus
    {
        /// <summary>
        /// Explodes all enemy tanks on stage
        /// </summary>
        /// <param name="soundGame">Sound with detonation</param>
        public static void DetonationAllTankInScene(ISoundGame soundGame)
        {
            List<Unit> dispousTank = new List<Unit>();
            for (int j = 0; j < Scene.Tanks.Count; j++)
            {
                if ((Owner)Scene.Tanks[j].Properties[PropertiesType.Owner] == Owner.Enemy)
                    dispousTank.Add(Scene.Tanks[j]);
            }
            dispousTank.ForEach(u =>
            {
                u.Dispose();
                FactoryUnit.CreateBigDetonation(u, TypeUnit.BigDetonation).Start();
            });
            soundGame.DetonationTank();
        }
    }
}

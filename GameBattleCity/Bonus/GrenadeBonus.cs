using SuperTank.Audio;
using System.Collections.Generic;

namespace SuperTank
{
    class GrenadeBonus
    {
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

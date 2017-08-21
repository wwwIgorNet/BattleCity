using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class Eagle : Unit
    {
        private readonly ISoundGame soundGame;

        public Eagle(int id, int x, int y, int width, int height, TypeUnit type, ISoundGame soundGame) : base(id, x, y, width, height, type)
        {
            this.soundGame = soundGame;
            this.Properties[PropertiesType.Detonation] = false;
        }

        public override void Dispose()
        {
            if ((bool)Properties[PropertiesType.Detonation])
            {
                Properties[PropertiesType.Detonation] = true;
                soundGame.DetonationEagle();
                FactoryUnit.CreateBigDetonation(this, TypeUnit.BigDetonation).Start();
            }
        }
    }
}

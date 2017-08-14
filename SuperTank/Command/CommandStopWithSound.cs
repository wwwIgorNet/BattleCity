using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Command
{
    class CommandStopWithSound : BaseOffsetCommand
    {
        private readonly ISoundGame soundGame;

        public CommandStopWithSound(Unit unit, ISoundGame soundGame) : base(unit)
        {
            this.soundGame = soundGame;
        }

        public override void Execute()
        {
            if (!(bool)Unit.Properties[PropertiesType.IsParking])
            {
                bool isStop = OffsetToBorderTile();
                Unit.Properties[PropertiesType.IsParking] = isStop;
                if (isStop) soundGame.Stop();
            }


        }
    }
}

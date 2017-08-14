using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Command
{
    class CommandFireWithSound : CommandFire
    {
        private ISoundGame soundGame;

        public CommandFireWithSound(Unit unit, IFactoryUnit factoryUnit, IScene scene, int velosityFire, ISoundGame soundGame) : base(unit, factoryUnit, scene, velosityFire)
        {
            this.soundGame = soundGame;
        }

        protected override Unit PrevShell
        {
            set
            {
                base.PrevShell = value;
                if (value != null)
                {
                    soundGame.Fire();
                    value.Commands.Replace(TypeCommand.Move, new CommandMoveSellWithSound((CommandMoveSell)value.Commands.Get(TypeCommand.Move), soundGame));
                }
            }
        }
    }
}

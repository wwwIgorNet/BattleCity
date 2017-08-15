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

        protected override Unit CreateShell(int x, int y)
        {
            Unit shell = base.CreateShell(x, y);
            shell.Commands.Replace(TypeCommand.Move, new CommandMoveSellWithSound((CommandMoveSell)shell.Commands.Get(TypeCommand.Move), soundGame));
            soundGame.Fire();
            return shell;
        }
    }
}

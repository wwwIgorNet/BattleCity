using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Command
{
    class CommandMoveSellWithSound : CommandMoveSell
    {
        private ISoundGame soundGame;

        public CommandMoveSellWithSound(CommandMoveSell command, ISoundGame soundGame) : base(command.Unit, command.Scene, command.ShellDetonaition)
        {
            this.soundGame = soundGame;
        }

        protected override void Detonation(Unit item)
        {
            base.Detonation(item);
            if (item == null)
            {
                soundGame.DetonationShell();
                return;
            }
            switch (item.Type)
            {
                case TypeUnit.PainTank:
                    break;
                case TypeUnit.Shell:
                    break;
                case TypeUnit.BrickWall:
                    soundGame.DetonationBrickWall();
                    break;
                case TypeUnit.ConcreteWall:
                    soundGame.DetonationShell();
                    break;
                case TypeUnit.Eagle:
                    break;
            }
        }
    }
}

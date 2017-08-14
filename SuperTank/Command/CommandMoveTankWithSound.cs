using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Command
{
    class CommandMoveTankWithSound : CommandMoveTank
    {
        private readonly ISoundGame soundGame;

        public CommandMoveTankWithSound(Unit unit, IScene scene, ISoundGame soundGame) : base(unit, scene)
        {
            this.soundGame = soundGame;
        }

        protected override bool IsParcing
        {
            set
            {
                if (base.IsParcing != value)
                {
                    base.IsParcing = value;
                    if (value) soundGame.Stop();
                    else soundGame.Move();
                }
            }
        }

        protected override bool IsGlide
        {
            set
            {
                if (base.IsGlide != value)
                {
                    base.IsGlide = value;
                    if (value) soundGame.Glide();
                }
            }
        }
    }
}

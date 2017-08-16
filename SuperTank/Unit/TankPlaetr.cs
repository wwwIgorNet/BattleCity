using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class TankPlaetr : Tank
    {
        private ISoundGame soundGame;

        public TankPlaetr(int id, int x, int y, int width, int height, TypeUnit type, IScene scene, int velosity, Direction direction, int velosityShell, ISoundGame soundGame) : base(id, x, y, width, height, type, scene, velosity, direction, velosityShell)
        {
            this.soundGame = soundGame;
        }

        protected override bool IsParking
        {
            set
            {
                if (base.IsParking != value)
                {
                    base.IsParking = value;
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

        protected override Shell GetShell(int x, int y, int velosityShell, IScene scene)
        {
            soundGame.Fire();
            return new PlaeyrShell(Unit.NextID, x, y, ConfigurationGame.WidthShell, ConfigurationGame.HeightShell, TypeUnit.Shell, velosityShell, Direction, this, scene, soundGame);
        }
    }
}

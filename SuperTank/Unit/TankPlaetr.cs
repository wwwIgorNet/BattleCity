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

        public TankPlaetr(int id, int x, int y, int width, int height, TypeUnit type, IScene scene, IFactoryUnit factoryUnit, int velosity, Direction direction, int velosityShell, ISoundGame soundGame) : base(id, x, y, width, height, type, scene, factoryUnit, velosity, direction, velosityShell)
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

        public override bool Fire()
        {
            bool isFire = base.Fire();
            if (isFire) soundGame.Fire();

            return isFire;
        }
    }
}

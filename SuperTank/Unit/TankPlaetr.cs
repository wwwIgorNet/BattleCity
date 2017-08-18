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

        public TankPlaetr(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, IDriver driver, TypeUnit typeShell, ISoundGame soundGame) : base(id, x, y, width, height, type, velosity, direction, driver, typeShell)
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

        protected override Shell GetShell(int x, int y)
        {
            soundGame.Fire();
            return FactoryUnit.CreateShell(x, y, TypeUnit.Shell, Direction, this, soundGame);
        }

        public override void Dispose()
        {
            base.Dispose();
            soundGame.TankDispouse();
            soundGame.BigDetonation();
        }
    }
}

using SuperTank.Audio;

namespace SuperTank
{
    class TwoShellTank : TankPlayer
    {
        public TwoShellTank(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, IDriver driver, TypeUnit typeShell, int velosityShell, ISoundGame soundGame, Player plaeyr) : base(id, x, y, width, height, type, velosity, direction, driver, typeShell, velosityShell, soundGame, plaeyr)
        {
        }

        public Shell Shell2 { get; set; }

        public override bool TryFire()
        {
            if (Shell2 != null) return false;

            return base.TryFire();
        }

        protected override void Fire()
        {
            base.FireWithoutSound();

            Shell2 = GetShell(VelosityShell - 2);
            Shell2.UnitDisposable += u => { Shell2 = null; };
            Shell2.Start();
            SoundGame.TwoFire();
        }
    }
}

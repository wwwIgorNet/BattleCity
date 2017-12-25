using SuperTank.Audio;
using System.Drawing;

namespace SuperTank
{
    public class TankPlayer : Tank
    {
        private ISoundGame soundGame;

        public TankPlayer(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, IDriver driver, TypeUnit typeShell, int velosityShell, ISoundGame soundGame, Player plaeyr) : base(id, x, y, width, height, type, velosity, direction, driver, typeShell, velosityShell)
        {
            this.soundGame = soundGame;
            this.OwnerPlaeyr = plaeyr;
            Properties[PropertiesType.Owner] = plaeyr.Owner;
        }

        public Player OwnerPlaeyr { get; private set; }
        public ISoundGame SoundGame { get { return soundGame; } }
        public override bool IsParking
        {
            set
            {
                if (base.IsParking != value)
                {
                    base.IsParking = value;
                    if (value) soundGame.StopSondTank();
                    else soundGame.Move();
                }
            }
        }

        public override void Turn(Direction dir)
        {
            base.Turn(dir);
            // если направление изменилось и танк на льду
            if (OnIce && Direction == dir)
                soundGame.Glide();
        }
        public override void Dispose()
        {
            base.Dispose();
            soundGame.TankSoundStopMove();
            soundGame.DetonationEagle();
            OwnerPlaeyr.CurrentTank = null;
            OwnerPlaeyr.TryAddToScene();
        }
        public override void Move()
        {
            base.Move();
            for (int i = 0; i < Scene.Bonus.Count; i++)
            {
                if (BoundingBox.IntersectsWith(Scene.Bonus[i].BoundingBox))
                {
                    switch (Scene.Bonus[i].Type)
                    {
                        case TypeUnit.Clock:
                            new ClockBonus(ConfigurationGame.TimeClockBonus).Start();
                            break;
                        case TypeUnit.Grenade:
                            GrenadeBonus.DetonationAllTankInScene(soundGame);
                            break;
                        case TypeUnit.Helmet:
                            new HelmetBonus(this, ConfigurationGame.TimeHelmetBonus).Start();
                            break;
                        case TypeUnit.Pistol:
                            PistolBonus.UpdatableTank(this);
                            break;
                        case TypeUnit.Shovel:
                            new ShovelBonus().Start();
                            break;
                        case TypeUnit.StarMedal:
                            StarMedalBonus.UpdatableTank(this);
                            break;
                        case TypeUnit.Tank:
                            OwnerPlaeyr.CountTank++;
                            break;
                    }
                    soundGame.Bonus();
                    int points = ConfigurationGame.GetCountPoints(Scene.Bonus[i].Type);
                    OwnerPlaeyr.Points += points;
                    FactoryUnit.CreatePoints(Scene.Bonus[i].X, Scene.Bonus[i].Y, TypeUnit.Points, points).Start();
                    Scene.Bonus[i].Dispose();
                }
            }
        }

        #region Fire
        protected void FireWithoutSound()
        {
            base.Fire();
        }
        protected override void Fire()
        {
            base.Fire();
            soundGame.Fire();
        }
        protected override Shell GetShell(int velosityShell)
        {
            Point pos = GetCoordNewShell();
            return FactoryUnit.CreateShell(pos.X, pos.Y, TypeShell, Direction, velosityShell, this, soundGame);
        }
        #endregion
    }
}

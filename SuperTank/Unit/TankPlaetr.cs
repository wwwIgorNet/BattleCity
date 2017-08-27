using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SuperTank
{
    public class TankPlaetr : Tank
    {
        private ISoundGame soundGame;

        public TankPlaetr(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, IDriver driver, TypeUnit typeShell, int velosityShell, ISoundGame soundGame, Plaeyr plaeyr) : base(id, x, y, width, height, type, velosity, direction, driver, typeShell, velosityShell)
        {
            this.soundGame = soundGame;
            this.OwnerPlaeyr = plaeyr;
        }

        public Plaeyr OwnerPlaeyr { get; }
        public ISoundGame SoundGame { get { return soundGame; } }

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

        protected override void StartNewShell(int x, int y, TypeUnit type, int velosityShell)
        {
            Shell = FactoryUnit.CreateShell(x, y, type, Direction, velosityShell, this, soundGame);
            Shell.Start();
            soundGame.Fire();
        }

        public override void Dispose()
        {
            base.Dispose();
            soundGame.TankDispouse();
            soundGame.BigDetonation();
            OwnerPlaeyr.AddToScene();
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
                            new ClockBonus().Start();
                            break;
                        case TypeUnit.Grenade:
                            GrenadeBonus.DetonationAllTankInScene(soundGame);
                            break;
                        case TypeUnit.Helmet:
                            new HelmetBonus(this, 10).Start();
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
                    OwnerPlaeyr.Points += 500;
                    FactoryUnit.CreatePoints(Scene.Bonus[i].X, Scene.Bonus[i].Y, TypeUnit.Points, 500).Start();
                    Scene.Bonus[i].Dispose();
                }
            }
        }
    }
}

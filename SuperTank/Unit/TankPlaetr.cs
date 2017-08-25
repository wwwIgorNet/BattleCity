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

        public TankPlaetr(int id, int x, int y, int width, int height, TypeUnit type, int velosity, Direction direction, IDriver driver, TypeUnit typeShell, int velosityShell, ISoundGame soundGame) : base(id, x, y, width, height, type, velosity, direction, driver, typeShell, velosityShell)
        {
            this.soundGame = soundGame;
        }

        public Plaeyr OwnerPlaeyr { get; set; }

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
            OwnerPlaeyr.AddToScene();
        }

        public override void Move()
        {
            base.Move();
            for (int i = 0; i < Scene.Bonus.Count; i++)
            {
                if (BoundingBox.IntersectsWith(Scene.Bonus[i].BoundingBox))
                {
                    soundGame.Bonus();
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
                            break;
                        case TypeUnit.Shovel:
                            new ShovelBonus().Start();
                            break;
                        case TypeUnit.StarMedal:
                            break;
                        case TypeUnit.Tank:
                            OwnerPlaeyr.CountTank++;
                            break;
                    }
                    Scene.Bonus[i].Dispose();
                }
            }
        }
    }
}

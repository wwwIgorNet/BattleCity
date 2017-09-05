using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public override bool IsParking
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

        public override void Turn(Direction dir)
        {
            base.Turn(dir);
            // если направление изменилось и танк на льду
            if(OnIce && Direction == dir)
                soundGame.Glide();
        }

        #region Fire

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

        public override void Dispose()
        {
            base.Dispose();
            soundGame.TankDispouse();
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

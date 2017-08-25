using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class Plaeyr : BaseOwner
    {
        private ISoundGame soundGame;
        public Owner owner;

        public Plaeyr(ISoundGame soundGame, Owner owner)
        {
            this.soundGame = soundGame;
            this.owner = owner;
            this.CountTank = 1;
        }

        public int CountTank { get; set; }

        public void Start()
        {
            AddToScene();
        }

        public void AddToScene()
        {
            if(CountTank > 0)
            {
                if (Scene.IsFreePosition(new Rectangle(ConfigurationGame.StartPositionTankPlaeyr, new Size(ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank))))
                {
                    IDriver plaeyrDriver = new PlaeyrDriver(Game.Keyboard);
                    plaeyrDriver.Tank = FactoryUnit.CreateTank(ConfigurationGame.StartPositionTankPlaeyr.X, ConfigurationGame.StartPositionTankPlaeyr.Y
                            , TypeUnit.SmallTankPlaeyr, Direction.Up, plaeyrDriver, soundGame);
                    ((TankPlaetr)plaeyrDriver.Tank).OwnerPlaeyr = this;

                    Star star = FactoryUnit.CreateStar(TypeUnit.Star, plaeyrDriver.Tank);
                    star.Start();
                    new HelmetBonus(plaeyrDriver.Tank, 6).Start();
                    CountTank--;
                    Stop();
                }
                else Start();
            }
        }

        public override void Update()
        {
            AddToScene();
        }
    }
}

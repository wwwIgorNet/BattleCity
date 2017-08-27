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
            Points = 0;
            DestroyedTanks = new Dictionary<TypeUnit, int>()
            {
                { TypeUnit.PlainTank, 0 },
                { TypeUnit.ArmoredPersonnelCarrierTank, 0 },
                { TypeUnit.QuickFireTank, 0 },
                { TypeUnit.ArmoredTank, 0}
            };
        }

        public int CountTank { get; set; }
        public int Points { get; set; }
        public Dictionary<TypeUnit, int> DestroyedTanks { get; protected set; }
        public TankPlaetr CurrentTank { get; set; }

        public override void Start()
        {
            base.Start();
            AddToScene();
            
            //foreach (var item in DestroyedTanks)
            //    DestroyedTanks[item.Key] = 0;
        }

        public void AddToScene()
        {
            if(CountTank > 0)
            {
                if (Scene.IsFreePosition(new Rectangle(ConfigurationGame.StartPositionTankPlaeyr, new Size(ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank))))
                {
                    IDriver plaeyrDriver = new PlaeyrDriver(Game.Keyboard);
                    CurrentTank = FactoryUnit.CreateTank(ConfigurationGame.StartPositionTankPlaeyr.X, ConfigurationGame.StartPositionTankPlaeyr.Y
                            , TypeUnit.SmallTankPlaeyr, Direction.Up, plaeyrDriver, soundGame, this);
                    plaeyrDriver.Tank = CurrentTank;


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

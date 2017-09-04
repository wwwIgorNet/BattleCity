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
        private int countTank;

        public Plaeyr(ISoundGame soundGame, Owner owner)
        {
            this.soundGame = soundGame;
            this.owner = owner;
            Points = 0;
            DestroyedTanks = new Dictionary<TypeUnit, int>()
            {
                { TypeUnit.PlainTank, 0 },
                { TypeUnit.ArmoredPersonnelCarrierTank, 0 },
                { TypeUnit.QuickFireTank, 0 },
                { TypeUnit.ArmoredTank, 0}
            };
        }

        public int CountTank
        {
            get
            {
                return countTank;
            }
            set
            {
                Game.GameInfo.SetCountTankPlaeyr(value);
                countTank = value;
            }
        }
        public int Points { get; set; }
        public Dictionary<TypeUnit, int> DestroyedTanks { get; protected set; }
        public TankPlaetr CurrentTank { get; set; }

        public override void Start()
        {
            if (CountTank > 0 || CurrentTank != null)
            {
                base.Start();
                AddToScene(CurrentTank == null ? TypeUnit.SmallTankPlaeyr : CurrentTank.Type);
            }
        }

        public void TryAddToScene()
        {
            if (CountTank > 0)
            {
                if (Scene.IsFreePosition(new Rectangle(ConfigurationGame.StartPositionTankPlaeyr, new Size(ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank))))
                {
                    AddToScene(TypeUnit.SmallTankPlaeyr);
                    CountTank--;
                }
                else if (!LevelManager.Updatable.Contains(this))
                    base.Start();
            }
        }

        private void AddToScene(TypeUnit typeTank)
        {
            IDriver plaeyrDriver = new PlaeyrDriver(Game.Keyboard);
            CurrentTank = FactoryUnit.CreateTank(ConfigurationGame.StartPositionTankPlaeyr.X, ConfigurationGame.StartPositionTankPlaeyr.Y
                    , typeTank, Direction.Up, plaeyrDriver, soundGame, this);
            plaeyrDriver.Tank = CurrentTank;


            Star star = FactoryUnit.CreateStar(TypeUnit.Star, CurrentTank);
            star.Start();
            new HelmetBonus(CurrentTank, 6).Start();
            Stop();
        }

        public override void Update()
        {
            TryAddToScene();
        }
    }
}

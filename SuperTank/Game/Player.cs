using SuperTank.Audio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class Player : IUpdatable, IDisposable
    {
        private int CountTank;
        private ISoundGame soundGame;

        public void Start()
        {
            Game.Updatable.Add(this);
        }

        public void Dispose()
        {
            Game.Updatable.Remove(this);
        }

        public void Update()
        {
            if(Scene.Tanks.Find(t => (Owner)t.Properties[PropertiesType.Owner] == Owner.Plaeyr) == null && CountTank > 0)
            {
                if (IsFreePosition(new Rectangle(ConfigurationGame.StartPositionTankPlaeyr, new Size(ConfigurationGame.WidthTank, ConfigurationGame.HeigthTank))))
                {
                    IDriver plaeyrDriver = new PlaeyrDriver(Game.Keyboard);
                    plaeyrDriver.Tank = FactoryUnit.CreateTank(ConfigurationGame.StartPositionTankPlaeyr.X, ConfigurationGame.StartPositionTankPlaeyr.Y
                            , TypeUnit.SmallTankPlaeyr, Direction.Up, plaeyrDriver, soundGame);

                    Star star = FactoryUnit.CreateStar(TypeUnit.Star, plaeyrDriver.Tank);
                    star.Start();
                    new HelmetBonus(plaeyrDriver.Tank, 6).Start();
                }
            }
        }

        public bool IsFreePosition(Rectangle recPos)
        {
            for (int i = 0; i < Scene.Tanks.Count; i++)
                if (Scene.Tanks[i].BoundingBox.IntersectsWith(recPos))
                    return false;

            for (int i = 0; i < Scene.Stars.Count; i++)
                if (Scene.Stars[i].BoundingBox.IntersectsWith(recPos))
                    return false;

            return true;
        }
    }
}

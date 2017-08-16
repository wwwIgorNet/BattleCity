using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class Star : Unit, IUpdatable
    {
        private IPlaeyr plaeyr;
        private IScene scene;
        private int delay;

        public Star(int id, int x, int y, int width, int height, TypeUnit type, IPlaeyr plaeyr, IScene scene) : base(id, x, y, width, height, type)
        {
            this.plaeyr = plaeyr;
            this.scene = scene;
        }

        public void Start()
        {
            Game.Updatable.Remove(plaeyr);
            scene.Add(this);
            Game.Updatable.Add(this);
        }

        public void Update()
        {
            if (delay < ConfigurationGame.DelayAppearanceOfTank)
                delay++;
            else
            {
                scene.Remove(this);
                scene.Add(plaeyr.Tank);
                Game.Updatable.Add(plaeyr);
                plaeyr.Tank.Properties[PropertiesType.IsParking] = !(bool)plaeyr.Tank.Properties[PropertiesType.IsParking];
                Game.Updatable.Remove(this);
            }
        }
    }
}

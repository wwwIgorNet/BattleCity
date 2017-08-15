using SuperTank;
using SuperTank.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.Updatable
{
    /// <summary>
    /// Появление танка в виде звезды
    /// </summary>
    class StarUpdate : IUpdatable
    {
        private IPlaeyr plaeyr;
        private IScene scene;
        private Unit star;
        private int delay;

        public StarUpdate(Unit star, IPlaeyr plaeyr, IScene scene)
        {
            this.star = star;
            this.plaeyr = plaeyr;
            this.scene = scene;
        }

        public void Update()
        {
            if (delay < ConfigurationGame.DelayAppearanceOfTank)
                delay++;
            else
            {
                Game.Updatable.Remove(this);
                scene.Remove(star);
                scene.Add(plaeyr.Tank);
                Game.Updatable.Add(plaeyr);
                plaeyr.Tank.Properties[PropertiesType.IsParking] = !(bool)plaeyr.Tank.Properties[PropertiesType.IsParking];
            }
        }
    }
}

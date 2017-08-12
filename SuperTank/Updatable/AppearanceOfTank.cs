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
    class AppearanceOfTank : IUpdatable
    {
        private Unit tank;
        private Dictionary<TypeCommand, BaseCommand> commands;
        private int delay;

        public AppearanceOfTank(Unit tank, Dictionary<TypeCommand, BaseCommand> commands)
        {
            this.tank = tank;
            this.commands = commands;
        }

        public void Update()
        {
            if (delay < ConfigurationGame.DelayAppearanceOfTank)
                delay++;
            else
            {
                foreach (var item in commands)
                    tank.Commands.AddCommand(item.Key, item.Value);
                Game.Updatable.Remove(this);
            }
            
        }
    }
}

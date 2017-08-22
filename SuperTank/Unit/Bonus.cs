using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class Bonus : Unit, IUpdatable
    {
        private int interval = 500;
        public Bonus(int id, int x, int y, int width, int height, TypeUnit type) : base(id, x, y, width, height, type)
        {
        }

        public void Start()
        {
            AddToScene();
            Game.Updatable.Add(this);
            Scene.Bonus.Add(this);
        }

        public override void Dispose()
        {
            base.Dispose();
            Game.Updatable.Remove(this);
            Scene.Bonus.Remove(this);
        }

        public void Update()
        {
            interval--;
            if (interval == 0)
                Dispose();
        }
    }
}

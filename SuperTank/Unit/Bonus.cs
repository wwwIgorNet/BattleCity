using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class Bonus : Unit, IUpdatable
    {
        private DateTime starTime;
        public Bonus(int id, int x, int y, int width, int height, TypeUnit type) : base(id, x, y, width, height, type)
        {
        }

        public void Start()
        {
            AddToScene();
            Game.Updatable.Add(this);
            Scene.Bonus.Add(this);
            starTime = DateTime.Now;
        }

        public override void Dispose()
        {
            base.Dispose();
            Game.Updatable.Remove(this);
            Scene.Bonus.Remove(this);
        }

        public void Update()
        {
            if (DateTime.Now - starTime > TimeSpan.FromSeconds(10))
                Dispose();
        }
    }
}

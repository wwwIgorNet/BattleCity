using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class BigDetonation : Unit, IUpdatable
    {
        private IScene scene;
        private int iterationUpdate;

        public BigDetonation(int id, int x, int y, int width, int height, TypeUnit type, IScene scene) : base(id, x, y, width, height, type)
        {
            this.scene = scene;
        }

        public void Update()
        {
            iterationUpdate++;
            if(iterationUpdate == ConfigurationGame.TimeBigDetonation)
            {
                Game.Updatable.Remove(this);
                scene.Remove(this);
            }
        }

        internal void Start()
        {
            Game.Updatable.Add(this);
            scene.Add(this);
        }
    }
}

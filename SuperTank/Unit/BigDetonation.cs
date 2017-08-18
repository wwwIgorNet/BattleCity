using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    class BigDetonation : Unit, IUpdatable
    {
        private int iterationUpdate;

        public BigDetonation(int id, int x, int y, int width, int height, TypeUnit type) : base(id, x, y, width, height, type)
        { }

        public void Update()
        {
            iterationUpdate++;
            if (iterationUpdate == ConfigurationGame.TimeBigDetonation)
                Dispose();
        }

        public void Start()
        {
            Game.Updatable.Add(this);
            AddToScene();
        }

        public override void Dispose()
        {
            base.Dispose();
            Game.Updatable.Remove(this);
        }
    }
}

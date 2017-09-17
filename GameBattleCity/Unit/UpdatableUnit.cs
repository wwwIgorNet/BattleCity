using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public abstract class UpdatableUnit : Unit, IUpdatable
    {
        public UpdatableUnit(int id, int x, int y, int width, int height, TypeUnit type) : base(id, x, y, width, height, type)
        {
        }

        public virtual void Start()
        {
            LevelManager.Updatable.Add(this);
        }

        public override void Dispose()
        {
            base.Dispose();
            LevelManager.Updatable.Remove(this);
        }

        abstract public void Update();
    }
}

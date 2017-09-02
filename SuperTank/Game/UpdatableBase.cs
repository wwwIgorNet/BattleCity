using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public abstract class UpdatableBase : IUpdatable, IDisposable
    {
        public virtual void Start()
        {
            LevelManager.Updatable.Add(this);
        }

        public virtual void Dispose()
        {
            LevelManager.Updatable.Remove(this);
        }

        abstract public void Update();
    }
}

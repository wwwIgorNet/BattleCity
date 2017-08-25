using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public abstract class BaseOwner : IUpdatable, IDisposable
    {

        public virtual void Start()
        {
            Game.Updatable.Add(this);
        }

        public void Stop()
        {
            Game.Updatable.Remove(this);
        }

        public void Dispose()
        {
            Game.Updatable.Remove(this);
        }

        public abstract void Update();
    }
}

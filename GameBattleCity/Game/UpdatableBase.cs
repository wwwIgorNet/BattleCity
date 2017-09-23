using System;

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

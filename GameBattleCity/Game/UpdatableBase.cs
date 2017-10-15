using System;

namespace SuperTank
{
    /// <summary>
    /// The base class that is updated by the LevelMenedger class
    /// </summary>
    public abstract class UpdatableBase : IUpdatable, IDisposable
    {
        /// <summary>
        /// Starts the work
        /// </summary>
        public virtual void Start()
        {
            LevelManager.Updatable.Add(this);
        }
        /// <summary>
        /// Stops work
        /// </summary>
        public virtual void Dispose()
        {
            LevelManager.Updatable.Remove(this);
        }

        /// <summary>
        /// Called after a certain interval
        /// </summary>
        abstract public void Update();
    }
}

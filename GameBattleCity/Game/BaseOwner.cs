namespace SuperTank
{
    /// <summary>
    /// Base class for participants of the game
    /// </summary>
    public abstract class BaseOwner : UpdatableBase
    {
        public void Stop()
        {
            LevelManager.Updatable.Remove(this);
        }
    }
}

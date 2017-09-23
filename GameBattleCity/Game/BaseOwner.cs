namespace SuperTank
{
    public abstract class BaseOwner : UpdatableBase
    {
        public void Stop()
        {
            LevelManager.Updatable.Remove(this);
        }
    }
}

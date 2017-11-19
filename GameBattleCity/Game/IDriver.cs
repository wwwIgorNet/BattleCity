namespace SuperTank
{
    public interface IDriver : IUpdatable
    {
        Tank Tank { get; set; }
    }
}
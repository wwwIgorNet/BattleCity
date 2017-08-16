using SuperTank.Audio;

namespace SuperTank
{
    public interface IPlaeyr: IUpdatable
    {
        Tank Tank { get; set; }
    }
}
using SuperTank.Audio;

namespace SuperTank
{
    public interface IFactoryUnit
    {
        Unit CreatePlaeyrTank(int x, int y, TypeUnit type, ISoundGame soundGame);
        Unit Create(int x, int y,TypeUnit type);
    }
}
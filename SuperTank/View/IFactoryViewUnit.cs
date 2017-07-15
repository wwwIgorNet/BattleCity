using SuperTank.View;

namespace SuperTank
{
    interface IFactoryViewUnit
    {
        BaseView Create(int id, float x, float y, TypeUnit typeUnit);
    }
}

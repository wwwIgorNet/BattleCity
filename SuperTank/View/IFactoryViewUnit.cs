using SuperTank.View;

namespace SuperTank
{
    interface IFactoryViewUnit
    {
        BaseView Create(Unit unit);
    }
}

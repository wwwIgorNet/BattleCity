using SuperTank.View;

namespace SuperTank.Interface
{
    interface IFactoryViewUnit
    {
        BaseView Create(Unit unit);
    }
}

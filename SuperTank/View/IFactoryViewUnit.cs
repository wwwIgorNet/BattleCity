using SuperTank.View;
using System.Collections.Generic;

namespace SuperTank
{
    interface IFactoryViewUnit
    {
        BaseView Create(int id, float x, float y, TypeUnit typeUnit, Dictionary<PropertiesType, object> properties);
    }
}

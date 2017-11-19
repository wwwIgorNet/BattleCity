using SuperTank.View;
using System.Collections.Generic;

namespace SuperTank
{
    /// <summary>
    /// The object creator interface for displaying units
    /// </summary>
    interface IFactoryViewUnit
    {
        BaseView Create(int id, float x, float y, TypeUnit typeUnit, Dictionary<PropertiesType, object> properties);
    }
}

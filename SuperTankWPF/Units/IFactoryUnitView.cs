using SuperTank;
using System.Collections.Generic;

namespace SuperTankWPF.Units
{
    interface IFactoryUnitView
    {
        UnitView Create(UnitViewModel unitViewModel);
        UnitView Create(float x, float y, TypeUnit typeUnit, Dictionary<PropertiesType, object> properties);
    }
}
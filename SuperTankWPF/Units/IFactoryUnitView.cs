using SuperTank;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SuperTankWPF.Units
{
    interface IFactoryUnitView
    {
        UnitView Create(int id, TypeUnit typeUnit, int x, int y, Dictionary<PropertiesType, object> properties);
    }
}
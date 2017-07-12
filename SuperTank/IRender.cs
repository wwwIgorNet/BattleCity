using System.Collections.Generic;

namespace SuperTank
{
    internal interface IRender
    {
        List<ViewUnit> Drowable { get; }
    }
}
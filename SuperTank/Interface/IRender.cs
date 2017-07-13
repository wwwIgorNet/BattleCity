using SuperTank.View;
using System.Collections.Generic;

namespace SuperTank.Interface
{
    internal interface IRender
    {
        List<BaseView> Drowable { get; }
    }
}
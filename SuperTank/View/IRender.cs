using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SuperTank.View
{
    public interface IRender
    {
        void SceneChangedHendler(SceneEventArgs e);
    }
}
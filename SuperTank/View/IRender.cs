using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SuperTank.View
{
    public interface IRender
    {
        void SceneChanged(SceneEventArgs e);
    }

    public class SceneEventArgs
    {
        private TypeAction action;
        private Unit unit;

        public SceneEventArgs(TypeAction action, Unit unit)
        {
            this.action = action;
            this.unit = unit;
        }
        
        public TypeAction Action { get { return action; } }
        public Unit Unit { get { return unit; } }
    }

    public enum TypeAction
    {
        Add,
        Remove
    }
}
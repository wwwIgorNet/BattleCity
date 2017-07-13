using System;
using System.Collections.ObjectModel;

namespace SuperTank
{
    public interface IScene
    {
        int Height { get; }
        int Widtch { get; }
        
        event Action<SceneEventArgs> SceneChenges;

        void Add(Unit unit);
        void Remove(Unit unit);
        void Clear();
        Unit Colision(Unit unit);
        bool ColisionBoard(Unit unit);
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
        Remove,
        Clear
    }
}
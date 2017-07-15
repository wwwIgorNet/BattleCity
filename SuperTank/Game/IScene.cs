using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SuperTank
{
    public interface IScene
    {
        Int32 Height { get; }
        Int32 Widtch { get; }
        
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
        private Dictionary<PropertiesType, Object> properties;

        public SceneEventArgs(TypeAction action)
        {
            this.action = action;
        }

        public TypeAction Action { get { return action; } }
        public Dictionary<PropertiesType, Object> Properties
        {
            get
            {
                return properties == null ? properties = new Dictionary<PropertiesType, object>() : properties;
            }
        }
    }

    public enum TypeAction
    {
        Add,
        Remove,
        Clear,
        Update
    }
}
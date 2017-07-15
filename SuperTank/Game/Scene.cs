using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SuperTank
{
    public class Scene : IScene
    {
        private readonly List<Unit> units = new List<Unit>();
        private readonly int width = ConfigurationGame.WidthBoard;
        private readonly int height = ConfigurationBase.HeightBoard;

        public event Action<SceneEventArgs> SceneChenges;

        public int Height { get { return height; } }
        public int Widtch { get { return width; } }

        public void Clear()
        {
            units.Clear();
            OnSceneChenges(new SceneEventArgs(TypeAction.Clear));
        }
        public Unit Colision(Unit unit)
        {
            Unit colisionUnit = null;
            for (int i = 0; i < units.Count; i++)
            {
                if (!unit.Equals(units[i]) && unit.BoundingBox.IntersectsWith(units[i].BoundingBox))
                {
                    colisionUnit = units[i];
                    break;
                }
            }
            return colisionUnit;
        }
        public bool ColisionBoard(Unit unit)
        {
            if (unit.X < 0)
                return true;
            if (unit.Y < 0)
                return true;
            if (unit.X + unit.Width > width)
                return true;
            if (unit.Y + unit.Height > height)
                return true;

            return false;
        }
        public void Add(Unit unit)
        {
            units.Add(unit);

            SceneEventArgs sceneEventArgs = new SceneEventArgs(TypeAction.Add);
            sceneEventArgs.Properties[PropertiesType.ID] = unit.ID;
            sceneEventArgs.Properties[PropertiesType.X] = unit.X;
            sceneEventArgs.Properties[PropertiesType.Y] = unit.Y;
            sceneEventArgs.Properties[PropertiesType.TypeUnit] = unit.Type;
            switch (unit.Type)
            {
                case TypeUnit.PlainTank:
                    sceneEventArgs.Properties[PropertiesType.Direction] = unit.Properties[PropertiesType.Direction];
                    sceneEventArgs.Properties[PropertiesType.IsStop] = unit.Properties[PropertiesType.IsStop];
                    break;
                case TypeUnit.Shell:
                    sceneEventArgs.Properties[PropertiesType.Direction] = unit.Properties[PropertiesType.Direction];
                    break;
            }

            OnSceneChenges(sceneEventArgs);
            unit.PropertyChanged += Unit_PropertyChanged;
        }

        private void Unit_PropertyChanged(int id, PropertiesType type, object value)
        {
            SceneEventArgs sceneEventArgs = new SceneEventArgs(TypeAction.Update);
            sceneEventArgs.Properties[PropertiesType.ID] = id;
            sceneEventArgs.Properties[type] = value;
            sceneEventArgs.Properties[PropertiesType.TypeUpdate] = type;

            OnSceneChenges(sceneEventArgs);
        }

        public void Remove(Unit unit)
        {
            units.Remove(unit);
            SceneEventArgs sceneEventArgs = new SceneEventArgs(TypeAction.Remove);
            sceneEventArgs.Properties[PropertiesType.ID] = unit.ID;
            OnSceneChenges(sceneEventArgs);
            unit.PropertyChanged -= Unit_PropertyChanged;
        }

        protected void OnSceneChenges(SceneEventArgs e)
        {
            if (SceneChenges != null)
                SceneChenges.Invoke(e);
        }
    }
}
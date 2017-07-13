using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SuperTank
{
    public class Scene : IScene
    {
        private readonly List<Unit> units = new List<Unit>();
        private readonly int width = ConfigurationGame.WidthBoard;
        private readonly int height = ConfigurationGame.HeightBoard;

        public event Action<SceneEventArgs> SceneChenges;

        public int Height { get { return height; } }
        public int Widtch { get { return width; } }

        public void Clear()
        {
            units.Clear();
            OnSceneChenges(new SceneEventArgs(TypeAction.Clear, null));
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
            OnSceneChenges(new SceneEventArgs(TypeAction.Add, unit));
        }
        public void Remove(Unit unit)
        {
            units.Remove(unit);
            OnSceneChenges(new SceneEventArgs(TypeAction.Remove, unit));
        }

        protected void OnSceneChenges(SceneEventArgs e)
        {
            if (SceneChenges != null)
                SceneChenges.Invoke(e);
        }
    }
}
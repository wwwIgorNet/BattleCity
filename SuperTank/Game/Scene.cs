using System;
using System.Collections.ObjectModel;

namespace SuperTank
{
    public class Scene : IScene
    {
        private readonly ObservableCollection<Unit> units = new ObservableCollection<Unit>();
        private readonly int width = ConfigurationGme.WidthBoard;
        private readonly int height = ConfigurationGme.HeightBoard;

        public Scene()
        {
            units.CollectionChanged += Units_CollectionChanged;
        }

        private void Units_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (SceneChenges != null)
                        for (int i = 0; i < e.NewItems.Count; i++)
                            SceneChenges.Invoke(new SceneEventArgs(TypeAction.Add, (Unit)e.NewItems[i]));
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    if (SceneChenges != null)
                        for (int i = 0; i < e.NewItems.Count; i++)
                            SceneChenges.Invoke(new SceneEventArgs(TypeAction.Remove, (Unit)e.NewItems[i]));
                    break;
            }
        }

        public event Action<SceneEventArgs> SceneChenges;

        public int Height { get { return height; } }
        public int Widtch { get { return width; } }

        public void Clear()
        {
            units.Clear();
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
        }
        public void Remove(Unit unit)
        {
            units.Remove(unit);
        }
    }
}
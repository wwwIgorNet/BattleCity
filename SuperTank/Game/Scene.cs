using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SuperTank
{
    public class Scene : IScene
    {
        private readonly ObservableCollection<Unit> units = new ObservableCollection<Unit>();
        private readonly int width = ConfigurationGme.WidthBoard;
        private readonly int height = ConfigurationGme.HeightBoard;

        public ObservableCollection<Unit> Units { get { return units; } }
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
    }
}
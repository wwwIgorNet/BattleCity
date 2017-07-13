using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SuperTank
{
    class Scene
    {
        private static readonly ObservableCollection<Unit> alloObj = new ObservableCollection<Unit>();
        private static readonly int width = Configuration.WidthBoard;
        private static readonly int height = Configuration.HeightBoard;

        public static ObservableCollection<Unit> AlloObj { get { return alloObj; } }
        public static int Height { get { return height; } }
        public static int Widtch { get { return width; } }

        public static void Clear()
        {
            alloObj.Clear();
        }

        public static Unit Colision(Unit unit)
        {
            Unit colisionUnit = null;
            for (int i = 0; i < alloObj.Count; i++)
            {
                if (!unit.Equals(alloObj[i]) && unit.BoundingBox.IntersectsWith(alloObj[i].BoundingBox))
                {
                    colisionUnit = alloObj[i];
                    break;
                }
            }
            return colisionUnit;
        }

        public static bool ColisionBoard(Unit unit)
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
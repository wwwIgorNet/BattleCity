using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;

namespace SuperTank
{
    public class Scene : IScene
    {
        private IRender render;
        private readonly List<Unit> units = new List<Unit>();
        private readonly int width = ConfigurationGame.WidthBoard;
        private readonly int height = ConfigurationBase.HeightBoard;

        public Scene(IRender render)
        {
            this.render = render;
        }

        public int Height { get { return height; } }
        public int Widtch { get { return width; } }

        public void Clear()
        {
            units.Clear();
            render.Clear();
        }
        public Unit Colision(Unit unit)
        {
            return Colision(unit.BoundingBox, unit.ID);
        }
        public Unit Colision(Rectangle rect, int unitId)
        {
            for (int i = 0; i < units.Count; i++)
                if (units[i].ID != unitId && rect.IntersectsWith(units[i].BoundingBox))
                    return units[i];
            return null;
        }

        public bool ColisionBoard(Unit unit)
        {
            return ColisionBoard(unit.BoundingBox);
        }
        public bool ColisionBoard(Rectangle rect)
        {
            if (rect.X < 0)
                return true;
            if (rect.Y < 0)
                return true;
            if (rect.X + rect.Width > width)
                return true;
            if (rect.Y + rect.Height > height)
                return true;

            return false;
        }
        public void Add(Unit unit)
        {
            units.Add(unit);
            Dictionary<PropertiesType, object> properties = null;
            switch (unit.Type)
            {
                case TypeUnit.PlainTank:
                    properties = new Dictionary<PropertiesType, object>();

                    properties[PropertiesType.Direction] = unit.Properties[PropertiesType.Direction];
                    properties[PropertiesType.IsStop] = unit.Properties[PropertiesType.IsStop];
                    break;
                case TypeUnit.Shell:
                    properties = new Dictionary<PropertiesType, object>();
                    properties[PropertiesType.Direction] = unit.Properties[PropertiesType.Direction];
                    break;
            }
            render.Add(unit.ID, unit.Type, unit.X, unit.Y, properties);

            unit.PropertyChanged += render.Update;
        }

        public void Remove(Unit unit)
        {
            units.Remove(unit);
            render.Remove(unit.ID);
        }
    }
}
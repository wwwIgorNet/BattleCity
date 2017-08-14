using SuperTank.Updatable;
using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Threading;

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
        
        public List<Unit> Units { get { return units; } }

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
            Dictionary<PropertiesType, object> properties = GetPropertiesForView(unit);
            render.Add(unit.ID, unit.Type, unit.X, unit.Y, properties);

            unit.PropertyChanged += Unit_PropertyChanged;
        }

        private void Unit_PropertyChanged(int id, PropertiesType propType, object val)
        {
            switch (propType)
            {
                case PropertiesType.Direction:
                case PropertiesType.IsParking:
                case PropertiesType.X:
                case PropertiesType.Y:
                case PropertiesType.Detonation:
                //case PropertiesType.Glide:
                    render.Update(id, propType, val);
                    break;
            }
        }

        private static Dictionary<PropertiesType, object> GetPropertiesForView(Unit unit)
        {
            Dictionary<PropertiesType, object> properties = null;
            switch (unit.Type)
            {
                case TypeUnit.PainTank:
                    properties = new Dictionary<PropertiesType, object>();

                    properties[PropertiesType.Direction] = unit.Properties[PropertiesType.Direction];
                    properties[PropertiesType.IsParking] = unit.Properties[PropertiesType.IsParking];
                    //properties[PropertiesType.Glide] = unit.Properties[PropertiesType.Glide];
                    break;
                case TypeUnit.Shell:
                    properties = new Dictionary<PropertiesType, object>();
                    properties[PropertiesType.Direction] = unit.Properties[PropertiesType.Direction];
                    break;
            }

            return properties;
        }

        public void Remove(Unit unit)
        {
            units.Remove(unit);
            render.Remove(unit.ID);
        }

        public void AddRange(List<Unit> collection)
        {
            units.AddRange(collection);
            List<UnitDataForView> data = new List<UnitDataForView>();
            for (int i = 0; i < collection.Count; i++)
            {
                Unit u = collection[i];
                data.Add(new UnitDataForView(u.ID, u.Type, u.X, u.Y, GetPropertiesForView(u)));
            }

            render.AddRange(data);
        }
    }
}
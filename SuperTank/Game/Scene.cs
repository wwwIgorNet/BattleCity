using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

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
            Property[] properties = null;
            switch (unit.Type)
            {
                case TypeUnit.PlainTank:
                    properties = new Property[] {
                        new Property {
                            Type = PropertiesType.Direction,
                            Value = unit.Properties[PropertiesType.Direction]
                        }, new Property {
                            Type = PropertiesType.IsStop,
                            Value = unit.Properties[PropertiesType.IsStop]
                        } };
                    break;
                case TypeUnit.Shell:
                    properties = new Property[] { new Property {
                    Type = PropertiesType.Direction,
                    Value = unit.Properties[PropertiesType.Direction]
                    } };
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
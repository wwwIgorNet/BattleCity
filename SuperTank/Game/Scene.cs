using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Threading;

namespace SuperTank
{
    public static class Scene
    {
        private static readonly List<Unit> units = new List<Unit>();
        private static readonly List<Unit> bonus = new List<Unit>();
        private static readonly List<Unit> tanks = new List<Unit>();
        private static readonly int width = ConfigurationGame.WidthBoard;
        private static readonly int height = ConfigurationBase.HeightBoard;

        public static IRender Render { get; set; }

        public static int Height { get { return height; } }
        public static int Widtch { get { return width; } }

        public static void Clear()
        {
            units.Clear();
            Render.Clear();
        }

        public static List<Unit> Units { get { return units; } }
        public static List<Unit> Tanks { get { return tanks; } }
        public static List<Unit> Bonus { get { return bonus; } }

        public static bool ColisionBoard(Unit unit)
        {
            return ColisionBoard(unit.BoundingBox);
        }
        public static bool ColisionBoard(Rectangle rect)
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
        public static void Add(Unit unit)
        {
            units.Add(unit);
            Dictionary<PropertiesType, object> properties = GetPropertiesForView(unit);
            Render.Add(unit.ID, unit.Type, unit.X, unit.Y, properties);

            unit.PropertyChanged += Unit_PropertyChanged;
        }

        private static void Unit_PropertyChanged(int id, PropertiesType propType, object val)
        {
            switch (propType)
            {
                case PropertiesType.Direction:
                case PropertiesType.IsParking:
                case PropertiesType.X:
                case PropertiesType.Y:
                case PropertiesType.Detonation:
                case PropertiesType.NumberOfHits:
                    Render.Update(id, propType, val);
                    break;
            }
        }

        private static Dictionary<PropertiesType, object> GetPropertiesForView(Unit unit)
        {
            Dictionary<PropertiesType, object> properties = null;
            switch (unit.Type)
            {
                // todo
                case TypeUnit.SmallTankPlaeyr:
                case TypeUnit.LightTankPlaeyr:
                case TypeUnit.MediumTankPlaeyr:
                case TypeUnit.HeavyTankPlaeyr:

                case TypeUnit.PainTank:
                case TypeUnit.ArmoredPersonnelCarrierTank:
                case TypeUnit.QuickFireTank:
                    properties = new Dictionary<PropertiesType, object>();

                    properties[PropertiesType.Direction] = unit.Properties[PropertiesType.Direction];
                    properties[PropertiesType.IsParking] = unit.Properties[PropertiesType.IsParking];
                    properties[PropertiesType.IsBonusTank] = unit.Properties[PropertiesType.IsBonusTank];
                    break;
                case TypeUnit.ArmoredTank:
                    properties = new Dictionary<PropertiesType, object>();

                    properties[PropertiesType.Direction] = unit.Properties[PropertiesType.Direction];
                    properties[PropertiesType.IsParking] = unit.Properties[PropertiesType.IsParking];
                    properties[PropertiesType.NumberOfHits] = unit.Properties[PropertiesType.NumberOfHits];
                    properties[PropertiesType.IsBonusTank] = unit.Properties[PropertiesType.IsBonusTank];
                    break;
                case TypeUnit.Shell:
                    properties = new Dictionary<PropertiesType, object>();
                    properties[PropertiesType.Direction] = unit.Properties[PropertiesType.Direction];
                    break;
                case TypeUnit.Eagle:
                    properties = new Dictionary<PropertiesType, object>();
                    properties[PropertiesType.Detonation] = unit.Properties[PropertiesType.Detonation];
                    break;
            }

            return properties;
        }

        public static void Remove(Unit unit)
        {
            units.Remove(unit);
            Render.Remove(unit.ID);
        }

        public static void AddRange(List<Unit> collection)
        {
            units.AddRange(collection);
            List<UnitDataForView> data = new List<UnitDataForView>();
            for (int i = 0; i < collection.Count; i++)
            {
                Unit u = collection[i];
                data.Add(new UnitDataForView(u.ID, u.Type, u.X, u.Y, GetPropertiesForView(u)));
                u.PropertyChanged += Unit_PropertyChanged;
            }

            Render.AddRange(data);
        }
    }
}
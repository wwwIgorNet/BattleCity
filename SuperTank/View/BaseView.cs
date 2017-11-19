using System;
using System.Collections.Generic;
using System.Drawing;

namespace SuperTank.View
{
    /// <summary>
    /// Base class to display the unit
    /// </summary>
    public abstract class BaseView
    {
        private int id;
        private Dictionary<PropertiesType, Object> properties;

        public BaseView(int id, float x, float y, float width, float height, int zIndex)
        {
            this.id = id;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            ZIndex = zIndex;
        }

        /// <summary>
        /// Picture for the unit
        /// </summary>
        public abstract Image Img { get; }
        public float X { get; set; }
        public float Width { get; set; }
        public float Y { get; set; }
        public float Height { get; set; }
        public int ID { get { return id; } }
        public int ZIndex { get; }
        public Dictionary<PropertiesType, Object> Properties
        {
            get
            {
                return properties == null ? properties = new Dictionary<PropertiesType, object>() : properties;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            BaseView view = obj as BaseView;
            if (obj == null)
                return false;

            return view.ID.Equals(ID);
        }
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
using SuperTank.Command;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;

namespace SuperTank
{
    public class Unit
    {
        private Rectangle boundingBox;
        private TypeUnit type;
        private Invoker invoker = new Invoker();
        private readonly PropertiesUnit properties;
        private readonly int id;

        public Unit(int id, int x, int y, int width, int height, TypeUnit type)
        {
            this.id = id;
            boundingBox = new Rectangle(x, y, width, height);
            this.type = type;
            properties = new PropertiesUnit(this);
        }

        public event Action<int, PropertiesType, object> PropertyChanged;

        public int ID { get { return id; } }
        public int X
        {
            get { return boundingBox.X; }
            set
            {
                boundingBox.X = value;
                OnPropertyChenges(PropertiesType.X, value);
            }
        }
        public int Y
        {
            get { return boundingBox.Y; }
            set
            {
                boundingBox.Y = value;
                OnPropertyChenges(PropertiesType.Y, value);
            }
        }
        public int Width
        {
            get { return boundingBox.Width; }
            set { boundingBox.Width = value; }
        }
        public int Height
        {
            get { return boundingBox.Height; }
            set { boundingBox.Height = value; }
        }
        public Rectangle BoundingBox { get { return boundingBox; } }
        public TypeUnit Type { get { return type; } }
        public PropertiesUnit Properties { get { return properties; } }
        public Invoker Commands
        {
            get { return invoker; }
            set { invoker = value; }
        }

        public void Execute(TypeCommand command)
        {
            invoker.Execute(command);
        }
        protected void OnPropertyChenges(PropertiesType type, Object value)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(ID, type, value);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Unit unit = obj as Unit;
            if (unit == null)
                return false;
            return unit.ID.Equals(ID) && Type.Equals(unit.Type) && BoundingBox.Equals(unit.BoundingBox);
        }

        public class PropertiesUnit : Dictionary<PropertiesType, object>
        {
            private Unit unit;

            public PropertiesUnit(Unit unit)
            {
                this.unit = unit;
            }

            public new object this[PropertiesType key]
            {
                get
                {
                    return base[key];
                }
                set
                {
                    base[key] = value;
                    unit.OnPropertyChenges(key, value);
                }
            }
        }
    }
}

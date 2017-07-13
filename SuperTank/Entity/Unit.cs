using SuperTank.Command;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SuperTank
{
    public class Unit : IDisposable
    {
        private Rectangle boundingBox;
        private TypeUnit type;
        private Invoker invoker;
        private readonly Dictionary<PropertiesType, Object> properties = new Dictionary<PropertiesType, object>();


        public Unit(int x, int y, int width, int height, TypeUnit type, Invoker invoker)
        {
            boundingBox = new Rectangle(x, y, width, height);
            this.type = type;
            this.invoker = invoker;
            Scene.AlloObj.Add(this);
        }

        public int X
        {
            get { return boundingBox.X; }
            set { boundingBox.X = value; }
        }
        public int Y
        {
            get { return boundingBox.Y; }
            set { boundingBox.Y = value; }
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
        public Dictionary<PropertiesType, Object> Properties { get { return properties; } }


        public void Execute(TypeCommand command)
        {
            invoker.Handl(command);
        }
        public virtual void Dispose()
        {
            Scene.AlloObj.Remove(this);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Unit tmp = obj as Unit;
            if (tmp == null)
                return false;
            return Type.Equals(tmp.Type) && BoundingBox.Equals(tmp.BoundingBox);
        }
    }
}

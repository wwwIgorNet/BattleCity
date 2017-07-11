using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameTankCore
{
    public class ObjGame : IDisposable
    {
        private Rectangle boundingBox;
        public TypeObjGame type;

        public ObjGame(int x, int y, int width, int height, TypeObjGame type)
        {
            boundingBox = new Rectangle(x, y, width, height);
            this.type = type;
            Board.AlloObj.Add(this);
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
        public TypeObjGame Type { get { return type; } }
        
        public virtual void Dispose()
        {
            Board.AlloObj.Remove(this);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            ObjGame tmp = obj as ObjGame;
            if (tmp == null)
                return false;
            return Type.Equals(tmp.Type) && BoundingBox.Equals(tmp.BoundingBox);
        }
    }
}

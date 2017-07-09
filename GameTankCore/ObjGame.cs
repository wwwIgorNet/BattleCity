using System;
using System.Drawing;

namespace GameTankCore
{
    public class ObjGame
    {
        private Rectangle boundingBox;

        public ObjGame(int x, int y, int width, int height)
        {
            boundingBox = new Rectangle(x, y, width, height);
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

        public String Type { get; set; }

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

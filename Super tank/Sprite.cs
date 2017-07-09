using GameTankCore;
using System.Drawing;

namespace Super_tank
{
    class Sprite
    {
        private Image img;
        private ObjGame obj;

        public Sprite(ObjGame obj, string imgPath)
        {
            this.obj = obj;
            img = Image.FromFile(imgPath);
        }


        public ObjGame Obj { get { return obj; } }

        public void Draw(Graphics g)
        {
            g.DrawImage(img, obj.BoundingBox);
        }
    }
}

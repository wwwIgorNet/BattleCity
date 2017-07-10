using GameTankCore;
using System.Drawing;
using System;

namespace Super_tank
{
    class Sprite
    {
        private Image img;
        private ObjGame obj;

        public Sprite(ObjGame obj, Image img)
        {
            this.obj = obj;
            this.img = img;
        }


        public ObjGame Obj { get { return obj; } }
        protected Image Img { set { img = value; } }

        public virtual void Draw(Graphics g)
        {
            g.DrawImage(img, obj.BoundingBox);
        }
    }
}

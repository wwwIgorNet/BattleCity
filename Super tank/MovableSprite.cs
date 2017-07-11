using GameTankCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_tank
{
    class MovableSprite : Sprite
    {
        private readonly int frameCount;
        private int carentFrame;
        private Dictionary<Direction, Image[]> images;
        private Unit obj;

        public MovableSprite(Dictionary<Direction, Image[]> images, Unit obj, int frameCount) : base(obj, null)
        {
            this.frameCount = frameCount;
            this.images = images;
            this.obj = obj;
            Update();
        }

        public void Update()
        {
            base.Img = images[obj.Direction]
                [carentFrame == frameCount ? carentFrame = 0 : carentFrame++];
        }

        public override void Draw(Graphics g)
        {
            Update();
            base.Draw(g);
        }
    }
}

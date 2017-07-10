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
        private Dictionary<Direction, Image> images;
        private MoveObj obj;

        public MovableSprite(Dictionary<Direction, Image> images, MoveObj obj) : base(obj, null)
        {
            this.images = images;
            this.obj = obj;
            Update();
        }

        public void Update()
        {
            base.Img = images[obj.Direction];
        }

        public override void Draw(Graphics g)
        {
            Update();
            base.Draw(g);
        }
    }
}

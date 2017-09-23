using System.Collections.Generic;
using System.Drawing;

namespace SuperTank.View
{
    class ViewAnimationTank : BaseView
    {
        private Dictionary<Direction, Image[]> images;
        private int currentFrame = 0;
        private readonly int countFrame;
        private int invulnerableFrame = 0;

        public ViewAnimationTank(int id, float x, float y, float width, float height, int zIndex, Dictionary<Direction, Image[]> imges) : base(id, x, y, width, height, zIndex)
        {
            this.images = imges;
            this.countFrame = images[Direction.Up].Length;
        }

        public override Image Img
        {
            get
            {
                if (!IsStop)
                {
                    currentFrame++;
                    if (currentFrame == countFrame) currentFrame = 0;
                }
                Image res = Images[Direction][currentFrame];

                if ((bool)Properties[PropertiesType.IsInvulnerable])
                {
                    Bitmap b = new Bitmap(res);
                    Graphics g = Graphics.FromImage(b);
                    Image add;
                    if (invulnerableFrame % 2 == 0)
                    {
                        add = SuperTank.View.Images.Invulnerable1;
                    }
                    else
                    {
                        add = SuperTank.View.Images.Invulnerable2;
                    }

                    g.DrawImage(add, 0, 0);
                    res = b;
                    invulnerableFrame++;
                }

                return res;
            }
        }

        protected virtual Dictionary<Direction, Image[]> Images
        {
            set { images = value; }
            get { return images; }
        }
        protected Direction Direction
        {
            get
            {
                return (Direction)Properties[PropertiesType.Direction];
            }
        }
        protected bool IsStop
        {
            get
            {
                return (bool)Properties[PropertiesType.IsParking];
            }
        }
    }
}

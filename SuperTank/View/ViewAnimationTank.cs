using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.View
{
    class ViewAnimationTank : BaseView
    {
        private Dictionary<Direction, Image[]> images;
        private int currentFrame = 0;
        private readonly int countFrame;

        public ViewAnimationTank(int id, float x, float y, float width, float height, int zIndex, Dictionary<Direction, Image[]> imges) : base(id, x, y, width, height, zIndex)
        {
            this.images = imges;
            this.countFrame = images[Direction.Up].Length;
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

        public override Image Img
        {
            get
            {
                if (!IsStop)
                {
                    currentFrame++;
                    if (currentFrame == countFrame) currentFrame = 0;
                }
                return Images[Direction][currentFrame];
            }
        }
    }
}

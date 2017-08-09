using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.View
{
    class ViewAnimationUnit : BaseView
    {
        private Image[] images;
        private int frame = 0;
        private readonly int DELAY;
        private int delay = 0;

        public ViewAnimationUnit(int id, float x, float y, float width, float height, int zIndex, Image[] images, int delay)
            : base(id, x, y, width, height, zIndex)
        {
            this.DELAY = delay;
            this.images = images;
        }

        public override Image Img
        {
            get
            {
                if (delay < DELAY) delay++;
                else
                {
                    delay = 0;
                    frame++;
                    if (frame >= images.Length) frame = 0;
                }
                return images[frame];
            }
        }
    }
}

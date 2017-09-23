using System.Drawing;

namespace SuperTank.View
{
    class ViewAnimationUnit : BaseView
    {
        private Image[] images;
        private int frame = 0;
        private int iteration = 0;
        private int updateInterval;

        public ViewAnimationUnit(int id, float x, float y, float width, float height, int zIndex, Image[] images, int updateInterval)
            : base(id, x, y, width, height, zIndex)
        {
            this.updateInterval = updateInterval;
            this.images = images;
        }

        public override Image Img
        {
            get
            {
                iteration++;
                if (iteration % updateInterval == 0)
                {
                    frame++;
                    if (frame >= images.Length)
                        frame = 0;
                }
                return images[frame];
            }
        }
    }
}

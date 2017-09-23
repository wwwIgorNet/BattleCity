using System.Drawing;

namespace SuperTank.View
{
    class BigDetonationView : ViewAnimationUnit
    {
        private float centrX;
        private float centrY;

        public BigDetonationView(int id, float x, float y, float width, float height, int zIndex, Image[] images, int updateInterval) : base(id, x, y, width, height, zIndex, images, updateInterval)
        {
            centrX = x + width / 2;
            centrY = y + height / 2;
        }

        public override Image Img
        {
            get
            {
                Image carent = base.Img;
                Width = carent.Width;
                Height = carent.Height;

                X = centrX - Width / 2;
                Y = centrY - Height / 2;

                return carent;
            }
        }
    }
}

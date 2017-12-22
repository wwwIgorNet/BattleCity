using System.Drawing;

namespace SuperTank.View
{
    /// <summary>
    /// Unit
    /// </summary>
    class ViewUnit : BaseView
    {
        private Image img;

        public ViewUnit(int id, float x, float y, float width, float height, int zIndex, Image img) : base(id, x, y, width, height, zIndex)
        {
            this.img = img;
        }

        public override Image Img
        {
            get { return img; }
        }

        protected void SetImg(Image img)
        {
            this.img = img;
        }
    }
}

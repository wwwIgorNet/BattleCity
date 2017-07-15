using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.View
{
    class ViewUnit : BaseView
    {
        private Image img;

        public ViewUnit(int id, float x, float y, float width, float height, int zIndex, Image img) : base(id, x, y, width, height, zIndex)
        {
            this.img = img;
        }

        public override Image Img { get { return img; } }
    }
}

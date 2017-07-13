using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.View
{
    public class ViewUnit : BaseView
    {
        private Image img;

        public ViewUnit(Unit unit, Image img)
            : base (unit)
        {
            this.img = img;
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(img, Unit.BoundingBox);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    public class ViewUnit
    {
        private Unit unit;
        private Image img;

        public ViewUnit(Unit unit, Image img)
        {
            this.unit = unit;
            this.img = img;
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(img, unit.BoundingBox);
        }
    }
}

using SuperTank.Command;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.View
{
    class ViewDirectionUnit : BaseView
    {
        private Dictionary<Direction, Image> imges;

        public ViewDirectionUnit(Unit unit, Dictionary<Direction, Image> imges) : base(unit)
        {
            this.imges = imges;
        }

        protected Direction Direction
        {
            get { return (Direction)Unit.Properties[PropertiesType.Direction]; }
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(imges[Direction], Unit.BoundingBox);
        }
    }
}

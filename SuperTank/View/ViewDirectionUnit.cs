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

        public ViewDirectionUnit(int id, float x, float y, float width, float height, int zIndex, Dictionary<Direction, Image> imges)
            : base(id, x, y, width, height, zIndex)
        {
            this.imges = imges;
        }

        public override Image Img
        {
            get { return imges[Direction]; }
        }

        protected Direction Direction
        {
            get { return (Direction)Properties[PropertiesType.Direction]; }
        }
    }
}

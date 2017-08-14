using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.View
{
    class ViewAnimationTankUnit : BaseView
    {
        private Dictionary<Direction, Image[]> imges;
        private int currentFrame = 0;
        private readonly int countFrame;

        public ViewAnimationTankUnit(int id, float x, float y, float width, float height, int zIndex, Dictionary<Direction, Image[]> imges, int countFrame) : base(id, x, y, width, height, zIndex)
        {
            this.imges = imges;
            this.countFrame = countFrame;
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
                return imges[Direction][currentFrame];
            }
        }

        protected Direction Direction
        {
            get { return (Direction)Properties[PropertiesType.Direction]; }
        }
        protected bool IsStop
        {
            get
            {
                return (bool)Properties[PropertiesType.IsParking];
            }
        }
    }
}

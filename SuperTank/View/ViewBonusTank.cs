using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.View
{
    class ViewBonusTank : BaseView
    {
        private int iteration = 0;
        private int delay;
        private ViewAnimationTank tank1;
        private ViewAnimationTank tank2;

        public ViewBonusTank(int id, float x, float y, float width, float height, int zIndex, ViewAnimationTank tank1, ViewAnimationTank tank2, int delay) : base(id, x, y, width, height, zIndex)
        {
            this.delay = delay;
            this.tank1 = tank1;
            this.tank2 = tank2;
        }

        public override Image Img
        {
            get
            {
                Image res;
                if (iteration > 0) res = tank1.Img;
                else res = tank2.Img;

                if (iteration == delay) iteration = -delay;
                else iteration++;
                return res;
            }
        }
    }
}

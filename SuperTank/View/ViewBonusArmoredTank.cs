using System.Collections.Generic;
using System.Drawing;

namespace SuperTank.View
{
    class ViewBonusArmoredTank : ViewAnimationArmoredTank
    {
        private int iteration = 0;
        private Dictionary<Direction, Image[]> redImages;

        public ViewBonusArmoredTank(int id, float x, float y, float width, float height, int zIndex, Dictionary<Direction, Image[]> tankGray, Dictionary<Direction, Image[]> tankGreen, Dictionary<Direction, Image[]> tankYellow, Dictionary<Direction, Image[]> redImages) : base(id, x, y, width, height, zIndex, tankGray, tankGreen, tankYellow)
        {
            this.redImages = redImages;
        }

        public override Image Img
        {
            get
            {
                iteration++;

                return base.Img;
            }
        }

        protected override Dictionary<Direction, Image[]> Images
        {
            get
            {
                if (iteration <= ConfigurationView.DelayChangColorBonusTank)
                {
                    return redImages;
                }
                else if (iteration == ConfigurationView.DelayChangColorBonusTank * 2)
                {
                    iteration = 0;
                }
                return base.Images;
            }
        }
    }
}

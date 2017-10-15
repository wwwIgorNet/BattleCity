using System.Collections.Generic;
using System.Drawing;

namespace SuperTank.View
{
    /// <summary>
    /// Displaying the bonus tank
    /// </summary>
    class ViewBonusTank : ViewAnimationTank
    {
        private int iteration = 0;
        private Dictionary<Direction, Image[]> redImages;
        private Dictionary<Direction, Image[]> greyImages;

        public ViewBonusTank(int id, float x, float y, float width, float height, int zIndex, Dictionary<Direction, Image[]> greyImages, Dictionary<Direction, Image[]> redImages) : base(id, x, y, width, height, zIndex, greyImages)
        {
            this.redImages = redImages;
            this.greyImages = greyImages;
        }

        public override Image Img
        {
            get
            {
                if (iteration == ConfigurationView.DelayChangColorBonusTank)
                {
                    base.Images = redImages;
                }
                else if (iteration == ConfigurationView.DelayChangColorBonusTank * 2)
                {
                    base.Images = greyImages;
                    iteration = 0;
                }

                iteration++;

                return base.Img;
            }
        }
    }
}

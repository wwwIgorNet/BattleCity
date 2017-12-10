using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SuperTank;
using SuperTankWPF.Model;

namespace SuperTankWPF.Units
{
    class BonusTankView : TankView
    {
        private Dictionary<Direction, ImageSource[]> imgGray;
        private Dictionary<Direction, ImageSource[]> imgRed;
        private int iteration = 0;

        public BonusTankView(Direction direction, Dictionary<Direction, ImageSource[]> imgGray, Dictionary<Direction, ImageSource[]> imgRed, int updateInterval) : base(direction, imgGray, updateInterval)
        {
            this.imgGray = imgGray;
            this.imgRed = imgRed;
        }

        protected override void UpdateSource()
        {
            iteration++;
            if (iteration == 2)
            {
                base.ImagesWithDirection = imgRed;
            }
            else if (iteration == 4)
            {
                iteration = 0;
                base.ImagesWithDirection = imgGray;
            }

            base.UpdateSource();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SuperTank;
using SuperTankWPF.Model;
using System.Timers;

namespace SuperTankWPF.Units
{
    class BonusTankView : TankView
    {
        private BonusTankAnimation bonusTankAnimation;
        private Dictionary<Direction, ImageSource[]> imgGray;
        private Dictionary<Direction, ImageSource[]> imgRed;

        public BonusTankView(Direction direction, Dictionary<Direction, ImageSource[]> imgGray, Dictionary<Direction, ImageSource[]> imgRed, int updateInterval) : base(direction, imgGray, updateInterval)
        {
            this.imgGray = imgGray;
            this.imgRed = imgRed;

            bonusTankAnimation = new BonusTankAnimation(this.Dispatcher);
            bonusTankAnimation.SetRedTank += SetImgTankRed;
            bonusTankAnimation.SetCurentTank += SetImgTankGray;
            bonusTankAnimation.Start();
        }

        private void SetImgTankRed()
        {
            base.ImagesWithDirection = imgRed;
        }
        private void SetImgTankGray()
        {
            base.ImagesWithDirection = imgGray;
        }
    }
}

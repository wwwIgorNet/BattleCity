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
    class BonusArmoredTankView : ArmoredTankView, IDisposable
    {
        private BonusTankAnimation bonusTankAnimation;
        private Dictionary<Direction, ImageSource[]> tankRed;
        private Dictionary<Direction, ImageSource[]> tankCurent;

        public BonusArmoredTankView(Direction direction, Dictionary<Direction, ImageSource[]> tankGray, Dictionary<Direction, ImageSource[]> tankGreen, Dictionary<Direction, ImageSource[]> tankYellow, Dictionary<Direction, ImageSource[]> tankRed, int updateInterval)
            : base(direction, tankGray, tankGreen, tankYellow, updateInterval)
        {
            this.tankRed = tankRed;
            tankCurent = base.ImagesWithDirection;
            bonusTankAnimation = new BonusTankAnimation(this.Dispatcher);
            bonusTankAnimation.SetRedTank += SetImgTankRed;
            bonusTankAnimation.SetCurentTank += SetImgTankCurent;
            bonusTankAnimation.Start();
        }

        private void SetImgTankRed()
        {
            tankCurent = base.ImagesWithDirection;
            base.ImagesWithDirection = tankRed;
        }

        private void SetImgTankCurent()
        {
            base.ImagesWithDirection = tankCurent;
        }

        public override int NumberOfHits
        {
            set
            {
                base.NumberOfHits = value;
                tankCurent = base.ImagesWithDirection;
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            bonusTankAnimation.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SuperTank;

namespace SuperTankWPF.Units
{
    class ArmoredTankView : TankView
    {
        private readonly Dictionary<Direction, ImageSource[]> tankGray;
        private readonly Dictionary<Direction, ImageSource[]> tankGreen;
        private readonly Dictionary<Direction, ImageSource[]> tankYellow;

        public ArmoredTankView(Direction direction, Dictionary<Direction, ImageSource[]> tankGray, Dictionary<Direction, ImageSource[]> tankGreen, Dictionary<Direction, ImageSource[]> tankYellow, int updateInterval) : base(direction, tankGray, updateInterval)
        {
            this.tankGray = tankGray;
            this.tankGreen = tankGreen;
            this.tankYellow = tankYellow;
        }

        protected override void UpdateSource()
        {
            base.UpdateSource();
        }

        public virtual int NumberOfHits
        {
            set
            {
                switch (value)
                {
                    case 1:
                        base.ImagesWithDirection = tankGreen;
                        break;
                    case 2:
                        base.ImagesWithDirection = tankYellow;
                        break;
                    default:
                        base.ImagesWithDirection = tankGray;
                        break;
                }
            }
        }

        public override void Update(PropertiesType prop, object value)
        {
            if (prop == PropertiesType.NumberOfHits)
                NumberOfHits = (int)value;
            else
                base.Update(prop, value);
        }
    }
}

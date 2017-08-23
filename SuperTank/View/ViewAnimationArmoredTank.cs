using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.View
{
    class ViewAnimationArmoredTank : ViewAnimationTank
    {
        private readonly Dictionary<Direction, Image[]> tankGray;
        private readonly Dictionary<Direction, Image[]> tankGreen;
        private readonly Dictionary<Direction, Image[]> tankYellow;

        public ViewAnimationArmoredTank(int id, float x, float y, float width, float height, int zIndex, Dictionary<Direction, Image[]> tankGray, Dictionary<Direction, Image[]> tankGreen, Dictionary<Direction, Image[]> tankYellow) : base(id, x, y, width, height, zIndex, tankGray)
        {
            this.tankGray = tankGray;
            this.tankGreen = tankGreen;
            this.tankYellow = tankYellow;
        }

        protected override Dictionary<Direction, Image[]> Images
        {
            get
            {
                switch (NumberOfHits)
                {
                    case 1:
                        return tankGreen;
                    case 2:
                        return tankYellow;
                    default:
                        return tankGray;
                }
            }
        }

        // количество попаданий снаряда
        private int NumberOfHits
        {
            get { return (int)Properties[PropertiesType.NumberOfHits]; }
        }
    }
}

using SuperTankWPF.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SuperTankWPF.Units
{
    class CentrAnimationView : AnimationView
    {
        public CentrAnimationView(int updateInterval, ImageSource[] imgSources, bool animStart)
            : base(updateInterval, imgSources, animStart)
        { }

        protected override void UpdateSource()
        {
            double centrX = Source.Width / 2 + X;
            double centrY = Source.Height / 2 + Y;
            base.UpdateSource();
            X = centrX - Source.Width / 2;
            Y = centrY - Source.Height / 2;
        }
    }
}

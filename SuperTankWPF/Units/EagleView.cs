using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SuperTankWPF.Units
{
    class EagleView : UnitView, IDetonation
    {
        private ImageSource img;

        public EagleView(ImageSource img)
        {
            this.img = img;
        }

        public bool Detonation
        {
            get { return false; }

            set
            {
                if (value) Source = img;
            }
        }
    }
}

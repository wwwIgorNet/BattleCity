using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SuperTank;

namespace SuperTankWPF.Units
{
    class EagleView : UnitView
    {
        private ImageSource img;

        public EagleView(ImageSource img)
        {
            this.img = img;
        }

        public override void Update(PropertiesType prop, object value)
        {
            if (prop == PropertiesType.Detonation && value.Equals(true))
                Source = img;
            else
                base.Update(prop, value);
        }
    }
}

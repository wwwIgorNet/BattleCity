using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank
{
    static class Images
    {
        private static Image plainTankUp;

        public static Image PlainTankUp
        {
            get
            {
                return plainTankUp == null ? plainTankUp = Image.FromFile(Configuration.Texture + "PlainTankUp.png") : plainTankUp;
            }
        }
    }
}

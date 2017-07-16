using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.View
{
    class ViewShell : ViewDirectionUnit
    {
        private Image[] detonation;
        private int frame = 0;

        public ViewShell(int id, float x, float y, float width, float height, int zIndex, Dictionary<Direction, Image> imges, Image[] detonation)
            : base(id, x, y, width, height, zIndex, imges)
        {
            this.detonation = detonation;
            Properties[PropertiesType.Scoore] = false;
        }

        public override Image Img
        {
            get
            {
                if (Properties[PropertiesType.Scoore].Equals(false))
                    return base.Img;
                else
                {
                    if (frame == detonation.Length - 1) frame = detonation.Length - 1;
                    else frame++;

                    float centerX = X + Width / 2;
                    float centerY = Y + Height / 2;
                    Width = detonation[frame].Width;
                    Height = detonation[frame].Height;
                    X = centerX - Width / 2;
                    Y = centerY - Height / 2;

                    return detonation[frame];
                }
            }
        }
    }
}

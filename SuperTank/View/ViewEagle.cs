using System.Drawing;
using SuperTank.View;

namespace SuperTank
{
    class ViewEagle : ViewUnit
    {
        private Image eagle2;

        public ViewEagle(int id, float x, float y, float width, float height, int zIndex, Image eagle, Image eagle2) : base(id, x, y, width, height, zIndex, eagle)
        {
            this.eagle2 = eagle2;
        }

        public override Image Img
        {
            get
            {
                if (IsDetonation)
                    return eagle2;

                return base.Img;
            }
        }

        protected bool IsDetonation
        {
            get { return (bool)Properties[PropertiesType.Detonation]; }
        }
    }
}
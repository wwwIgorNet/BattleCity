using System.Drawing;

namespace SuperTank.View
{
    /// <summary>
    /// Displaying the projectile
    /// </summary>
    class ViewShell : BaseView
    {
        private Image imgeShell;
        private Image[] detonation;
        private int frame = 0;

        public ViewShell(int id, float x, float y, float width, float height, int zIndex, Image imgeShell, Image[] detonation)
            : base(id, x, y, width, height, zIndex)
        {
            this.imgeShell = imgeShell;
            this.detonation = detonation;
            Properties[PropertiesType.Detonation] = false;
        }

        public override Image Img
        {
            get
            {
                if (Properties[PropertiesType.Detonation].Equals(false))
                    return imgeShell;
                else
                {
                    if (frame >= detonation.Length) return new Bitmap(1, 1);

                    float centerX = X + Width / 2;
                    float centerY = Y + Height / 2;
                    Width = detonation[frame].Width;
                    Height = detonation[frame].Height;
                    X = centerX - Width / 2;
                    Y = centerY - Height / 2;

                    return detonation[frame++];
                }
            }
        }
    }
}

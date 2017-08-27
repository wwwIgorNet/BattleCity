using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.View
{
    class ViewPonts : BaseView
    {
        private static SizeF sizeText;
        private static Font font;

        static ViewPonts()
        {
            Bitmap img = new Bitmap(100, 100);
            Graphics g = Graphics.FromImage(img);
            font = new Font("Courier", 14, FontStyle.Bold);
            string text = 100.ToString();
            sizeText = g.MeasureString(text, font);
        }


        public ViewPonts(int id, float x, float y, float width, float height, int zIndex) : base(id, x, y, width, height, zIndex)
        {
        }

        public override Image Img
        {
            get
            {
                Bitmap img = new Bitmap((int)Width, (int)Height);
                Graphics g = Graphics.FromImage(img);
                string text = ((int)Properties[PropertiesType.Points]).ToString();

                g.DrawString(text, font, Brushes.White, Width / 2 - sizeText.Width / 2, Height / 2 - sizeText.Height / 2);

                return img;
            }
        }
    }
}

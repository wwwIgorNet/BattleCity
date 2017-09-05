using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTank.WindowsForms
{
    class Text
    {
        private Graphics graphics;
        private string str;
        private SizeF size;
        private Font font;

        public Text(string str, Graphics g)
        {
            graphics = g;
            this.str = str;
            font = new Font(ConfigurationView.InfoFontFamily, 13);
            size = g.MeasureString(str, font);
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get { return size.Width; } }
        public float Height { get { return size.Height; } }
        public Brush Brush { get; set; }

        public void Draw()
        {
            graphics.DrawString(str, font, Brush, X, Y);
        }
    }
}

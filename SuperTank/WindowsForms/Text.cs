using System.Drawing;

namespace SuperTank.WindowsForms
{
    class Text
    {
        private PointF point = new PointF();

        public Text(string str, Brush brush)
        {
            Str = str;
            Brush = brush;
        }

        public Brush Brush { get; set; }
        public float X
        {
            get { return point.X; }
            set { point.X = value; }
        }
        public float Y
        {
            get { return point.Y; }
            set { point.Y = value; }
        }
        public PointF Point
        {
            get { return point; }
            set { point = value; }
        }
        public float Width { get { return Size.Width; } }
        public float Height { get { return Size.Height; } }
        public SizeF Size { get; set; }
        public string Str { get; set; }
    }
}

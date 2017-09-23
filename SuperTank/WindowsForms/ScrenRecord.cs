using SuperTank.View;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SuperTank.WindowsForms
{
    class ScrenRecord : Label
    {
        private Font font = new Font(ConfigurationView.FontFamilyBattleCities, 55);
        private string strScore = "HISCORE";
        private string strPoints;
        private Brush brush = Brushes.White;
        private Timer timer = new Timer();
        private DateTime start;

        public ScrenRecord(int points)
        {
            strPoints = points.ToString();
            timer.Interval = ConfigurationView.TimerInterval;
            timer.Tick += Timer_Tick;
        }

        public void Start()
        {
            start = DateTime.Now;
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            SizeF sizeScore = g.MeasureString(strScore, font);
            SizeF sizePoints = g.MeasureString(strPoints, font);

            float y = 150;
            float x = Width / 2 - sizeScore.Width / 2;
            g.DrawString(strScore, font, brush, x, y);
            g.DrawString(strPoints, font, brush, x + sizeScore.Width - sizePoints.Width, y + sizeScore.Height);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (brush == Brushes.White)
            {
                brush = Brushes.Yellow;
            }
            else if (brush == Brushes.Yellow)
            {
                brush = Brushes.Blue;
            }
            else
            {
                brush = Brushes.White;
            }
            Invalidate();

            if (DateTime.Now - start >= ConfigurationView.DelayScrenRecord)
            {
                timer.Stop();
            }
        }
    }
}

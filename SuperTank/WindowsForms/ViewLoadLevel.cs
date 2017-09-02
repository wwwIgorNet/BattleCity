using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTank.WindowsForms
{
    class ViewLoadLevel : Label
    {
        private bool isOpening;
        private Timer timer = new Timer();
        private float centrScrean = ConfigurationView.WindowClientHeight / 2;
        private float height;


        public ViewLoadLevel()
        {
            timer.Interval = ConfigurationView.TimerInterval;
            timer.Tick += Timer_Tick;
            this.BackColor = System.Drawing.Color.Transparent;
            this.GraphicsOption();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isOpening)
            {
                if (height < centrScrean)
                {
                    height += 10;
                    Invalidate();
                }
                else
                {
                    timer.Stop();
                }
            }
            else
            {
                if (height > 0)
                {
                    height -= 10;
                    Invalidate();
                }
                else
                {
                    timer.Stop();
                }
            }
        }

        public void CloseScrean()
        {
            isOpening = false;
            height = 0;
            timer.Start();
        }

        public void OpenScrean()
        {
            isOpening = true;
            height = centrScrean;
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(ConfigurationView.BackColor), 0, 0, ConfigurationView.WindowClientWidth, height);
            g.FillRectangle(new SolidBrush(ConfigurationView.BackColor), 0, ConfigurationView.WindowClientHeight - height, ConfigurationView.WindowClientWidth, height);
        }

        private void GraphicsOption()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}

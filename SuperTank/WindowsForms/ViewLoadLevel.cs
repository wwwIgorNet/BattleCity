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
    class ViewLoadLevel
    {
        private Bitmap imgLoadLevel = new Bitmap(ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);
        private bool isOpening;
        private Timer timer = new Timer();
        private float centrScrean = ConfigurationView.WindowClientHeight / 2;
        private float height;
        private DateTime startDelay;

        public Image ImgLoadLevel { get { return imgLoadLevel; } }

        public ViewLoadLevel()
        {
            timer.Interval = ConfigurationView.TimerInterval;
            timer.Tick += Timer_Tick;
        }

        public void Start()
        {
            isOpening = false;
            height = 0;
            timer.Start();
        }

        public void UpdateImage()
        {
            Graphics g = Graphics.FromImage(imgLoadLevel);
            g.Clear(Color.Transparent);
            g.FillRectangle(new SolidBrush(ConfigurationView.BackColor), 0, 0, imgLoadLevel.Width, height);
            g.FillRectangle(new SolidBrush(ConfigurationView.BackColor), 0, imgLoadLevel.Height - height, imgLoadLevel.Width, height);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isOpening)
            {
                if (height < centrScrean)
                {
                    height += 7;
                    UpdateImage();
                }
                else
                {
                    isOpening = true;
                    startDelay = DateTime.Now;
                }
            }
            else if (DateTime.Now - startDelay < TimeSpan.FromSeconds(1))
            {
                return;
            }
            else if(isOpening)
            {
                if (height > 0)
                {
                    height -= 7;
                    UpdateImage();
                }
                else
                {
                    timer.Stop();
                }
            }
        }
    }
}

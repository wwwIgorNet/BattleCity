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
        private TimeSpan timeCloseOrOpen;
        private DateTime startTime;
        private int curentLevel;
        private string infoText;
        
        public Image ImgLoadLevel { get { return imgLoadLevel; } }
        public event Action EndClose;

        public ViewLoadLevel()
        {
            timer.Interval = ConfigurationView.TimerInterval;
            timer.Tick += Timer_Tick;
        }

        public void Start(int level)
        {
            curentLevel = level;
            isOpening = false;
            height = 0;
            timer.Start();
            infoText = null;
            startTime = DateTime.Now;
        }

        public void UpdateImage()
        {
            Graphics g = Graphics.FromImage(imgLoadLevel);
            g.Clear(Color.Transparent);
            g.FillRectangle(new SolidBrush(ConfigurationView.BackColor), 0, 0, imgLoadLevel.Width, height);
            g.FillRectangle(new SolidBrush(ConfigurationView.BackColor), 0, imgLoadLevel.Height - height, imgLoadLevel.Width, height);

            if (infoText != null)
            {
                SizeF sizeText = g.MeasureString(infoText, ConfigurationView.InfoFont);
                float x = imgLoadLevel.Width / 2 - sizeText.Width / 2, y = imgLoadLevel.Height / 2 - sizeText.Height / 2;
                g.DrawString(infoText, ConfigurationView.InfoFont, Brushes.Black, x, y);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isOpening)
            {
                if (height < centrScrean)
                {
                    height += 10;
                    UpdateImage();
                }
                else
                {
                    isOpening = true;
                    timeCloseOrOpen = DateTime.Now - startTime;
                    infoText = "STAGE " + curentLevel;
                    if (EndClose != null) EndClose.Invoke();
                    UpdateImage();
                }
            }
            else if (DateTime.Now - startTime + timeCloseOrOpen < TimeSpan.FromSeconds(3))
                return;

            else if(isOpening)
            {
                if (height > 0)
                {
                    infoText = null;
                    height -= 10;
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

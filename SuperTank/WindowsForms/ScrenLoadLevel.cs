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
    class ScrenLoadLevel : BaseScren
    {
        private Font font = new Font(ConfigurationView.InfoFontFamily, 15);
        private bool isOpening;
        private float centrScrean = ConfigurationView.WindowClientHeight / 2;
        private float height;
        private TimeSpan timeCloseOrOpen;
        private int curentLevel;
        private string infoText;
        
        public event Action EndClose;

        public void Start(int level)
        {
            Start();
            curentLevel = level;
            isOpening = false;
            height = 0;
            infoText = null;
        }

        public override void UpdateImage()
        {
            Graphics g = Graphics.FromImage(ImgScren);
            g.Clear(Color.Transparent);
            g.FillRectangle(new SolidBrush(ConfigurationView.BackColor), 0, 0, Width, height);
            g.FillRectangle(new SolidBrush(ConfigurationView.BackColor), 0, Height - height, Width, height);

            if (infoText != null)
            {
                SizeF sizeText = g.MeasureString(infoText, font);
                float x = Width / 2 - sizeText.Width / 2, y = Height / 2 - sizeText.Height / 2;
                g.DrawString(infoText, font, Brushes.Black, x, y);
            }
        }

        protected override void Timer_Tick(object sender, EventArgs e)
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
                    timeCloseOrOpen = DateTime.Now - StartTime;
                    isOpening = true;
                    infoText = "STAGE " + curentLevel;
                    UpdateImage();
                    if (EndClose != null) EndClose.Invoke();
                }
            }
            else if (DateTime.Now - StartTime + timeCloseOrOpen < ConfigurationView.DelayScrenLoadLevel)
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
                    TimerStop();
                    IsAcive = false;
                }
            }
        }
    }
}

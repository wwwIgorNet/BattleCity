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
    abstract class BaseScren
    {
        private Bitmap imgScren = new Bitmap(ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);
        private Timer timer = new Timer();
        private DateTime startTime;

        public BaseScren()
        {
            timer.Interval = ConfigurationView.TimerInterval;
            timer.Tick += Timer_Tick;
        }

        public void Start()
        {
            startTime = DateTime.Now;
            IsAcive = true;
            TimerStart();
        }

        public abstract void UpdateImage();

        public bool IsAcive { get; set; }
        public Image ImgScren { get { return imgScren; } }
        public float Width { get { return imgScren.Width; } }
        public float Height { get { return imgScren.Height; } }

        protected DateTime StartTime { get { return startTime; } }

        protected void TimerStart()
        {
            timer.Start();
        }
        protected void TimerStop()
        {
            timer.Stop();
        }

        protected abstract void Timer_Tick(object sender, EventArgs e);
    }
}

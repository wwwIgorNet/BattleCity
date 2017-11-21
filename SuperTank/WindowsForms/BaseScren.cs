using SuperTank.View;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SuperTank.WindowsForms
{
    /// <summary>
    /// The screen is updated
    /// </summary>
    abstract class BaseScren
    {
        private Bitmap imgScren = new Bitmap(ConfigurationWinForms.WindowClientWidth, ConfigurationWinForms.WindowClientHeight);
        private Timer timer = new Timer();
        private DateTime startTime;

        public BaseScren()
        {
            timer.Interval = ConfigurationWinForms.TimerInterval;
            timer.Tick += Timer_Tick;
        }

        public bool IsAcive { get; set; }
        public Image ImgScren { get { return imgScren; } }
        public float Width { get { return imgScren.Width; } }
        public float Height { get { return imgScren.Height; } }

        protected DateTime StartTime { get { return startTime; } }

        public virtual void Start()
        {
            startTime = DateTime.Now;
            IsAcive = true;
            TimerStart();
        }
        public abstract void UpdateImage();

        protected void SetInterval(int interval)
        {
            timer.Interval = interval;
        }
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

using SuperTankWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Media;

namespace SuperTankWPF.Units
{
    class AnimationView : UnitView
    {
        private Timer timer = new Timer();
        private Action method;
        private int frame;
        private ImageSource[] imagesSources;

        public AnimationView(int updateInterval, ImageSource[] imgSources, bool animStart)
        {
            method = new Action(UpdateSource);
            ImagesSources = imgSources;
            CountFrame = imgSources.Length;
            timer.Interval = ConfigurationWPF.TimerInterval * updateInterval;
            timer.Elapsed += Timer_Elapsed;

            if (animStart) AnimStart();
        }

        public AnimationView(int updateInterval, ImageSource[] imgSources)
            :this(updateInterval, imgSources, false)
        { }

        protected ImageSource[] ImagesSources
        {
            get { return imagesSources; }
            set
            {
                imagesSources = value;
                Source = imagesSources[frame];
            }
        }
        public int CountFrame { get; set; }

        protected void AnimStop()
        {
            timer.Stop();
        }
        public void AnimStart()
        {
            timer.Start();
        }
        protected virtual void UpdateSource()
        {
            this.Source = imagesSources[frame];
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            frame = frame >= CountFrame - 1 ? 0 : frame + 1;
            this.Dispatcher.BeginInvoke(method);
        }

    }
}

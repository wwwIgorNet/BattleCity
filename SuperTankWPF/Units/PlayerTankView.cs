using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SuperTank;
using System.Windows;
using System.Windows.Media.Imaging;
using SuperTankWPF.Model;
using Microsoft.Practices.ServiceLocation;
using SuperTankWPF.ViewModel;
using System.Timers;

namespace SuperTankWPF.Units
{
    class PlayerTankView : TankView, IInvulnerable
    {
        private ImageSource[] invulnerable;
        private bool isInvulnerable;
        private Timer timer = new Timer(ConfigurationWPF.TimerInterval * 3);
        private int frame;
        private Action invalidateRender;

        public PlayerTankView(Direction direction, Dictionary<Direction, ImageSource[]> imgSources, ImageSource[] invulnerable, int updateInterval) : base(direction, imgSources, updateInterval)
        {
            invalidateRender = new Action(InvalidateRender);
            this.invulnerable = invulnerable;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(invalidateRender);
        }

        public bool IsInvulnerable
        {
            get { return isInvulnerable; }
            set
            {
                isInvulnerable = value;
                if (value) timer.Start();
                else
                {
                    timer.Stop();
                    InvalidateRender();
                }
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (IsInvulnerable)
            {
                frame = frame == 0 ? 1 : 0;
                dc.DrawImage(invulnerable[frame], new Rect(0, 0, ActualWidth, ActualHeight));
            }
        }

        private bool Invalidate
        {
            get { return (bool)GetValue(FremeProperty); }
            set { SetValue(FremeProperty, value); }
        }

        private static readonly DependencyProperty FremeProperty = DependencyProperty.Register("Invalidate", typeof(bool), typeof(PlayerTankView), new FrameworkPropertyMetadata(false) { AffectsRender = true });

       private void InvalidateRender()
        {
            Invalidate = !Invalidate;
        }
    }
}

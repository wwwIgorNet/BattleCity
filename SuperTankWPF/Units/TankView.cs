using SuperTank;
using SuperTankWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SuperTankWPF.Units
{
    class TankView : UnitView, IDirection, IParking
    {
        private int frame = 0;
        private Dictionary<Direction, ImageSource[]> imgSources;
        private Timer timer = new Timer(ConfigurationWPF.TimerInterval * 4);

        public TankView(Direction direction, Dictionary<Direction,ImageSource[]> imgSources)
        {
            this.imgSources = imgSources;
            Source = imgSources[direction][frame];
            Direction = direction;
            timer.Elapsed += Timer_Elapsed;
        }

        public int MaCountFrame { get; set; } = 2;

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            frame = frame >= MaCountFrame - 1 ? 0 : frame + 1;
            this.Dispatcher.BeginInvoke((Action) delegate () {
                this.Source = imgSources[this.Direction][frame];
            });
        }

        public Direction Direction
        {
            get { return (Direction)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }
        
        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register("Direction", typeof(Direction), typeof(TankView), new PropertyMetadata(DirectionChangedCallback));

        private static void DirectionChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TankView tv = (TankView)d;
            tv.Source = tv.imgSources[(Direction)e.NewValue][tv.frame];
        }

        public bool IsParking
        {
            get { return (bool)GetValue(IsParkingProperty); }
            set { SetValue(IsParkingProperty, value); }
        }
        
        public static readonly DependencyProperty IsParkingProperty =
            DependencyProperty.Register("IsParking", typeof(bool), typeof(TankView), new PropertyMetadata(true, IsParkingChangedCallback));

        private static void IsParkingChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue.Equals(true))
                ((TankView)d).timer.Stop();
            else ((TankView)d).timer.Start();
        }
    }
}

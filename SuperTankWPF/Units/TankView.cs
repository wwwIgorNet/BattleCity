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
    class TankView : AnimationView, IDirection, IParking
    {
        public TankView(Direction direction, Dictionary<Direction,ImageSource[]> imgSources, int updateInterval)
            : base(updateInterval, imgSources[direction])
        {
            this.ImagesWithDirection = imgSources;
            Direction = direction;
        }

        public Dictionary<Direction, ImageSource[]> ImagesWithDirection
        {
            get { return imagesWithDirection; }
            set
            {
                imagesWithDirection = value;
                ImagesSources = imagesWithDirection[this.Direction];
            }
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
            tv.ImagesSources = tv.ImagesWithDirection[(Direction)e.NewValue];
        }

        public bool IsParking
        {
            get { return (bool)GetValue(IsParkingProperty); }
            set { SetValue(IsParkingProperty, value); }
        }
        
        public static readonly DependencyProperty IsParkingProperty =
            DependencyProperty.Register("IsParking", typeof(bool), typeof(TankView), new PropertyMetadata(true, IsParkingChangedCallback));
        private Dictionary<Direction, ImageSource[]> imagesWithDirection;

        private static void IsParkingChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue.Equals(true))
                ((TankView)d).AnimStop();
            else ((TankView)d).AnimStart();
        }
    }
}

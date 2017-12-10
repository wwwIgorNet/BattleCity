using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SuperTankWPF.Units
{
    class ShellView : AnimationView, IDetonation
    {
        public ShellView(int updateInterval, ImageSource[] detonation)
            : base(updateInterval, detonation)
        { }

        protected override void UpdateSource()
        {
            double centrX = Source.Width / 2 + X;
            double centrY = Source.Height / 2 + Y;
            base.UpdateSource();
            X = centrX - Source.Width / 2;
            Y = centrY - Source.Height / 2;
        }

        public bool Detonation
        {
            get { return (bool)GetValue(DetonationProperty); }
            set { SetValue(DetonationProperty, value); }
        }

        public static readonly DependencyProperty DetonationProperty =
            DependencyProperty.Register("Detonation", typeof(bool), typeof(ShellView), new PropertyMetadata(false, DwetonationChangedCallback));

        private static void DwetonationChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
            {
                ((ShellView)d).AnimStart();
            }
        }
    }
}

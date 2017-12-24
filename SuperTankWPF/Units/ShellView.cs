using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using SuperTank;

namespace SuperTankWPF.Units
{
    class ShellView : CentrAnimationView
    {
        public ShellView(int updateInterval, ImageSource[] detonation)
            : base(updateInterval, detonation, false)
        { }

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

        public override void Update(PropertiesType prop, object value)
        {
            if (prop == PropertiesType.Detonation)
                this.SetValue(DetonationProperty, value);
            else
                base.Update(prop, value);
        }
    }
}

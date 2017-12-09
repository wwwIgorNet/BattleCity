using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SuperTankWPF.Units.View
{
    class UnitView : Image
    {
        public UnitView()
        {
            this.DataContextChanged += UnitView_DataContextChanged;
        }

        private void UnitView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Binding bindingId = new Binding();
            bindingId.Source = this.DataContext;
            bindingId.Path = new PropertyPath("ID");
            bindingId.Mode = BindingMode.OneWay;
            this.SetBinding(UnitView.IDProperty, bindingId);

            Binding bindingX = new Binding();
            bindingX.Source = this.DataContext;
            bindingX.Path = new PropertyPath("X");
            bindingX.Mode = BindingMode.OneWay;
            this.SetBinding(UnitView.XProperty, bindingX);

            Binding bindingY = new Binding();
            bindingY.Source = this.DataContext;
            bindingY.Path = new PropertyPath("Y");
            bindingY.Mode = BindingMode.OneWay;
            this.SetBinding(UnitView.YProperty, bindingY);
        }

        #region DependencyProperty
        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(UnitView), new PropertyMetadata(0));

        public int ZIndex
        {
            get { return (int)GetValue(ZIndexProperty); }
            set { SetValue(ZIndexProperty, value); }
        }

        public static readonly DependencyProperty ZIndexProperty =
            DependencyProperty.Register("ZIndex", typeof(int), typeof(UnitView), new PropertyMetadata(0, ZIndexChangedCallback));

        private static void ZIndexChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((UnitView)d).SetValue(Canvas.ZIndexProperty, e.NewValue);
        }

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(UnitView), new PropertyMetadata(0.0, xChangedCallback));

        private static void xChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((UnitView)d).SetValue(Canvas.LeftProperty, e.NewValue);
        }

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(UnitView), new PropertyMetadata(0.0, yChangedCallback));

        private static void yChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((UnitView)d).SetValue(Canvas.TopProperty, e.NewValue);
        }
        #endregion
    }
}

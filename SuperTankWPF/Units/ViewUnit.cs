using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SuperTankWPF.Units
{
    class ViewUnit : Image
    {
        public ViewUnit()
        {
        }

        public ViewUnit(int id, float x, float y, BitmapImage brickWall)
        {
            ID = id;
            X = x;
            Y = y;
            Source = brickWall;
        }

        #region DependencyProperty
        public int ZIndex
        {
            get { return (int)GetValue(ZIndexProperty); }
            set { SetValue(ZIndexProperty, value); }
        }
        
        public static readonly DependencyProperty ZIndexProperty =
            DependencyProperty.Register("ZIndex", typeof(int), typeof(ViewUnit), new PropertyMetadata(0, ZIndexChangedCallback));

        private static void ZIndexChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ViewUnit)d).SetValue(Canvas.ZIndexProperty, e.NewValue);
        }

        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }
        
        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(ViewUnit));

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(ViewUnit), new PropertyMetadata(0.0, xChangedCallback));

        private static void xChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ViewUnit)d).SetValue(Canvas.TopProperty, e.NewValue);
        }

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(ViewUnit), new PropertyMetadata(0.0, yChangedCallback));

        private static void yChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ViewUnit)d).SetValue(Canvas.LeftProperty, e.NewValue);
        }
        #endregion
    }
}

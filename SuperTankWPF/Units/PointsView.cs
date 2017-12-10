using SuperTank;
using SuperTankWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SuperTankWPF.Units
{
    class PointsView : UnitView
    {
        private int points;

        public PointsView(int points)
        {
            this.points = points;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            FontFamily ff =  (FontFamily)this.FindResource("fontFamilyTextInfo");
            FormattedText ft = new FormattedText(points.ToString(), Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, new Typeface(ff, new FontStyle(), new FontWeight(), new FontStretch()), this.FontSize, this.Brush);
            dc.DrawText(ft, new Point(-ConfigurationWPF.WidthTile, 4));
        }
        
        public int FontSize
        {
            get { return (int)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(int), typeof(PointsView), new PropertyMetadata(14));
        
        public Brush Brush
        {
            get { return (Brush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }

        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register("Brush", typeof(Brush), typeof(PointsView), new PropertyMetadata(Brushes.White));
    }
}

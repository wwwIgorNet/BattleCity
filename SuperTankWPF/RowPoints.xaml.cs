using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperTankWPF
{
    /// <summary>
    /// Interaction logic for RowPoints.xaml
    /// </summary>
    public partial class RowPoints : UserControl
    {
        public RowPoints()
        {
            InitializeComponent();

            Binding bind = new Binding();
            bind.Source = centr;
            bind.Path = new PropertyPath("ActualWidth");
            bind.Mode = BindingMode.OneWay;
            this.SetBinding(RowPoints.WidthCentrProperty, bind);
        }

        public void Clear()
        {
            PTS1Player = -1;
            CountTank1Player = -1;
            PTS2Player = -1;
            CountTank2Player = -1;
        }

        public static void SetText(int num, TextBlock textBlock, bool aligmentRight)
        {
            if (num < 0) textBlock.Text = "  ";
            else if (num < 10)
            {
                if(aligmentRight) textBlock.Text = " " + num;
                else textBlock.Text = num + " ";

            }
            else textBlock.Text = "" + num;
        }
        
        public double WidthCentr
        {
            get { return (double)GetValue(WidthCentrProperty); }
            set { SetValue(WidthCentrProperty, value); }
        }

        public static readonly DependencyProperty WidthCentrProperty =
            DependencyProperty.Register("WidthCentr", typeof(double), typeof(RowPoints), new PropertyMetadata(0.0));

        public string ImgSource
        {
            get { return (string)GetValue(ImgSourceProperty); }
            set { SetValue(ImgSourceProperty, value); }
        }

        public static readonly DependencyProperty ImgSourceProperty =
            DependencyProperty.Register("ImgSource", typeof(string), typeof(RowPoints), new PropertyMetadata(ImgSourceChangedCallback));

        private static void ImgSourceChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RowPoints)d).imgTank.Source = new BitmapImage(new Uri((string)e.NewValue, UriKind.Relative));
        }

        public int PTS1Player
        {
            get { return (int)GetValue(PTS1PlayerProperty); }
            set { SetValue(PTS1PlayerProperty, value); }
        }
        
        public static readonly DependencyProperty PTS1PlayerProperty =
            DependencyProperty.Register("PTS1Player", typeof(int), typeof(RowPoints), new PropertyMetadata(-1, PTS1PlayerChangedCallback));

        private static void PTS1PlayerChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SetText((int)e.NewValue, ((RowPoints)d).pts1Player, true);
        }

        public int CountTank1Player
        {
            get { return (int)GetValue(CountTank1PlayerProperty); }
            set { SetValue(CountTank1PlayerProperty, value); }
        }

        public static readonly DependencyProperty CountTank1PlayerProperty =
            DependencyProperty.Register("CountTank1Player", typeof(int), typeof(RowPoints), new PropertyMetadata(-1, CountTank1PlayerChangedCallback));

        private static void CountTank1PlayerChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SetText((int)e.NewValue, ((RowPoints)d).countTank1Player, true);
        }

        public int PTS2Player
        {
            get { return (int)GetValue(PTS2PlayerProperty); }
            set { SetValue(PTS2PlayerProperty, value); }
        }

        public static readonly DependencyProperty PTS2PlayerProperty =
            DependencyProperty.Register("PTS2Player", typeof(int), typeof(RowPoints), new PropertyMetadata(-1, PTS2PlayerChangedCallback));

        private static void PTS2PlayerChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SetText((int)e.NewValue, ((RowPoints)d).pts2Player, false);
        }
        
        public int CountTank2Player
        {
            get { return (int)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(int), typeof(RowPoints), new PropertyMetadata(-1, CountTank2PlayerChangedCallback));

        private static void CountTank2PlayerChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SetText((int)e.NewValue, ((RowPoints)d).countTank2Player, false);
        }
    }
}

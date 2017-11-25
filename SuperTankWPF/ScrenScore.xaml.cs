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
    /// Interaction logic for ScrenScore.xaml
    /// </summary>
    public partial class ScrenScore : UserControl
    {
        public ScrenScore()
        {
            InitializeComponent();
        }

        public int Points1Player
        {
            get { return (int)GetValue(Points1PlayerProperty); }
            set { SetValue(Points1PlayerProperty, value); }
        }

        public static readonly DependencyProperty Points1PlayerProperty =
            DependencyProperty.Register("Points1Player", typeof(int), typeof(ScrenScore), new PropertyMetadata(-1, Points1PlayerChangedCallback));

        private static void Points1PlayerChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RowPoints.SetText((int)e.NewValue, ((ScrenScore)d).points1Player, true);
        }

        public int Points2Player
        {
            get { return (int)GetValue(Points2PlayerProperty); }
            set { SetValue(Points2PlayerProperty, value); }
        }

        public static readonly DependencyProperty Points2PlayerProperty =
            DependencyProperty.Register("Points2Player", typeof(int), typeof(ScrenScore), new PropertyMetadata(-1, Points2PlayerChangedCallback));

        private static void Points2PlayerChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RowPoints.SetText((int)e.NewValue, ((ScrenScore)d).points2Player, false);
        }

        public int TotalTank1P
        {
            get { return (int)GetValue(TotalTank1PProperty); }
            set { SetValue(TotalTank1PProperty, value); }
        }

        public static readonly DependencyProperty TotalTank1PProperty =
            DependencyProperty.Register("TotalTank1P", typeof(int), typeof(SceneScene), new PropertyMetadata(-1, TotalTank1PChangedCallback));

        private static void TotalTank1PChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RowPoints.SetText((int)e.NewValue, ((ScrenScore)d).totalTank1P, true);
        }

        public int TotalTank2P
        {
            get { return (int)GetValue(TotalTank2PProperty); }
            set { SetValue(TotalTank2PProperty, value); }
        }

        public static readonly DependencyProperty TotalTank2PProperty =
            DependencyProperty.Register("TotalTank2P", typeof(int), typeof(SceneScene), new PropertyMetadata(-1, TotalTank2PChangedCallback));

        private static void TotalTank2PChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RowPoints.SetText((int)e.NewValue, ((ScrenScore)d).totalTank2P, true);
        }
    }
}

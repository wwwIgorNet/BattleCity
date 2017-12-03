using SuperTank.View;
using SuperTankWPF.Model;
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
    /// Interaction logic for LevelInfo.xaml
    /// </summary>
    public partial class LevelInfo : UserControl
    {
        private BitmapImage bitmapImage = BitmapImages.InformationTank;

        public LevelInfo()
        {
            InitializeComponent();
        }

        #region DependencyProperty
        public int Level
        {
            get { return (int)GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }

        public static readonly DependencyProperty LevelProperty =
            DependencyProperty.Register("Level", typeof(int), typeof(LevelInfo), new PropertyMetadata(0, LevelChangedCallback));

        private static void LevelChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LevelInfo)d).labelLevel.Content = e.NewValue;
        }


        public int CountTank1Player
        {
            get { return (int)GetValue(CountTank1PlayerProperty); }
            set { SetValue(CountTank1PlayerProperty, value); }
        }

        public static readonly DependencyProperty CountTank1PlayerProperty =
            DependencyProperty.Register("CountTank1Player", typeof(int), typeof(LevelInfo), new PropertyMetadata(0, CountTank1PlayerChangedCallback));

        private static void CountTank1PlayerChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LevelInfo)d).label1Player.Content = e.NewValue;
        }


        public int CountTankEnemy
        {
            get { return (int)GetValue(CountTankEnemyProperty); }
            set { SetValue(CountTankEnemyProperty, value); }
        }
        
        public static readonly DependencyProperty CountTankEnemyProperty =
            DependencyProperty.Register("CountTankEnemy", typeof(int), typeof(LevelInfo), new PropertyMetadata(0, CountTankEnemyChangedCallback));

        private static void CountTankEnemyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LevelInfo l = (LevelInfo)d;
            int r = (int)e.NewValue - (int)e.OldValue;
            if (r > 0) l.AddTankEnemy(r);
            else if (r < 0) l.RemoveTankkEnmy(-r);
        }
        #endregion

        private void RemoveTankkEnmy(int countTank)
        {
            uniformGrid.Children.RemoveRange(uniformGrid.Children.Count - countTank - 1, countTank);
        }

        private void AddTankEnemy(int countTank)
        {
            for (int i = 0; i < countTank; i++)
            {
                uniformGrid.Children.Add(new Image() { Source = bitmapImage });
            }
        }
    }
}

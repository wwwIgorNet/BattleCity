using SuperTankWPF.Model;
using SuperTankWPF.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SuperTankWPF.View
{
    /// <summary>
    /// Interaction logic for LevelInfo.xaml
    /// </summary>
    public partial class LevelInfo : UserControl
    {
        private ImageSource tankEnemy;

        public LevelInfo()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            tankEnemy = ((Image)FindResource("tankEnemy")).Source;
            dashboardInfo = ((Image)FindResource("backgroundImgIPlayer")).Source;
            dashboardInfoIIPlayer = ((Image)FindResource("backgroundImgIIPlayer")).Source;

            Binding bindingCountTankEnemy = new Binding();
            bindingCountTankEnemy.Source = this.DataContext;
            bindingCountTankEnemy.Path = new PropertyPath("CountTankEnemy");
            bindingCountTankEnemy.Mode = BindingMode.OneWay;
            this.SetBinding(LevelInfo.CountTankEnemyProperty, bindingCountTankEnemy);
            
            Binding bindingIsTwoPlayer = new Binding();
            bindingIsTwoPlayer.Source = this.DataContext;
            bindingIsTwoPlayer.Path = new PropertyPath("IsTwoPlayer");
            bindingIsTwoPlayer.Mode = BindingMode.OneWay;
            this.SetBinding(LevelInfo.IsTwoPlayerProperty, bindingIsTwoPlayer);
        }

        #region DependencyProperty
        public bool IsTwoPlayer
        {
            get { return (bool)GetValue(IsTwoPlayerProperty); }
            set { SetValue(IsTwoPlayerProperty, value); }
        }
        
        public static readonly DependencyProperty IsTwoPlayerProperty =
            DependencyProperty.Register("IsTwoPlayer", typeof(bool), typeof(LevelInfo), new PropertyMetadata(false, IsTwoPlayerChangedCallback));
        
        private static void IsTwoPlayerChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
                ((LevelInfo)d).backgroundImg.Source = dashboardInfoIIPlayer;
            else
                ((LevelInfo)d).backgroundImg.Source = dashboardInfo;
        }
        

        public int CountTankEnemy
        {
            get { return (int)GetValue(CountTankEnemyProperty); }
            set { SetValue(CountTankEnemyProperty, value); }
        }

        public static readonly DependencyProperty CountTankEnemyProperty =
            DependencyProperty.Register("CountTankEnemy", typeof(int), typeof(LevelInfo), new PropertyMetadata(0, CountTankEnemyChangedCallback));
        private static ImageSource dashboardInfo;
        private static ImageSource dashboardInfoIIPlayer;

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
            countTankEnemy.Children.RemoveRange(countTankEnemy.Children.Count - countTank - 1, countTank);
        }

        private void AddTankEnemy(int countTank)
        {
            for (int i = 0; i < countTank; i++)
            {
                countTankEnemy.Children.Add(new Image() { Source = tankEnemy });
            }
        }
    }
}

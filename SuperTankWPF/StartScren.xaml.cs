using SuperTank.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperTankWPF
{
    /// <summary>
    /// Interaction logic for StartScren.xaml
    /// </summary>
    public partial class StartScren : UserControl
    {
        private int indexMenu;
        private Image[] imageCursor;
        private ThicknessAnimation animation = new ThicknessAnimation();

        public StartScren()
        {
            InitializeComponent();

            imageCursor = new Image[]
            {
                imageFor1Player,
                imageFor2Players,
                imageForConstruction
            };

            animation.Duration = new Duration(TimeSpan.FromSeconds(4));
            SineEase easingFunction = new SineEase();
            easingFunction.EasingMode = EasingMode.EaseOut;
            animation.EasingFunction = easingFunction;
            animation.Completed += Animation_Completed;
        }

        public MainMenu CurentMenu { get { return (MainMenu)indexMenu; } }
        public bool IsAcive { get; set; }

        public void BeginLoad()
        {
            IsAcive = false;
            animation.From = new Thickness(0, this.ActualHeight, 0, 0);
            animation.To = new Thickness(0, 0, 0, 0);
            border.BeginAnimation(StackPanel.MarginProperty, animation);
            foreach (var c in imageCursor)
                c.Visibility = Visibility.Hidden;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (!IsAcive) return;

            if(e.Key == Key.Up) MenuUp();
            else if(e.Key == Key.Down) MenuDown();
        }

        private void MenuDown()
        {
            imageCursor[indexMenu].Visibility = Visibility.Hidden;
            if (indexMenu < 2) indexMenu++;
            imageCursor[indexMenu].Visibility = Visibility.Visible;
        }

        private void MenuUp()
        {
            imageCursor[indexMenu].Visibility = Visibility.Hidden;
            if (indexMenu > 0) indexMenu--;
            imageCursor[indexMenu].Visibility = Visibility.Visible;
        }

        private void Animation_Completed(object sender, EventArgs e)
        {
            IsAcive = true;
            indexMenu = 0;
            imageCursor[indexMenu].Visibility = Visibility.Visible;
        }
    }

    public enum MainMenu
    {
        Player,
        Players,
        Constructor
    }
}

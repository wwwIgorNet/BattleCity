using SuperTank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
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
using System.Globalization;
using SuperTankWPF.Model;
using SuperTankWPF.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace SuperTankWPF.View
{
    /// <summary>
    /// Interaction logic for ScrenGame.xaml
    /// </summary>
    public partial class ScreenGame : UserControl
    {
        private ScreenGameViewModel viewModel = ServiceLocator.Current.GetInstance<ScreenGameViewModel>();
        private EasingFunctionBase easingFunction;
        private ThicknessAnimation animationGameOver = new ThicknessAnimation();
        private DoubleAnimation animationTop;
        private DoubleAnimation animationBottom;
        private ObjectAnimationUsingKeyFrames animationText = new ObjectAnimationUsingKeyFrames();
        private TimeSpan durationShowLevel = TimeSpan.FromSeconds(ConfigurationWPF.DelayScrenLoadLevel.Seconds / 3);
        private TimeSpan durationClosOpenLevel = TimeSpan.FromSeconds(ConfigurationWPF.DelayScrenLoadLevel.Seconds / 3);
        private TimeSpan durationGameOver = TimeSpan.FromMilliseconds(ConfigurationWPF.TimeGameOver / 2);

        public ScreenGame()
        {
            InitializeComponent();

            this.Loaded += ScrenGame_Loaded;
            this.IsVisibleChanged += ScrenGame_IsVisibleChanged;
            Focusable = true;
        }

        private void ScrenGame_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true)) this.Focus();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.Key)
            {
                case Key.P:
                    CommandPause();
                    break;
                case Key.Up:
                case Key.Down:
                case Key.Left:
                case Key.Right:
                case Key.Space:
                    viewModel?.KeyDown(e.Key);
                    break;
            }
        }

        private void CommandPause()
        {
            ServiceLocator.Current.GetInstance<ScreenGameViewModel>().CommandPause.Execute(!IsPause);
        }

        private void AnimationPause(bool pause)
        {   
            ThicknessAnimation animationPause = new ThicknessAnimation();
            if (pause)
            {
                rectPause.Visibility = Visibility.Visible;
                animationPause.Duration = TimeSpan.FromSeconds(3);
                animationPause.EasingFunction = new ElasticEase() { EasingMode = EasingMode.EaseOut };
                animationPause.To = new Thickness(0, this.ActualHeight / 2 - textBlockPause.ActualHeight / 2, 0, 0);
            }
            else
            {
                rectPause.Visibility = Visibility.Collapsed;
                animationPause.Duration = TimeSpan.FromSeconds(1);
                animationPause.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut };
                animationPause.To = new Thickness(0, this.ActualHeight, 0, 0);
            }
            textBlockPause.BeginAnimation(TextBlock.MarginProperty, animationPause);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            switch (e.Key)
            {
                case Key.Up:
                case Key.Down:
                case Key.Left:
                case Key.Right:
                case Key.Space:
                    viewModel?.KeyUp(e.Key);
                    break;
            }
        }

        #region Dependensy
        public bool IsPause
        {
            get { return (bool)GetValue(IsPauseProperty); }
            set { SetValue(IsPauseProperty, value); }
        }
        
        public static readonly DependencyProperty IsPauseProperty =
            DependencyProperty.Register("IsPause", typeof(bool), typeof(ScreenGame), new PropertyMetadata(false, IsPauseChangedCallback));

        private static void IsPauseChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ScreenGame)d).AnimationPause((bool)e.NewValue);
        }

        public bool IsShowAnimationNewLevel
        {
            get { return (bool)GetValue(IsShowAnimationNewLevelProperty); }
            set { SetValue(IsShowAnimationNewLevelProperty, value); }
        }

        public static readonly DependencyProperty IsShowAnimationNewLevelProperty =
            DependencyProperty.Register("IsShowAnimationNewLevel", typeof(bool), typeof(ScreenGame), new PropertyMetadata(false, IsShowAnimationNewLevelChangedCallback));

        private static void IsShowAnimationNewLevelChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
                ((ScreenGame)d).StartAnimationNewLevel();
        }


        public bool IsShowGameOver
        {
            get { return (bool)GetValue(IsShowGameOverProperty); }
            set { SetValue(IsShowGameOverProperty, value); }
        }

        public static readonly DependencyProperty IsShowGameOverProperty =
            DependencyProperty.Register("IsShowGameOver", typeof(bool), typeof(ScreenGame), new PropertyMetadata(false, IsShowGameOverChangedCallback));

        private static void IsShowGameOverChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
                ((ScreenGame)d).AnimationGameOver();
            else
                ((ScreenGame)d).textGameOver.Visibility = Visibility.Collapsed;

        }
        #endregion

        private void ScrenGame_Loaded(object sender, RoutedEventArgs e)
        {
            textBlockPause.Margin = new Thickness(0, ConfigurationWPF.WindowClientHeight, 0, 0);

            animationText.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Visible, TimeSpan.Zero));

            easingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut };

            animationGameOver.EasingFunction = easingFunction;
            animationGameOver.Duration = durationGameOver;

            Binding bindingShowNewLevel = new Binding
            {
                Source = mainGrid.DataContext,
                Path = new PropertyPath("IsShowAnimationNewLevel"),
                Mode = BindingMode.TwoWay
            };
            this.SetBinding(ScreenGame.IsShowAnimationNewLevelProperty, bindingShowNewLevel);

            Binding bindingIsShowGameOver = new Binding
            {
                Source = mainGrid.DataContext,
                Path = new PropertyPath("IsShowGameOver"),
                Mode = BindingMode.TwoWay
            };
            this.SetBinding(ScreenGame.IsShowGameOverProperty, bindingIsShowGameOver);

            Binding bindingAnimationPause = new Binding
            {
                Source = mainGrid.DataContext,
                Path = new PropertyPath("IsPause"),
                Mode = BindingMode.TwoWay
            };
            this.SetBinding(ScreenGame.IsPauseProperty, bindingAnimationPause);

        }

        private void AnimationGameOver()
        {
            textGameOver.Visibility = Visibility.Visible;
            animationGameOver.From = new Thickness(0, this.ActualHeight, 0, 0);
            animationGameOver.To = new Thickness(0, this.ActualHeight / 2 - textGameOver.ActualHeight / 2, 0, 0);
            textGameOver.BeginAnimation(StackPanel.MarginProperty, animationGameOver);
        }

        private void StartAnimationNewLevel()
        {
            animationTop = new DoubleAnimation();
            animationBottom = new DoubleAnimation();

            animationTop.Completed += Animation_Completed;

            animationTop.Duration = durationClosOpenLevel;

            animationTop.EasingFunction = easingFunction;
            animationBottom.Duration = durationClosOpenLevel;
            animationBottom.EasingFunction = easingFunction;

            animationText.FillBehavior = FillBehavior.Stop;
            animationText.Duration = durationShowLevel;

            animationTop.From = 0.0;
            animationTop.To = ConfigurationWPF.WindowClientHeight / 2 + 4;

            animationBottom.From = 0.0;
            animationBottom.To = ConfigurationWPF.WindowClientHeight / 2 + 4;

            topRect.BeginAnimation(Rectangle.HeightProperty, animationTop);
            bottomRect.BeginAnimation(Rectangle.HeightProperty, animationBottom);
        }

        private void Animation_Completed(object sender, EventArgs e)
        {
            animationTop.Completed -= Animation_Completed;
            animationTop.Completed += AnimationTop_Completed;

            animationTop.From = this.ActualHeight / 2;
            animationTop.BeginTime = durationShowLevel;
            animationTop.To = 0.0;
            topRect.BeginAnimation(Rectangle.HeightProperty, animationTop);

            animationBottom.From = this.ActualHeight / 2;
            animationBottom.BeginTime = durationShowLevel;
            animationBottom.To = 0.0;
            bottomRect.BeginAnimation(Rectangle.HeightProperty, animationBottom);

            showNewLevelNumber.BeginAnimation(TextBlock.VisibilityProperty, animationText);
        }

        private void AnimationTop_Completed(object sender, EventArgs e)
        {
            animationTop.Completed -= AnimationTop_Completed;
            IsShowAnimationNewLevel = false;
        }
    }
}

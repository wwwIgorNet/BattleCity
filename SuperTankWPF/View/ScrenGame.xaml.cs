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
    public partial class ScrenGame : UserControl
    {
        private ScrenGameViewModel viewModel = ServiceLocator.Current.GetInstance<ScrenGameViewModel>();
        private ThicknessAnimation animationGameOver = new ThicknessAnimation();
        private DoubleAnimation animationTop = new DoubleAnimation();
        private DoubleAnimation animationBottom = new DoubleAnimation();
        private ObjectAnimationUsingKeyFrames animationText = new ObjectAnimationUsingKeyFrames();
        private TimeSpan durationShowLevel = TimeSpan.FromSeconds(ConfigurationWPF.DelayScrenLoadLevel.Seconds /  3);
        private TimeSpan durationAnim = TimeSpan.FromSeconds(ConfigurationWPF.DelayScrenLoadLevel.Seconds / 3);
        private TimeSpan durationGameOver = TimeSpan.FromMilliseconds(ConfigurationWPF.TimeGameOver);

        public ScrenGame()
        {
            InitializeComponent();

            this.Loaded += ScrenGame_Loaded;
            this.IsVisibleChanged += ScrenGame_IsVisibleChanged;
            Focusable = true;
        }

        private void ScrenGame_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue.Equals(true)) this.Focus();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.Key)
            {
                case Key.Up:
                case Key.Down:
                case Key.Left:
                case Key.Right:
                case Key.Space:
                    viewModel?.KeyDown(e.Key);
                    break;
            }
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
        public bool IsShowAnimationNewLevel
        {
            get { return (bool)GetValue(IsShowAnimationNewLevelProperty); }
            set { SetValue(IsShowAnimationNewLevelProperty, value); }
        }

        public static readonly DependencyProperty IsShowAnimationNewLevelProperty =
            DependencyProperty.Register("IsShowAnimationNewLevel", typeof(bool), typeof(ScrenGame), new PropertyMetadata(false, IsShowAnimationNewLevelChangedCallback));

        private static void IsShowAnimationNewLevelChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
                ((ScrenGame)d).StartAnimationNewLevel();
        }


        public bool IsShowGameOver
        {
            get { return (bool)GetValue(IsShowGameOverProperty); }
            set { SetValue(IsShowGameOverProperty, value); }
        }

        public static readonly DependencyProperty IsShowGameOverProperty =
            DependencyProperty.Register("IsShowGameOver", typeof(bool), typeof(ScrenGame), new PropertyMetadata(false, IsShowGameOverChangedCallback));

        private static void IsShowGameOverChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
                ((ScrenGame)d).AnimationGameOver();
            else
                ((ScrenGame)d).textGameOver.Visibility = Visibility.Collapsed;

        }
        #endregion

        private void ScrenGame_Loaded(object sender, RoutedEventArgs e)
        {
            animationTop.Duration = durationAnim;
            EasingFunctionBase easingFunction = new PowerEase();
            easingFunction.EasingMode = EasingMode.EaseInOut;
            animationTop.EasingFunction = easingFunction;

            animationBottom.Duration = durationAnim;
            animationBottom.EasingFunction = easingFunction;

            animationText.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Visible, durationAnim));
            animationText.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Collapsed, durationAnim + durationShowLevel));

            animationGameOver.EasingFunction = easingFunction;
            animationGameOver.Duration = durationGameOver;

            Binding bindingShowNewLevel = new Binding();
            bindingShowNewLevel.Source = mainGrid.DataContext;
            bindingShowNewLevel.Path = new PropertyPath("IsShowAnimationNewLevel");
            this.SetBinding(ScrenGame.IsShowAnimationNewLevelProperty, bindingShowNewLevel);

            Binding bindingIsShowGameOver = new Binding();
            bindingIsShowGameOver.Source = mainGrid.DataContext;
            bindingIsShowGameOver.Path = new PropertyPath("IsShowGameOver");
            this.SetBinding(ScrenGame.IsShowGameOverProperty, bindingIsShowGameOver);
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
            animationTop.Completed += Animation_Completed;
            animationTop.From = 0.0;
            animationTop.To = this.ActualHeight / 2 + 4;

            animationBottom.From = 0.0;
            animationBottom.To = this.ActualHeight / 2 + 4;

            topRect.BeginAnimation(Rectangle.HeightProperty, animationTop);
            bottomRect.BeginAnimation(Rectangle.HeightProperty, animationBottom);
            showNewLevelNumber.BeginAnimation(TextBlock.VisibilityProperty, animationText);
        }

        private void Animation_Completed(object sender, EventArgs e)
        {
            animationTop.Completed -= Animation_Completed;

            animationTop.From = this.ActualHeight / 2;
            animationTop.BeginTime = durationShowLevel;
            animationTop.To = 0.0;
            topRect.BeginAnimation(Rectangle.HeightProperty, animationTop);

            animationBottom.From = this.ActualHeight / 2;
            animationBottom.BeginTime = durationShowLevel;
            animationBottom.To = 0.0;
            bottomRect.BeginAnimation(Rectangle.HeightProperty, animationBottom);
        }
    }
}

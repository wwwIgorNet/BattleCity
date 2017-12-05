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

namespace SuperTankWPF.View
{
    /// <summary>
    /// Interaction logic for ScrenGame.xaml
    /// </summary>
    public partial class ScrenGame : UserControl
    {
        private ThicknessAnimation animationGameOver = new ThicknessAnimation();
        private DoubleAnimation animationTop = new DoubleAnimation();
        private DoubleAnimation animationBottom = new DoubleAnimation();
        private ObjectAnimationUsingKeyFrames animationText = new ObjectAnimationUsingKeyFrames();
        private TimeSpan durationShowLevel = TimeSpan.FromSeconds(2);
        private TimeSpan durationAnim = TimeSpan.FromSeconds(1);
        private TimeSpan durationGameOver = TimeSpan.FromSeconds(2);

        public ScrenGame()
        {
            InitializeComponent();

            this.Loaded += ScrenGame_Loaded;
        }

        private void ScrenGame_Loaded(object sender, RoutedEventArgs e)
        {
            Binding bindingLevel = new Binding();
            bindingLevel.Source = this.DataContext;
            bindingLevel.Path = new PropertyPath("Level");
            bindingLevel.StringFormat = "STAGE {0}";
            this.showNewLevelNumber.SetBinding(TextBlock.TextProperty, bindingLevel);

            animationTop.Duration = durationAnim;
            EasingFunctionBase easingFunction = new PowerEase();
            easingFunction.EasingMode = EasingMode.EaseOut;
            animationTop.EasingFunction = easingFunction;

            animationBottom.Duration = durationAnim;
            animationBottom.EasingFunction = easingFunction;

            animationText.Duration = durationShowLevel;
            animationText.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Visible, durationAnim));
            animationText.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Collapsed, durationShowLevel + durationAnim));

            animationGameOver.EasingFunction = easingFunction;
            animationGameOver.Duration = durationGameOver;

            AnimationGameOver();
            StartAnimation();
        }

        private void AnimationGameOver()
        {
            textGameOver.Visibility = Visibility.Visible;
            animationGameOver.From = new Thickness(0, this.ActualHeight, 0, 0);
            animationGameOver.To = new Thickness(0, this.ActualHeight / 2 - textGameOver.ActualHeight / 2, 0, 0);
            textGameOver.BeginAnimation(StackPanel.MarginProperty, animationGameOver);
        }

        private void StartAnimation()
        {
            animationTop.Completed += Animation_Completed;

            animationTop.From = 0.0;
            animationTop.To = this.ActualHeight / 2;

            animationBottom.From = 0.0;
            animationBottom.To = this.ActualHeight / 2;

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

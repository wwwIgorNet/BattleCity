using SuperTankWPF.Util;
using SuperTankWPF.ViewModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperTankWPF.View
{
    /// <summary>
    /// Interaction logic for StartScren.xaml
    /// </summary>
    public partial class ScreenStart : UserControl
    {
        private ThicknessAnimation animation = new ThicknessAnimation();

        public ScreenStart()
        {
            InitializeComponent();

            Loaded += StartScren_Loaded;
            IsVisibleChanged += StartScren_IsVisibleChanged;
        }

        private void StartScren_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible) AnimationLoadScren();
        }

        private void StartScren_Loaded(object sender, RoutedEventArgs e)
        {
            animation.Duration = new Duration(TimeSpan.FromSeconds(3));
            EasingFunctionBase easingFunction = new PowerEase();
            easingFunction.EasingMode = EasingMode.EaseOut;
            animation.EasingFunction = easingFunction;
            animation.Completed += Animation_Completed;
            
            AnimationLoadScren();
        }

        private void AnimationLoadScren()
        {
            if (!IsLoaded || !IsVisible) return;

            animation.From = new Thickness(0d, this.ActualHeight, 0d, 0d);
            animation.To = new Thickness(0d);
            mainBborder.BeginAnimation(Rectangle.MarginProperty, animation);

            if (IsValidIndex(menuList.SelectedIndex))
                (menuList.ItemContainerGenerator.ContainerFromIndex(menuList.SelectedIndex) as ListBoxItem).IsSelected = false;

            for (int i = 0; i < menuList.ItemContainerGenerator.Items.Count; i++)
                (menuList.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem).IsEnabled = false;
        }

        private void Animation_Completed(object sender, EventArgs e)
        {
            for (int i = 0; i < menuList.Items.Count; i++)
                (menuList.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem).IsEnabled = true;

            (menuList.ItemContainerGenerator.ContainerFromIndex(0) as ListBoxItem).Focus();
            (menuList.ItemContainerGenerator.ContainerFromIndex(0) as ListBoxItem).IsSelected = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (IsValidIndex(menuList.SelectedIndex))
                ((menuList.ItemContainerGenerator.ContainerFromIndex(menuList.SelectedIndex) as ListBoxItem).Tag as ICommand)?.Execute(null);
        }

        private bool IsValidIndex(int index)
        {
            return index >= 0 && menuList.Items.Count > index;
        }
    }
}

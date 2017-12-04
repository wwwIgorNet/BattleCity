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
    public partial class StartScren : UserControl
    {
        private DoubleAnimation animation = new DoubleAnimation();

        public StartScren()
        {
            InitializeComponent();

            Loaded += StartScren_Loaded;

            KeyDown += StartScren_KeyDown;
        }

        private void StartScren_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.R) BeginAnimationLoad();
        }

        private void StartScren_Loaded(object sender, RoutedEventArgs e)
        {
            animation.Duration = new Duration(TimeSpan.FromSeconds(4));
            SineEase easingFunction = new SineEase();
            easingFunction.EasingMode = EasingMode.EaseOut;
            animation.EasingFunction = easingFunction;
            animation.Completed += Animation_Completed;

            Binding bindimgIsAnimationLoadScren = new Binding();
            bindimgIsAnimationLoadScren.Source = this.DataContext;
            bindimgIsAnimationLoadScren.Path = new PropertyPath("IsAnimationLoadScren");
            bindimgIsAnimationLoadScren.Mode = BindingMode.TwoWay;
            this.SetBinding(StartScren.IsAnimationLoadScrenProperty, bindimgIsAnimationLoadScren);
        }

        #region DependencyProperty
        public bool IsAnimationLoadScren
        {
            get { return (bool)GetValue(IsAnimationLoadScrenProperty); }
            set { SetValue(IsAnimationLoadScrenProperty, value); }
        }

        public static readonly DependencyProperty IsAnimationLoadScrenProperty =
            DependencyProperty.Register(nameof(IsAnimationLoadScren), typeof(bool), typeof(StartScren), new PropertyMetadata(false, IsAnimationLoadScrenChangedCallback));

        private static void IsAnimationLoadScrenChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
                ((StartScren)d).BeginAnimationLoad();
        }
        #endregion

        private void BeginAnimationLoad()
        {
            if (!IsVisible) return;

            animation.From = this.ActualHeight;
            animation.To = 0.0;
            rectangle.BeginAnimation(Rectangle.HeightProperty, animation);

            if (IsValidIndex(menuList.SelectedIndex))
                (menuList.ItemContainerGenerator.ContainerFromIndex(menuList.SelectedIndex) as ListBoxItem).IsSelected = false;

            for (int i = 0; i < menuList.ItemContainerGenerator.Items.Count; i++)
                (menuList.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem).IsEnabled = false;

            IsAnimationLoadScren = true;
        }

        private void Animation_Completed(object sender, EventArgs e)
        {
            for (int i = 0; i < menuList.Items.Count; i++)
                (menuList.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem).IsEnabled = true;

            (menuList.ItemContainerGenerator.ContainerFromIndex(0) as ListBoxItem).Focus();
            (menuList.ItemContainerGenerator.ContainerFromIndex(0) as ListBoxItem).IsSelected = true;

            IsAnimationLoadScren = false;
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

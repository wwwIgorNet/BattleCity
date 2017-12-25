using SuperTankWPF.Model;
using SuperTankWPF.View;
using SuperTankWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private float volumeIncrement = ConfigurationWPF.VolumeIncrement;

        public MainWindow()
        {
            InitializeComponent();

            ClientWidth = ConfigurationWPF.WindowClientWidth;
            ClientHeight = ConfigurationWPF.WindowClientHeight;

            Binding binding = new Binding
            {
                Mode = BindingMode.OneWayToSource,
                Source = this.FindResource("Locator"),
                Path = new PropertyPath("SoundGame.Volume")
            };
            this.SetBinding(MainWindow.VolumeProperty, binding);
        }

        #region DependencyProperty
        public float Volume
        {
            get { return (float)GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }
        
        public static readonly DependencyProperty VolumeProperty =
            DependencyProperty.Register("Volume", typeof(float), typeof(MainWindow), new PropertyMetadata(ConfigurationWPF.VolumeDefoult));


        public double ClientHeight
        {
            get { return (double)GetValue(ClientHeightProperty); }
            set { SetValue(ClientHeightProperty, value); }
        }

        public static readonly DependencyProperty ClientHeightProperty =
            DependencyProperty.Register("ClientHeight", typeof(double), typeof(MainWindow), new PropertyMetadata(0.0, clientHeightChangedCallback));

        private static void clientHeightChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var horizontalBorderHeight = SystemParameters.ResizeFrameHorizontalBorderHeight;
            var captionHeight = SystemParameters.WindowCaptionHeight;

            ((MainWindow)d).Height = 2 * horizontalBorderHeight + captionHeight + (double)e.NewValue;
        }

        public double ClientWidth
        {
            get { return (double)GetValue(ClientWidthProperty); }
            set { SetValue(ClientWidthProperty, value); }
        }

        public static readonly DependencyProperty ClientWidthProperty =
            DependencyProperty.Register("ClientWidth", typeof(double), typeof(MainWindow), new PropertyMetadata(0.0, clientWidthChangedCallback));

        private static void clientWidthChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var verticalBorderWidth = SystemParameters.ResizeFrameVerticalBorderWidth;
            ((MainWindow)d).Width = 2 * verticalBorderWidth + (double)e.NewValue;
        }
        #endregion

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                Process.Start("IExplore.exe", @"file:///" + Directory.GetCurrentDirectory() + @"\Content\Help\Index.html");
            }
            else if (e.Key == Key.Escape)
            {
                this.Close();
            }
            else if (e.Key == Key.PageUp)
            {
                if (Volume <= 1 - volumeIncrement) Volume += volumeIncrement;
            }
            else if (e.Key == Key.PageDown)
            {
                if (Volume >= 0 + volumeIncrement) Volume -= volumeIncrement;
            }
        }
    }
}

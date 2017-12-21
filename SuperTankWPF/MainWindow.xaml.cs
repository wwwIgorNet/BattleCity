using SuperTankWPF.Model;
using SuperTankWPF.View;
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
        public MainWindow()
        {
            InitializeComponent();

            ClientWidth = ConfigurationWPF.WindowClientWidth;
            ClientHeight = ConfigurationWPF.WindowClientHeight;
        }


        #region DependencyProperty
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
            if (e.Key == Key.Enter)
            {

            }
            else if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}

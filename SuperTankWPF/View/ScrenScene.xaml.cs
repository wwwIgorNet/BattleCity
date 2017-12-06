using SuperTankWPF.Model;
using SuperTankWPF.Units;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    /// Interaction logic for ScrenScene.xaml
    /// </summary>
    public partial class ScrenScene : UserControl
    {
        public ScrenScene()
        {
            InitializeComponent();


            ((INotifyCollectionChanged)this.DataContext.GetType().GetProperty("Units").GetValue(this.DataContext)).CollectionChanged += ScrenScene_CollectionChanged;
        }

        private void ScrenScene_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                //foreach (UIElement element in e.NewItems)
                //    board.Children.Add(element);
                this.Dispatcher.BeginInvoke((Action)delegate () {
                    board.Children.Add(new ViewUnit() { Source = BitmapImages.BrickWall, X = 490, Y = 490 });
                    });
            }

            else if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (UIElement element in e.NewItems)
                    board.Children.Remove(element);
        }
    }
}

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SuperTankWPF.Model;
using SuperTankWPF.Units;
using SuperTankWPF.ViewModel;
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
        private IImageSourceStor imageSource = SimpleIoc.Default.GetInstance<IImageSourceStor>();
        private IFactoryUnitView factoryUnitView = SimpleIoc.Default.GetInstance<IFactoryUnitView>();

        public ScrenScene()
        {
            InitializeComponent();

            ServiceLocator.Current.GetInstance<ScrenSceneViewModel>().Units.CollectionChanged += ScrenScene_CollectionChanged;
        }

        private void ScrenScene_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                this.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    foreach (UnitViewModel unitViewModel in e.NewItems)
                    {
                        board.Children.Add(factoryUnitView.Create(unitViewModel));
                    }
                });
            }

            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                this.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    foreach (UnitViewModel unitViewModel in e.OldItems)
                    {
                        board.Children.Remove(unitViewModel.View);
                    }
                });
            }
        }
    }
}

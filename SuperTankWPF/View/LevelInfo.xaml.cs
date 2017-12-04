using SuperTankWPF.Model;
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

namespace SuperTankWPF.View
{
    /// <summary>
    /// Interaction logic for LevelInfo.xaml
    /// </summary>
    public partial class LevelInfo : UserControl
    {
        private BitmapImage bitmapImage = BitmapImages.InformationTank;

        public LevelInfo()
        {
            InitializeComponent();
        }

        private void RemoveTankkEnmy(int countTank)
        {
            uniformGrid.Children.RemoveRange(uniformGrid.Children.Count - countTank - 1, countTank);
        }

        private void AddTankEnemy(int countTank)
        {
            for (int i = 0; i < countTank; i++)
            {
                uniformGrid.Children.Add(new Image() { Source = bitmapImage });
            }
        }
    }
}

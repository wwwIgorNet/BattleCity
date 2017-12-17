using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuperTankWPF.View
{
    /// <summary>
    /// Interaction logic for DialogIP.xaml
    /// </summary>
    public partial class DialogIP : Window
    {
        public DialogIP()
        {
            InitializeComponent();
        }

        private void Button_Click_OK(object sender, RoutedEventArgs e)
        {
            buttonOK.Focus();
            iPRemoteComputer.Focus();
            //if (string.Empty == ((IDataErrorInfo)this.gridConteiner.DataContext)["IDataErrorInfo"])
            if (IPAddress.TryParse(this.iPRemoteComputer.Text, out IPAddress iPAddress))
                this.DialogResult = true;
        }
    }
}

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
        private string textIPRemouteComputer;

        public DialogIP()
        {
            InitializeComponent();

            newGame.Checked += NewGame_Checked;
            joinToGame.Checked += JoinToGame_Checked;
            NewGame_Checked(null, null);
        }

        private void JoinToGame_Checked(object sender, RoutedEventArgs e)
        {
            if (joinToGame.IsChecked == true)
            {
                iPRemoteComputer.Text = textIPRemouteComputer;
            }
        }

        private void NewGame_Checked(object sender, RoutedEventArgs e)
        {
            if (newGame.IsChecked == true)
            {
                textIPRemouteComputer = iPRemoteComputer.Text;
                iPRemoteComputer.Text = "";
            }
        }

        private void Button_Click_OK(object sender, RoutedEventArgs e)
        {
            if (newGame.IsChecked == true)
            {
                this.DialogResult = true;
            }
            else
            {
                buttonOK.Focus();
                iPRemoteComputer.Focus();
                IPAddress iPAddress;
                if (IPAddress.TryParse(this.iPRemoteComputer.Text, out iPAddress))
                    this.DialogResult = true;
            }
        }
    }
}

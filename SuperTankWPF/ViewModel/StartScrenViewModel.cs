using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SuperTankWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SuperTankWPF.ViewModel
{
    class StartScrenViewModel : ObservableObject
    {
        public StartScrenViewModel()
        {
            ListMenu = new ObservableCollection<MenuItem> {
                new MenuItem("I PLAYER", new RelayCommand(() => MessageBox.Show("Command I PLAYERS"))) { IsActiv = false },
                new MenuItem("II PLAYERS", new RelayCommand(() => MessageBox.Show("Command II PLAYERS"))),
                new MenuItem("CONSTRUCTION", new RelayCommand(() => MessageBox.Show("Command CONSTRUCTION")))
            };
        }

        public ObservableCollection<MenuItem> ListMenu { get; set; }
    }

    public class MenuItem : ObservableObject
    {
        private string text;
        private bool isActiv;
        private ICommand command;

        public MenuItem(string text, ICommand command)
        {
            this.Text = text;
            Command = command;
        }

        public ICommand Command
        {
            get { return command; }
            set { Set(ref command, value); }
        }

        public string Text
        {
            get { return text; }
            set { Set(ref text, value); }
        }

        public bool IsActiv
        {
            get { return isActiv; }
            set { Set(ref isActiv, value); }
        }

        public override string ToString()
        {
            return Text;
        }
    }
}

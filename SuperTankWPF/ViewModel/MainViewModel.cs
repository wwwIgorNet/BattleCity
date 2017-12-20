using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperTankWPF.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private enum Screen
        {
            Score,
            Game,
            Start,
            GameOver,
            Record,
            Construction,
            Lock
        }

        private Dictionary<Screen, Visibility> screenVisibility = new Dictionary<Screen, Visibility>();
        
        public MainViewModel()
        {
            foreach (Screen screen in Enum.GetValues(typeof(Screen)))
            {
                screenVisibility.Add(screen, Visibility.Collapsed);
            }

            screenVisibility[Screen.Start] = Visibility.Visible;
        }

        public Visibility ScrenScoreVisibility
        {
            get { return screenVisibility[Screen.Score]; }
            set { UpdateValue(nameof(ScrenScoreVisibility), Screen.Score, value); }
        }
        public Visibility ScrenGameVisibility
        {
            get { return screenVisibility[Screen.Game]; }
            set { UpdateValue(nameof(ScrenGameVisibility), Screen.Game, value); }
        }
        public Visibility StartScrenVisibility
        {
            get { return screenVisibility[Screen.Start]; }
            set { UpdateValue(nameof(StartScrenVisibility), Screen.Start, value); }
        }
        public Visibility ScrenGameOverVisibility
        {
            get { return screenVisibility[Screen.GameOver]; }
            set { UpdateValue(nameof(ScrenGameOverVisibility), Screen.GameOver, value); }
        }
        public Visibility ScrenRecordVisibility
        {
            get { return screenVisibility[Screen.Record]; }
            set { UpdateValue(nameof(ScrenRecordVisibility), Screen.Record, value); }
        }
        public Visibility ScrenConstructionVisibility
        {
            get { return screenVisibility[Screen.Construction]; }
            set { UpdateValue(nameof(ScrenConstructionVisibility), Screen.Construction, value); }
        }
        public Visibility ScreenLockVisibility
        {
            get { return screenVisibility[Screen.Lock]; }
            set { UpdateValue(nameof(ScreenLockVisibility), Screen.Lock, value); }
        }

        private void UpdateValue(string propertyName, Screen screen, Visibility newValue)
        {
            if (newValue == screenVisibility[screen]) return;

            ChangeVisibility(Visibility.Collapsed);
            screenVisibility[screen] = newValue;
            RaisePropertyChanged(propertyName);
        }

        public void ChangeVisibility(Visibility visibility)
        {
            foreach (Screen screen in Enum.GetValues(typeof(Screen)))
                if (screenVisibility[screen] != visibility)
                    screenVisibility[screen] = visibility;

            foreach (var prop in this.GetType().GetProperties())
                    this.RaisePropertyChanged(prop.Name);
        }
    }
}

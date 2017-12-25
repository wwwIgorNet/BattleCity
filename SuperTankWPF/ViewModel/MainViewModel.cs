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

        public Visibility ScreenScoreVisibility
        {
            get { return screenVisibility[Screen.Score]; }
            set { UpdateValue(nameof(ScreenScoreVisibility), Screen.Score, value); }
        }
        public Visibility ScreenGameVisibility
        {
            get { return screenVisibility[Screen.Game]; }
            set { UpdateValue(nameof(ScreenGameVisibility), Screen.Game, value); }
        }
        public Visibility ScreenStartVisibility
        {
            get { return screenVisibility[Screen.Start]; }
            set { UpdateValue(nameof(ScreenStartVisibility), Screen.Start, value); }
        }
        public Visibility ScreenGameOverVisibility
        {
            get { return screenVisibility[Screen.GameOver]; }
            set { UpdateValue(nameof(ScreenGameOverVisibility), Screen.GameOver, value); }
        }
        public Visibility ScreenRecordVisibility
        {
            get { return screenVisibility[Screen.Record]; }
            set { UpdateValue(nameof(ScreenRecordVisibility), Screen.Record, value); }
        }
        public Visibility ScreenConstructionVisibility
        {
            get { return screenVisibility[Screen.Construction]; }
            set { UpdateValue(nameof(ScreenConstructionVisibility), Screen.Construction, value); }
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

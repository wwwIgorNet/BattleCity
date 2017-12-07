using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperTankWPF.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private Visibility screnScoreVisibility;
        private Visibility screnGameVisibility;
        private Visibility startScrenVisibility;
        private Visibility screnGameOverVisibility;
        private Visibility screnRecordVisibility;

        public MainViewModel()
        {
            ScrenGameOverVisibility = Visibility.Collapsed;
            ScrenScoreVisibility = Visibility.Collapsed;
            ScrenGameVisibility = Visibility.Collapsed;
            StartScrenVisibility = Visibility.Visible;
            ScrenRecordVisibility = Visibility.Collapsed;
        }

        public Visibility ScrenScoreVisibility
        {
            get { return screnScoreVisibility; }
            set { Set(nameof(ScrenScoreVisibility), ref screnScoreVisibility, value); }
        }

        public Visibility ScrenGameVisibility
        {
            get { return screnGameVisibility; }
            set { Set(nameof(ScrenGameVisibility), ref screnGameVisibility, value); }
        }
        public Visibility StartScrenVisibility
        {
            get { return startScrenVisibility; }
            set { Set(nameof(StartScrenVisibility), ref startScrenVisibility, value); }
        }
        public Visibility ScrenGameOverVisibility
        {
            get { return screnGameOverVisibility; }
            set { Set(nameof(ScrenGameOverVisibility), ref screnGameOverVisibility, value); }
        }
        public Visibility ScrenRecordVisibility
        {
            get { return screnRecordVisibility; }
            set { Set(nameof(ScrenRecordVisibility), ref screnRecordVisibility, value); }
        }
    }
}

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
            ColapsAll();
        }

        public Visibility ScrenScoreVisibility
        {
            get { return screnScoreVisibility; }
            set { UpdateValue(nameof(ScrenScoreVisibility), ref screnScoreVisibility, value); }
        }

        public Visibility ScrenGameVisibility
        {
            get { return screnGameVisibility; }
            set { UpdateValue(nameof(ScrenGameVisibility), ref screnGameVisibility, value); }
        }
        public Visibility StartScrenVisibility
        {
            get { return startScrenVisibility; }
            set { UpdateValue(nameof(StartScrenVisibility), ref startScrenVisibility, value); }
        }
        public Visibility ScrenGameOverVisibility
        {
            get { return screnGameOverVisibility; }
            set { UpdateValue(nameof(ScrenGameOverVisibility), ref screnGameOverVisibility, value); }
        }
        public Visibility ScrenRecordVisibility
        {
            get { return screnRecordVisibility; }
            set { UpdateValue(nameof(ScrenRecordVisibility), ref screnRecordVisibility, value); }
        }

        private void UpdateValue(string propertyName, ref Visibility field, Visibility newValue)
        {
            ColapsAll();
            Set(propertyName, ref field, newValue);
        }

        public void ColapsAll()
        {
            Visibility visibility = Visibility.Collapsed;

            Set(nameof(ScrenScoreVisibility), ref screnScoreVisibility, visibility);
            Set(nameof(ScrenGameVisibility), ref screnGameVisibility, visibility);
            Set(nameof(StartScrenVisibility), ref startScrenVisibility, visibility);
            Set(nameof(ScrenGameOverVisibility), ref screnGameOverVisibility, visibility);
            Set(nameof(ScrenRecordVisibility), ref screnRecordVisibility, visibility);
        }
    }
}

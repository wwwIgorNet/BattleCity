using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperTankWPF.ViewModel
{
    class ScreenLockViewModel : ObservableObject
    {
        private Visibility isCancelVisible = Visibility.Collapsed;

        public Visibility IsCancelVisible
        {
            get { return isCancelVisible; }
            set { Set(nameof(IsCancelVisible), ref isCancelVisible, value); }
        }

        public event Action Cancel;

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand(() => Cancel?.Invoke()));
            }
        }

        public void Clear()
        {
            Cancel = null;
            IsCancelVisible = Visibility.Collapsed;
        }
    }
}

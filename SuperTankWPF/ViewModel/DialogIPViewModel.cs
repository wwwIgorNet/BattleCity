using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SuperTankWPF.ViewModel
{
    class DialogIPViewModel : ObservableObject, IDataErrorInfo
    {
        private IPAddress iPCurrentComputer;
        private IPAddress iPRemoteComputer;

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                if (columnName == "IPRemoteComputer")
                {
                    if (null == IPRemoteComputer)
                    {
                        error = "Неверный формат IP";
                    }
                }
                return error;
            }
        }
        public IPAddress IPCurrentComputer
        {
            get { return iPCurrentComputer; }
            private set { Set(nameof(IPCurrentComputer), ref iPCurrentComputer, value); }
        }
        public IPAddress IPRemoteComputer
        {
            get { return iPRemoteComputer; }
            set { Set(nameof(IPRemoteComputer), ref iPRemoteComputer, value); }
        }

        public string Error => "";

        public void Init()
        {
            // Получение ip-адреса.
            String host = System.Net.Dns.GetHostName();
            IPCurrentComputer = Dns.GetHostByName(host).AddressList[0];
            IPRemoteComputer = IPCurrentComputer;
        }
    }
}

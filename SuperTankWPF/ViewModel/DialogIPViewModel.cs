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
        private bool newGame;
        private bool joinGame;

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
        public bool NewGame
        {
            get { return newGame; }
            set { Set(nameof(NewGame), ref newGame, value); }
        }
        public bool JoinGame
        {
            get { return joinGame; }
            set { Set(nameof(JoinGame), ref joinGame, value); }
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
#pragma warning disable CS0618 // Type or member is obsolete
            IPCurrentComputer = Dns.GetHostByName(host).AddressList[0];
#pragma warning restore CS0618 // Type or member is obsolete
            IPRemoteComputer = IPCurrentComputer;
            NewGame = true;
        }

        internal void Clerar()
        {
            JoinGame = false;
            IPCurrentComputer = null;
            IPRemoteComputer = null;
        }
    }
}

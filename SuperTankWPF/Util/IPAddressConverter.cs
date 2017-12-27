using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SuperTankWPF.Util
{
    class IPAddressConverter : IValueConverter
    {
        private string prevIp = string.Empty;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return ((IPAddress)value).ToString();
            else
                return prevIp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            prevIp = (string)value;
            IPAddress iPAddress;
            IPAddress.TryParse((string)value, out iPAddress);
            return iPAddress;
        }
    }
}

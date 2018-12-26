using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
    class PrientTimesboolConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int count =System.Convert.ToInt32(MyHelper.ConfigurationHelper.GetConfig(ConfigItemName.defaultPrintFrequency.ToString()));
            if (((int)value) <count) { return true; } else { return false; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

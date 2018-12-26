using System;
using System.Globalization;

namespace ManageCenter
{
    class PrientTimesNowConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return MyHelper.DateTimeHelper.getCurrentDateTime();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

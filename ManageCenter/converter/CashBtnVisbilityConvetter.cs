using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ManageCenter
{
    class CashBtnVisbilityConvetter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          WeighingBill bill= (WeighingBill)value;
            if (bill == null)
            {
                return Visibility.Collapsed;
            }
            else
            {
                if (bill.isReceiveMoney == 0 && bill.overtopMoney > 0) {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }              
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}

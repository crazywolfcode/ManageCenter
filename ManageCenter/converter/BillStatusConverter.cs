using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
    class BillStatusConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WeighingBill bill = (WeighingBill)value;
            String result = String.Empty;         
            if (bill.isChecked == 1)
            {
                result = "已审核";
            }
            else {
                result = "未审核";
            }
            if (bill.isReceiveMoney == 2)
            {
                result = result + "  不需补税";
            }else  if (bill.isReceiveMoney == 1)
            {
                result = result + "  已补税";
            }
            else
            {
                result = result + "  未补税";
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
    public class BillIamgeModel
    {
        public static List<BillImage> GetListByBillNumber(String number)
        {
            string condition = BillImageColumns.bill_number.ToString()+" = '"+number+"'" ;         
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.bill_image.ToString(), null, condition);
            List<BillImage> list = DatabaseOPtionHelper.GetInstance().select<BillImage>(sql);
            return list;
        }
        
    }
}

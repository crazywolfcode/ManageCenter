using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
   public class BillTaxationMoneyRecordModel
    {

        public static List<BillTaxationMoneyRecord> searchData(string condition)
        {
            List<BillTaxationMoneyRecord> list = new List<BillTaxationMoneyRecord>();
            String order = BillTaxationMoneyRecordColumuns.add_time.ToString() + " desc";
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.bill_taxation_money_record.ToString(), null, condition, null, null, order);
            list = DatabaseOPtionHelper.GetInstance().select<BillTaxationMoneyRecord>(sql);
            return list;
        }
    }
}

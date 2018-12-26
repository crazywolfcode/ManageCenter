using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
   public class WeighingBillModel
    {
        public static List<WeighingBill> GetTodayData() {
            List<WeighingBill> list = new List<WeighingBill>();
            String date = DateTime.Now.ToString("yyyy-MM-dd");
            String condition = WeighingBillColumns.add_time.ToString() +" like '"+date+"%'";
            String order = WeighingBillColumns.add_time.ToString() + " asc";
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.weighing_bill.ToString(), null, condition, null, null, order);
            list = DatabaseOPtionHelper.GetInstance().select<WeighingBill>(sql);
            return list;
        }

        public static List<WeighingBill> searchData(string condition)
        {
            List<WeighingBill> list = new List<WeighingBill>();          
            String order = WeighingBillColumns.add_time.ToString() + " asc";
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.weighing_bill.ToString(), null, condition, null, null, order);
            list = DatabaseOPtionHelper.GetInstance().select<WeighingBill>(sql);
            return list;
        }

        public static int Create(WeighingBill mWeighingBill)
        {
            return DatabaseOPtionHelper.GetInstance().insert(mWeighingBill);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
    public class CarInfoModel
    {
        public static List<CarInfo> FuzzySearch(String palteNumberPart)
        {
            string condition = null;
            if (!string.IsNullOrEmpty(palteNumberPart)) {
                 condition = CarInfoColumns.car_number.ToString() + " like '%" + palteNumberPart + "%' ";
            } 
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.car_info.ToString(), null, condition);
            List<CarInfo> list = DatabaseOPtionHelper.GetInstance().select<CarInfo>(sql);
            return list;
        }

        public static bool Existence(String carnumber)
        {           
                string condition = CarInfoColumns.car_number.ToString() + " = '" + carnumber + "' ";
                String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.car_info.ToString(), null, condition,null,null,null,1);
                List<CarInfo> list = DatabaseOPtionHelper.GetInstance().select<CarInfo>(sql);
                return list.Count > 0;           
        }

        public static bool ExistenceAndCreate(CarInfo carInfo)
        {
            if (!Existence(carInfo.carNumber))
            {
              return  DatabaseOPtionHelper.GetInstance().insert(carInfo) > 0;
            }
            else {
                return true;
            }
        }

        public static int Create(CarInfo carInfo)
        {
            return DatabaseOPtionHelper.GetInstance().insert(carInfo) ;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
    class BaseDataModel
    {    
        public static String GetSql(String tableName, string time,int limit = 10)
        {
            string condition = PublicColumns.add_time.ToString() + "> '" +time+ "' OR " + PublicColumns.last_update_time.ToString() + " > '" + time + "'";
            string order = PublicColumns.add_time.ToString() + " asc ," + PublicColumns.last_update_time.ToString() + " asc";           
            string sql = DatabaseOPtionHelper.GetInstance().getSelectSqlNoSoftDeleteCondition(tableName, null, condition, null, null, order, limit);
            return sql;
        }
    }
}

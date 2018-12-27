using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
  public  class RolesModel
    {
        public static List<Roles> GetList()
        {
            List<Roles> list = new List<Roles>();
            String order = RolesColumns.role_level.ToString();
            String condition = RolesColumns.role_level.ToString() + " < " + (int)RoleLevelType.XTZZ ;
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.roles.ToString(), null, condition, null, null, order, 3);
            list = DatabaseOPtionHelper.GetInstance().select<Roles>(sql);
            return list;
        }
    }
}

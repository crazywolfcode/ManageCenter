using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
    class UserModel
    {
        public static User Login(String phone,String pwd)
        {
            string condition = UserColumns.phone.ToString() + " = '"+phone+"' and "+UserColumns.pwd.ToString()+" ='"+pwd+"'";
            
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.user.ToString(), null, condition,null,null,null,1);
            List<User> list = DatabaseOPtionHelper.GetInstance().select<User>(sql);
            if (list.Count > 0) {
                return list[0];
            }
            return null;
        }
    }
}

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

        

        public static List<User> GetByRoleLevel(int level)
        {
            List<User> list = new List<User>();
            String order =UserColumns.name.ToString();
            String condition = UserColumns.role_level.ToString() + "  ="+level;
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.user.ToString(), null, condition, null, null, order);
            list = DatabaseOPtionHelper.GetInstance().select<User>(sql);
            return list;
        }

        internal static bool CheckUserByPhone(string phone)
        {
            List<User> list = new List<User>();           
            String condition = UserColumns.phone.ToString() + "  =" + phone;
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.user.ToString(), null, condition, null, null, null,1);
            list = DatabaseOPtionHelper.GetInstance().select<User>(sql);
            return list.Count>0;
        }
    }
}

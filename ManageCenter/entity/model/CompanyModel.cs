using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
    public class CompanyModel
    {
        public static List<Company> IndistinctSearchByNameOrNameFirstCase(String nameOrCase)
        {
            string condition = null;
            if (!String.IsNullOrEmpty(nameOrCase))
            {
                condition = CompanyColumns.name.ToString() + " like '%" + nameOrCase + "%' " + " OR " + CompanyColumns.name_first_case.ToString() + " like '%" + nameOrCase.ToUpper() + "%'";
            }
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.company.ToString(), null, condition);
            List<Company> list = DatabaseOPtionHelper.GetInstance().select<Company>(sql);
            return list;
        }

        public static Company GetByName(string sendCompany)
        {
            string condition = CompanyColumns.name.ToString() + " = '" + sendCompany + "' ";
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.company.ToString(), null, condition, null, null, null, 1);
            List<Company> list = DatabaseOPtionHelper.GetInstance().select<Company>(sql);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public static int Create(Company company)
        {

            return DatabaseOPtionHelper.GetInstance().insert(company);
        }
    }
}

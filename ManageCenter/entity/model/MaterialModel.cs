using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
    public class MaterialModel
    {
        public static List<Material> IndistinctSearchByNameOrNameFirstCase(String nameOrCase)
        {
            string condition = null;
            if (!String.IsNullOrEmpty(nameOrCase))
            {
                condition = MaterialColumns.name.ToString() + " like '%" + nameOrCase + "%' " + " OR " + MaterialColumns.name_first_case.ToString() + " like '%" + nameOrCase.ToUpper() + "%'";
            }
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.material.ToString(), null, condition);
            List<Material> list = DatabaseOPtionHelper.GetInstance().select<Material>(sql);
            return list;
        }

        public static Material GetByName(string materialName)
        {
            string condition = MaterialColumns.name.ToString() + " = '" + materialName + "' ";
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.material.ToString(), null, condition, null, null, null, 1);
            List<Material> list = DatabaseOPtionHelper.GetInstance().select<Material>(sql);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        public static int Create(Material material)
        {
            return DatabaseOPtionHelper.GetInstance().insert(material);
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
   public class MaterialTaxationRecordModel
    {
        public static List<MaterialTaxationRecod> GetList(String materialId)
        {
            List<MaterialTaxationRecod> list = new List<MaterialTaxationRecod>();
            String order = MaterialTaxationRecodColumns.add_time.ToString();
            String condition = MaterialTaxationRecodColumns.material_id.ToString() + " = '" +materialId+"'";
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.material_taxation_recod.ToString(), null, condition, null, null, order, 3);
            list = DatabaseOPtionHelper.GetInstance().select<MaterialTaxationRecod>(sql);
            return list;
        }

    }
}

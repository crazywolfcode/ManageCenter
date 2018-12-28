using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
    public class StationModel
    {
        public static Station SelectById(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return null;
            }
            else
            {
                string condition = StationColumns.id.ToString() + " = '" + id + "' and status = 1";
                String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.station.ToString(), null, condition, null, null, null, 1);
                List<Station> list = DatabaseOPtionHelper.GetInstance().select<Station>(sql);
                if (list.Count > 0)
                {
                    return list[0];
                }
                return null;
            }
        }

        public static List<Station> GetList()
        {
            List<Station> list = new List<Station>();
            string condition = StationColumns.status.ToString() + " = 1";
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.station.ToString(), null, condition);
            list = DatabaseOPtionHelper.GetInstance().select<Station>(sql);
            return list;
        }

        internal static Station GetByName(string name)
        {
            List<Station> list = new List<Station>();
            string condition = StationColumns.name.ToString() + $"=  '{name}'";
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.station.ToString(), null, condition,null,null,null,1);
            list = DatabaseOPtionHelper.GetInstance().select<Station>(sql);
            if (list.Count > 0) {
                return list[0];
            }
            return null;
        }
    }
}

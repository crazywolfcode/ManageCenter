using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
    public class ConfigModel
    {
        public static List<Config> GetCurrStationConfigs()
        {
            string condition = ConfigColumns.station_id.ToString()+" = '"+App.mStation.id+"'" ;         
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.config.ToString(), null, condition);
            List<Config> list = DatabaseOPtionHelper.GetInstance().select<Config>(sql);
            return list;
        }
        
    }
}

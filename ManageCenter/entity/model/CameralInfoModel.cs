using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
    public class CameralInfoModel
    {
        public List<CameraInfo> GetList(String stationId)
        {
            List<CameraInfo> list = new List<CameraInfo>();
            String order = CameraInfoColumuns.position.ToString() + " asc";
            String condition = CameraInfoColumuns.station_id.ToString() + " = '" + stationId + "'";
            String sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.camera_info.ToString(), null, condition, null, null, order, 3);
            list = DatabaseOPtionHelper.GetInstance().select<CameraInfo>(sql);
            return list;
        }
    }
}

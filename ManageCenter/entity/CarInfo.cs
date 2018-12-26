using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ManageCenter
{

    /// <summary>
    /// 车辆信息
    /// 数据条数:6
    /// 数据大小:16KB
    /// </summary>


    public class CarInfo
    {

        /// <summary>
        /// 可空:NO
        /// </summary>

        public String id { get; set; }

        /// <summary>
        /// 可空:NO
        /// </summary>

        public String carNumber { get; set; }
        /// <summary>
        /// IC卡的编号
        /// </summary>
        public String icNumber { get; set; }
        /// <summary>
        /// 注释:所属省份
        /// 可空:YES
        /// </summary>
        public String affiliatedProvince { get; set; }
        /// <summary>
        /// 可空:NO
        /// </summary>

        public String driver { get; set; }

        /// <summary>
        /// 可空:NO
        /// </summary>

        public String driverMobile { get; set; }

        /// <summary>
        /// 注释:驾驶员身份证号码
        /// 可空:NO
        /// </summary>

        public String driverIdnumber { get; set; }

        /// <summary>
        /// 可空:YES
        /// </summary>

        public String ownerId { get; set; }

        /// <summary>
        /// 可空:YES
        /// </summary>

        public String ownerName { get; set; }

        /// <summary>
        /// 注释:备注信息
        /// 可空:YES
        /// </summary>

        public String remark { get; set; }

        /// <summary>
        /// 注释:添加时间
        /// 可空:NO
        ///默认值:CURRENT_TIMESTAMP
        /// </summary>

        public DateTime addTime { get; set; }

        /// <summary>
        /// 注释:0 未启用 1 正常启用
        /// 可空:NO
        ///默认值:1
        /// </summary>

        public Int32 status { get; set; }

        /// <summary>
        /// 注释:添加人信息
        /// 可空:YES
        /// </summary>

        public String addUserId { get; set; }

        /// <summary>
        /// 注释:添加人信息
        /// 可空:YES
        /// </summary>

        public String addUserName { get; set; }

        /// <summary>
        /// 注释:最后变动时间
        /// 可空:YES
        /// </summary>

        public DateTime lastUpdateTime { get; set; }

        /// <summary>
        /// 注释:是否删除 0 否 1 是 
        /// 可空:NO
        ///默认值:0
        /// </summary>

        public Int32 isDelete { get; set; }

        /// <summary>
        /// 注释:删除时间
        /// 可空:YES
        /// </summary>

        public DateTime deleteTime { get; set; }

        /// <summary>
        /// 注释:最后修改人员Id
        /// 可空:YES
        /// </summary>

        public String lastUpdateUserId { get; set; }

        /// <summary>
        /// 注释:最后修改人姓名
        /// 可空:YES
        /// </summary>

        public String lastUpdateUserName { get; set; }
        /// <summary>
        /// 皮重
        /// </summary>
        public double traeWeight { get; set; }
    }

}

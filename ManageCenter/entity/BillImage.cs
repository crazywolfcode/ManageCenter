using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ManageCenter
{

    /// <summary>
    /// 过磅时系统自动截取图片
    /// 数据条数:1
    /// 数据大小:16KB
    /// </summary>


    public class BillImage
    {

        /// <summary>
        /// 可空:NO
        /// </summary>

        public String id { get; set; }

        /// <summary>
        /// 注释:1 前 2 后 3上
        /// 可空:NO
        ///默认值:1
        /// </summary>

        public Int32 positon { get; set; }

        /// <summary>
        /// 可空:YES
        /// </summary>

        public String address { get; set; }

        /// <summary>
        /// 服务器图片地址
        /// 可空:YES
        /// </summary>
        public String remoteAddress { get; set; }
        /// <summary>
        /// 可空:NO
        /// </summary>

        public String billId { get; set; }

        /// <summary>
        /// 可空:NO
        /// </summary>

        public String billNumber { get; set; }

        /// <summary>
        /// 注释:1 入场 2出场
        /// 可空:NO
        ///默认值:1
        /// </summary>

        public Int32 type { get; set; }

        /// <summary>
        /// 注释:0 未上传 1 已经上传
        /// 可空:NO
        ///默认值:0
        /// </summary>

        public Int32 isUp { get; set; }

        /// <summary>
        /// 注释:上传时间
        /// 可空:YES
        /// </summary>

        public DateTime upDatetime { get; set; }

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
        /// 可空:YES
        /// </summary>

        public String stationId { get; set; }

        /// <summary>
        /// 注释:所属验票站点的名称
        /// 可空:YES
        /// </summary>

        public String stationName { get; set; }

    }
}

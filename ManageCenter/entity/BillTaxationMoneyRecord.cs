using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ManageCenter
{

    /// <summary>
    /// 数据条数:0
    /// 数据大小:16KB
    /// </summary>


    public class BillTaxationMoneyRecord
    {

        /// <summary>
        /// 可空:NO
        /// </summary>

        public String id { get; set; }

        /// <summary>
        /// 注释:金额 
        /// 可空:NO
        ///默认值:0.000
        /// </summary>

        public Double money { get; set; }

        /// <summary>
        /// 注释:0现金 1微信 2 支付定 3刷卡 4其它
        /// 可空:NO
        ///默认值:0
        /// </summary>

        public Int32 receiveType { get; set; }

        /// <summary>
        /// 注释:编号
        /// 可空:YES
        /// </summary>

        public String number { get; set; }

        /// <summary>
        /// 可空:YES
        /// </summary>

        public String stationId { get; set; }

        /// <summary>
        /// 注释:所属验票站点的名称
        /// 可空:YES
        /// </summary>

        public String stationName { get; set; }

        /// <summary>
        /// 可空:YES
        /// </summary>

        public String sendCompany { get; set; }

        /// <summary>
        /// 可空:YES
        /// </summary>

        public String receiveCompany { get; set; }

        /// <summary>
        /// 可空:YES
        /// </summary>

        public String materialId { get; set; }

        /// <summary>
        /// 注释:物质名称
        /// 可空:YES
        /// </summary>

        public String materialName { get; set; }

        /// <summary>
        /// 注释:车牌号
        /// 可空:YES
        /// </summary>

        public String carNumber { get; set; }

        /// <summary>
        /// 注释:驾驶员
        /// 可空:YES
        /// </summary>

        public String driver { get; set; }

        /// <summary>
        /// 注释:超限吨位
        /// 可空:NO
        ///默认值:0.000
        /// </summary>

        public Double overtopWeight { get; set; }

        /// <summary>
        /// 注释:税费单价
        /// 可空:NO
        ///默认值:0
        /// </summary>

        public Double materialTaxation { get; set; }

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

    }
}

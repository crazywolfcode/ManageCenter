using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ManageCenter
{

    /// <summary>
    /// 过磅单
    /// 数据条数:0
    /// 数据大小:16KB
    /// </summary>


    public class WeighingBill
    {

        /// <summary>
        /// 可空:NO
        /// </summary>

        public String id { get; set; }

        /// <summary>
        /// 可空:YES
        /// </summary>

        public String stationId { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public String number { get; set; }
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
        /// 注释:发货公司首拼字母
        /// 可空:YES
        /// </summary>

        public String sendCompanyCase { get; set; }

        /// <summary>
        /// 可空:YES
        /// </summary>

        public String receiveCompany { get; set; }

        /// <summary>
        /// 注释:物质分类
        /// 可空:YES
        /// </summary>

        public String materialCateId { get; set; }

        /// <summary>
        /// 注释:收货公司首拼字母
        /// 可空:YES
        /// </summary>

        public String receiveCompanyCase { get; set; }

        /// <summary>
        /// 注释:物质分类名称
        /// 可空:YES
        /// </summary>

        public String materialCateName { get; set; }

        /// <summary>
        /// 注释:物质分类首拼字母
        /// 可空:YES
        /// </summary>

        public String materialCateCase { get; set; }

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
        /// 注释:物质名称首拼字母
        /// 可空:YES
        /// </summary>

        public String materialFirstCase { get; set; }

        /// <summary>
        /// 注释:税费单价
        /// 可空:NO
        ///默认值
        /// </summary>

        public Double materialTaxation { get; set; }

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
        /// 注释:驾驶员身份证号码
        /// 可空:YES
        /// </summary>

        public String driverIdUmber { get; set; }

        /// <summary>
        /// 注释:驾驶员电话
        /// 可空:YES
        /// </summary>

        public String phone { get; set; }

        /// <summary>
        /// 注释:发货毛重
        /// 可空:NO
        ///默认值:0.000
        /// </summary>

        public Double sendGrossWeight { get; set; }

        /// <summary>
        /// 注释:发货皮重
        /// 可空:NO
        ///默认值:0.000
        /// </summary>

        public Double sendTraeWeight { get; set; }

        /// <summary>
        /// 注释:发货争重
        /// 可空:NO
        ///默认值:0.000
        /// </summary>

        public Double sendNetWeight { get; set; }

        public Double grossWeight { get; set; }

        public Double netWeight { get; set; }
        /// <summary>
        /// 注释:车辆原始皮重
        /// 可空:NO
        ///默认值:0.000
        /// </summary>

        public Double carTraeWeight { get; set; }

        /// <summary>
        /// 注释:限定吨位
        /// 可空:NO
        ///默认值:0.000
        /// </summary>

        public Double limitWeight { get; set; }

        /// <summary>
        /// 注释:超限吨位
        /// 可空:NO
        ///默认值:0.000
        /// </summary>

        public Double overtopWeight { get; set; }

        /// <summary>
        /// 可空:NO
        ///默认值:0.000
        /// </summary>

        public Double overtopMoney { get; set; }

        /// <summary>
        /// 注释:是否收取超限税费 0 未收 1 已收 2不需要收钱
        /// 可空:NO
        ///默认值:0
        /// </summary>

        public Int32 isReceiveMoney { get; set; }

        /// <summary>
        /// 可空:YES
        /// </summary>

        public String operatorId { get; set; }

        /// <summary>
        /// 注释:过磅员，验票员
        /// 可空:YES
        /// </summary>

        public String operatorName { get; set; }

        /// <summary>
        /// 可空:YES
        /// </summary>

        public String checkUserId { get; set; }

        /// <summary>
        /// 注释:审核员名称
        /// 可空:YES
        /// </summary>

        public String checkUserName { get; set; }

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
        /// 注释:是否审核 0 否 1 是 
        /// 可空:NO
        ///默认值:0
        /// </summary>

        public Int32 isChecked { get; set; }

        /// <summary>
        /// 注释:删除时间
        /// 可空:YES
        /// </summary>

        public DateTime checkedTime { get; set; }

        public UInt32 printFrequency { get; set; }
        public DateTime printTime { get; set; }
        /// <summary>
        /// 总税款
        /// </summary>
        public double totalTaxstionMoney { get; set; }                
    }
}

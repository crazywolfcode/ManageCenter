using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ManageCenter
{

    /// <summary>
    /// 数据同步信息表
    /// 数据条数:0
    /// 数据大小:16KB
    /// </summary>


    public class TableSync
    {

        /// <summary>
        /// 可空:NO
        /// </summary>

        public Int32 id { get; set; }

        /// <summary>
        /// 注释:表名称
        /// 可空:NO
        /// </summary>

        public String tableName { get; set; }

        /// <summary>
        /// 注释:上一次上传同步时间
        /// 可空:YES
        /// </summary>
        public DateTime syncUpTime { get; set; }

        /// <summary>
        /// 注释:上传影响记录数
        /// 可空:NO
        ///默认值:0
        /// </summary>

        public Int32 syncUpCount { get; set; }

        /// <summary>
        /// 注释:上一次下载同步时间
        /// 可空:YES
        /// </summary>

        public DateTime syncDownTime { get; set; }

        /// <summary>
        /// 注释:下载影响记录数
        /// 可空:NO
        ///默认值:0
        /// </summary>

        public Int32 syncDownCount { get; set; }

        /// <summary>
        /// 注释:是否有上传状态 1 是 0否
        /// 可空:NO
        ///默认值:0
        /// </summary>

        public Int32 hasUpStatus { get; set; }

        /// <summary>
        /// 注释:0 不需要同步 1要同步 
        /// 可空:NO
        ///默认值:1
        /// </summary>

        public Int32 noSync { get; set; }

        /// <summary>
        /// 注释:是否还有更多数据 1是 0否
        /// 可空:NO
        ///默认值:0
        /// </summary>

        public Int32 hasMore { get; set; }

        /// <summary>
        /// 注释:限制条数
        /// 可空:YES
        ///默认值:1
        /// </summary>

        public Int32 limitCount { get; set; }

        /// <summary>
        /// 注释:是否为公共数据 1 是 0否
        /// 可空:NO
        ///默认值:1
        /// </summary>

        public Int32 isCommom { get; set; }

    }
}

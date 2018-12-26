using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

 namespace ManageCenter
{

	 /// <summary>
	 /// 公司信息
	 /// 数据条数:0
	 /// 数据大小:16KB
	 /// </summary>


	  public  class Company
	 {

	 /// <summary>
	 /// 可空:NO
	 /// </summary>

	 public String id{ get; set; }

	 /// <summary>
	 /// 可空:NO
	 /// </summary>

	 public String name{ get; set; }

	 /// <summary>
	 /// 注释:法人
	 /// 可空:YES
	 /// </summary>

	 public String legalPerson{ get; set; }

	 /// <summary>
	 /// 注释:首拼字母
	 /// 可空:YES
	 /// </summary>

	 public String nameFirstCase{ get; set; }

	 /// <summary>
	 /// 注释:社会信用代码
	 /// 可空:YES
	 /// </summary>

	 public String creditNumber{ get; set; }

	 /// <summary>
	 /// 可空:YES
	 /// </summary>

	 public String busineseLincenseNumber{ get; set; }

	 /// <summary>
	 /// 注释:有效果日期
	 /// 可空:YES
	 /// </summary>

	 public String liceseEspriseTime{ get; set; }

	 /// <summary>
	 /// 可空:YES
	 /// </summary>

	 public String abbreviation{ get; set; }

	 /// <summary>
	 /// 可空:YES
	 /// </summary>

	 public String abbreviationFirstCase{ get; set; }

	 /// <summary>
	 /// 可空:NO
	 ///默认值:1
	 /// </summary>

	 public Int32 level{ get; set; }

	 /// <summary>
	 /// 可空:NO
	 ///默认值:0
	 /// </summary>

	 public String parentId{ get; set; }

	 /// <summary>
	 /// 注释:0 未知 1私营 2 国营 3政府
	 /// 可空:YES
	 ///默认值:1
	 /// </summary>

	 public Int32 type{ get; set; }

	 /// <summary>
	 /// 注释:办公地址
	 /// 可空:YES
	 /// </summary>

	 public String address{ get; set; }

	 /// <summary>
	 /// 注释:0  未使用系统 1 使用系统
	 /// 可空:NO
	 ///默认值:0
	 /// </summary>

	 public Int32 isUseSystem{ get; set; }

	 /// <summary>
	 /// 注释:0 别人添加 1 自已注册
	 /// 可空:NO
	 ///默认值:1
	 /// </summary>

	 public Int32 regesterType{ get; set; }

	 /// <summary>
	 /// 注释:0 个人 1 公司
	 /// 可空:NO
	 ///默认值:1
	 /// </summary>

	 public Int32 customerType{ get; set; }

	 /// <summary>
	 /// 注释:所属省份
	 /// 可空:YES
	 ///默认值:0
	 /// </summary>

	 public Int32 affiliatedProvinceId{ get; set; }

	 /// <summary>
	 /// 注释:所属省份
	 /// 可空:YES
	 /// </summary>

	 public String affiliatedProvince{ get; set; }

	 /// <summary>
	 /// 注释:备注信息
	 /// 可空:YES
	 /// </summary>

	 public String remark{ get; set; }

	 /// <summary>
	 /// 注释:添加时间
	 /// 可空:NO
	 ///默认值:CURRENT_TIMESTAMP
	 /// </summary>

	 public DateTime addTime{ get; set; }

	 /// <summary>
	 /// 注释:0 未启用 1 正常启用
	 /// 可空:NO
	 ///默认值:1
	 /// </summary>

	 public Int32 status{ get; set; }

	 /// <summary>
	 /// 注释:添加人信息
	 /// 可空:YES
	 /// </summary>

	 public String addUserId{ get; set; }

	 /// <summary>
	 /// 注释:添加人信息
	 /// 可空:YES
	 /// </summary>

	 public String addUserName{ get; set; }

	 /// <summary>
	 /// 注释:最后变动时间
	 /// 可空:YES
	 /// </summary>

	 public DateTime lastUpdateTime{ get; set; }

	 /// <summary>
	 /// 注释:是否删除 0 否 1 是 
	 /// 可空:NO
	 ///默认值:0
	 /// </summary>

	 public Int32 isDelete{ get; set; }

	 /// <summary>
	 /// 注释:删除时间
	 /// 可空:YES
	 /// </summary>

	 public DateTime deleteTime{ get; set; }

	 /// <summary>
	 /// 注释:最后修改人员Id
	 /// 可空:YES
	 /// </summary>

	 public String lastUpdateUserId{ get; set; }

	 /// <summary>
	 /// 注释:最后修改人姓名
	 /// 可空:YES
	 /// </summary>

	 public String lastUpdateUserName{ get; set; }

	 }
}

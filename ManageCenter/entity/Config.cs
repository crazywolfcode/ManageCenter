using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

 namespace ManageCenter
{

	 /// <summary>
	 /// 配置表
	 /// 数据条数:0
	 /// 数据大小:16KB
	 /// </summary>


	  public  class Config
	 {

	 /// <summary>
	 /// 可空:NO
	 ///默认值:0
	 /// </summary>

	 public String id{ get; set; }

	 /// <summary>
	 /// 注释:配置项名称
	 /// 可空:YES
	 /// </summary>

	 public String configName{ get; set; }

	 /// <summary>
	 /// 可空:YES
	 /// </summary>

	 public String configValue{ get; set; }

	 /// <summary>
	 /// 注释:0  其它配置 1 用户客户端配置 2应用客户端配置 3 用户平台配置 4应用平台配置 5平台配置
	 /// 可空:NO
	 ///默认值:1
	 /// </summary>

	 public Int32 configType{ get; set; }

	 /// <summary>
	 /// 注释:说明
	 /// 可空:YES
	 /// </summary>

	 public String description{ get; set; }

	 /// <summary>
	 /// 可空:YES
	 /// </summary>

	 public String stationId{ get; set; }

	 /// <summary>
	 /// 注释:所属验票站点的名称
	 /// 可空:YES
	 /// </summary>

	 public String stationName{ get; set; }

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

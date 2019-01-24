using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
    public enum ConfigItemName
    {
        //1 连接数据库 2 设置当站点名称
        InitStep,
        CurrStationId,
         //1耀华系 2 宁波柯力 3   托利多 4赛多利斯 0其它      
        //connectionStrings
        appSettings,
        connectionStrings,
        mysqlConn,
        //appSettings    
        coryRight,      
        defaultPrintFrequency,
        BillDescription,//磅单联的介绍
        PrintTitle,//磅单打印的标题
        CashReportTitle,//磅单打印的标题
        autoPrintSecend,//自动开始打印的秒数
        autoPrint,//是否自动开始打印
        IsUnifeidLimitTone,//统一限定的吨位
        limitTone,
       // 读卡器,
        IcCom,
        IcBaudRate,
        SoftSrartDate,
        SoftEndDate,

        //listener
        listenerPort,

        imageSavePath,

        isOpenListence,//是否开启端口监听
    }


    public enum ReceiveMoneyType {
        No,Yes,NotNeed            
    }

    //角色级别 0 验票员 1 审核员 2 监管员 3系统作者
    public enum RoleLevelType
    {
        YPY, SHY, JGY,XTZZ
    }

    /// <summary>
    ///0  其它配置 1 用户客户端配置 2应用客户端配置 3 用户平台配置 4应用平台配置 5平台配置
    /// </summary>
    public enum ConfigType
    {
        OtherConfig,
        clientUserConfig,
        ClientAppConfig,
        platformUserConfig,
        platformAppConfig,
        platformConfig
    }
}

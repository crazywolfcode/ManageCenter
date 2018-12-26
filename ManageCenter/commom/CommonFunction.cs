using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using MyCustomControlLibrary;

namespace ManageCenter
{
    /// <summary>
    /// CoMmon Function Library
    /// </summary>
    public class CommonFunction
    {
        
        #region 显示提示窗口

        /// <summary>
        /// 显示提示窗口
        /// </summary>
        /// <param name="content">提示内容</param>
        /// <param name="Title">标题</param>
        ///   /// <param name="orientation">方向</param>
        public static void ShowAlert(String content, String Title = "提示", Orientation orientation = Orientation.Horizontal)
        {
            MMessageBox.GetInstance().ShowBox(content, Title, MMessageBox.ButtonType.Yes, MMessageBox.IconType.Info, orientation,"好");
        }

        public static void ShowSuccessAlert(String content, String Title = "提示", Orientation orientation = Orientation.Horizontal)
        {
            MMessageBox.GetInstance().ShowBox(content, Title, MMessageBox.ButtonType.Yes, MMessageBox.IconType.success, orientation,"好");
        }

        public static void ShowErrorAlert(String content, String Title = "错误", Orientation orientation = Orientation.Horizontal)
        {
            MMessageBox.GetInstance().ShowBox(content, Title, MMessageBox.ButtonType.No, MMessageBox.IconType.success, orientation);
        }
        public static MMessageBox.Result ShowAlertResult()
        {
            return MMessageBox.GetInstance().ShowBox("保存成功 ! 要继续过磅吗？", "恭喜", MMessageBox.ButtonType.YesNo, MMessageBox.IconType.success, Orientation.Vertical, "是");
        }

        #endregion

        //角色级别 0 验票员 1 审核员 2 监管员 3系统作者
        public static string GetRoleName(RoleLevelType type) {
            int temp = (int)type;
            String res = "";
            switch (temp)
            {
                case 0:
                    res = "验票员";
                    break;
                case 1:
                    res = "审核员";
                    break;
                case 2:
                    res = "监管员";
                    break;
                case 3:
                    res = "系统作者";
                    break;         
            }
            return res;
        }
        public static readonly string connectionStringTemplate = "Database={0};Data Source={1};User Id={2};Password={3};pooling=false;CharSet=utf8;port={4};";        
        public static string BuildMyqlconn(String db,String ip, String user, String pwd, String port)
        {
            return string.Format(connectionStringTemplate, db, ip, user, pwd, port);
        }
    }
}


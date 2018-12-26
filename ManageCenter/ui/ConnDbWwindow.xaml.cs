using MyHelper;
using System.Windows;
using System.Windows.Input;
namespace ManageCenter
{
    /// <summary>
    /// SettingW.xaml 的交互逻辑
    /// </summary>
    public partial class ConnDbWwindow : Window
    {
        //是否为再次设置
        private bool IsReSet = false;

        public ConnDbWwindow(bool isReSet = false)
        {
            this.IsReSet = isReSet;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          

        }
        #region Window Default Event
        /// <summary>
        /// window move event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void headerBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
        #endregion
        
               
        private void Connbtn_Click(object sender, RoutedEventArgs e)
        {
          

            string ip = this.IpTb.Text.Trim();
            if (string.IsNullOrEmpty(ip))
            {
                CommonFunction.ShowErrorAlert("IP地址不能为空！");
                IpTb.Focus();
                return;
            }
            string port = this.portTb.Text.Trim();
            if (string.IsNullOrEmpty(port))
            {
                CommonFunction.ShowErrorAlert("端口不能为空，如：3306");
                portTb.Focus();
                return;
            }
            string dbname = this.dbNameTb.Text.Trim();
            if (string.IsNullOrEmpty(dbname))
            {
                CommonFunction.ShowErrorAlert("数据库名称不能为空");
                dbNameTb.Focus();
                return;
            }
            string userid = this.usernameTb.Text.Trim();
            if (string.IsNullOrEmpty(dbname))
            {
                CommonFunction.ShowErrorAlert("用户名称不能为空");
                usernameTb.Focus();
                return;
            }
            string pwd = this.pwdTb.Text.Trim();
            string connstr = CommonFunction.BuildMyqlconn(dbname,ip,userid,pwd,port);
            ConfigurationHelper.SetConnectionConfig(ConfigItemName.mysqlConn.ToString(), connstr);
            if (DatabaseOPtionHelper.GetInstance().CheckConn(connstr))
            {
                try
                {                    
                    ConfigurationHelper.SetConnectionConfig(ConfigItemName.mysqlConn.ToString(), connstr);
                    ConfigurationHelper.SetConfig(ConfigItemName.InitStep.ToString(), "2");
                    if (IsReSet)
                    {
                        CommonFunction.ShowSuccessAlert("保存成功！");
                    }
                    else
                    {
                        CommonFunction.ShowSuccessAlert("保存成功！,需要重先启动");
                    }                          
                    this.Close();                   
                } catch {
                    CommonFunction.ShowErrorAlert("保存失败！");
                }                
            }
            else
            {
                CommonFunction.ShowErrorAlert("连接不成功！无法保存！");
                return;
            }

        }
    }
}

using MyCustomControlLibrary;
using MyHelper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
namespace ManageCenter
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region variable area
        DispatcherTimer mDispatcherTimer;      
        private bool isLogout = false;
        private bool softExpird = false;
        #endregion

        public MainWindow()
        {
            App.Current.MainWindow = this;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartClock();
            App.Current.MainWindow = this;
            this.CurrUserBtn.Content = App.currentUser.name;
            this.RoleNameTb.Text = App.currentUser.roleName;
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {

            CheckSoftExpiredTime();
        }
 


        #region 时钟
        private void StartClock()
        {
            mDispatcherTimer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 1),
            };
            mDispatcherTimer.Tick += MDispatcherTimer_Tick;
            mDispatcherTimer.Start();
        }

        private void MDispatcherTimer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string timeStr = string.Format("{0}年{1}月{2}日 {3}:{4}:{5}",
                now.Year,
                now.Month.ToString("00"),
                now.Day.ToString("00"),
                now.Hour.ToString("00"),
                now.Minute.ToString("00"),
                now.Second.ToString("00"));
            this.currTimeTb.Text = timeStr;
        }
        #endregion



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isLogout == false)
            {
                e.Cancel = true;
                this.WindowState = WindowState.Minimized;
                String title = "煤炭运煤监管系统 ";
                String text = "最小化在到这里";
                App.ShowBalloonTip(title, text);
                return;
            }
            if (mDispatcherTimer != null)
            {
                mDispatcherTimer.Stop();
            }       
        }
     

    
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.IsLoaded == false)
            {
                return;
            }      
        }

        /// <summary>
        /// 报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportBtn_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow() { Owner = this }.ShowDialog();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            isLogout = true;
            new LoginWindow() { IsChangeAccount = true }.Show();
            this.Close();
        }
        /// <summary>
        ///  MenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            if (item == null)
            {
                return;
            }          
            switch (item.Name)
            {
                case "BaseSettingMI":
                    new SettingW().ShowDialog();
                    break;
                case "HeithtSettintMI":
                    new SettingW(1).ShowDialog();
                    break;
                case "SystemSettintMI":
                    if (App.currentUser.roleLevel == (int)RoleLevelType.XTZZ)
                    {
                        new SettingW(2).ShowDialog();
                    }
                    else
                    {
                        CommonFunction.ShowAlert("无权操作");
                    }         
                    break;
                case "UserManagerMI":
                    
                    break;
                case "BillReporetMI":
                    new ReportWindow().ShowDialog();
                    break;
                case "TaxationReporetMI":
                    new CashReportWindow().ShowDialog();
                    break;
                case "CarManageMI":
                    new CarManageWindow().ShowDialog();
                    break;
                case "AddMaterialMI":

                    break;
                case "MaterialManageMI":
                    new MaterialManageWindow().ShowDialog();
                    break;
                case "CompanyManageMI":
                    new CompanyManageWindow().ShowDialog();
                    break;
                case "AboutMI":
                    new AboutW().ShowDialog();
                    break;
                case "ConnMeMI":
                    new ConnectionWindow().ShowDialog();
                    break;

            }
        }          
        private void CheckSoftExpiredTime()
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                try
                {
                    long end = Convert.ToInt64(ConfigurationHelper.GetConfig(ConfigItemName.SoftEndDate.ToString()));
                    end = end / 1000;

                    long now = DateTimeHelper.GetTimeStamp() / 1000;
                    decimal day = Math.Floor(Convert.ToDecimal((end - now) / 86400));

                    if (day <= 30)
                    {
                        this.Dispatcher.Invoke(new Action(delegate {
                            if (day <= 0)
                            {
                                softExpird = true;
                                this.SoftExpiredAlertTB.Text = $"系统服务到期";
                            }
                            else
                            {
                                this.SoftExpiredAlertTB.Text = $" {day} 天后系统服务到期";
                            }

                            this.SoftExpiredSBI.Visibility = Visibility.Visible;
                        }));
                    }
                }
                catch { }
            }))
            {
                IsBackground = true,
            };
            thread.Start();

        }
    }
}

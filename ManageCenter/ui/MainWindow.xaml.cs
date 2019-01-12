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
using Visifire;
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

        private string FrequencySql = " SELECT station_name, COUNT(id) as id from weighing_bill where is_delete = 0 GROUP BY station_name;";

        private string WeightSql = "SELECT station_name, sum(net_weight) as net_weight from weighing_bill where is_delete = 0 GROUP BY station_name;";

        private string MoneySql = "SELECT station_name, sum(overtop_money) as overtop_money from weighing_bill where is_delete = 0 GROUP BY station_name;";

        private string CompanySql = "SELECT send_company, COUNT(id) as id , sum(send_net_weight) as send_net_weight from weighing_bill where is_delete = 0 GROUP BY send_company;";

        private string LineSqlT = "SELECT sum(net_weight) as net_weight,station_name , DATE_FORMAT(add_time,\" %Y-%m-%d\")as add_time from weighing_bill where is_delete = 0 and add_time >'{0}' and station_id ='{1}' GROUP BY station_name,DATE_FORMAT(add_time,\"%y%m%d\") ORDER BY add_time;";


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
            StationModel.GetList();

            BuildChart();
        }


        #region BuildChart

        private void BuildChart() {
            BuildChart1();
            BuildChart2();
            BuildChart3();
            BuildChart4();
            BuildChart5();
        }

        /// <summary>
        /// 站点验票次数对比图
        /// </summary>
        private void BuildChart1() {
            this.Chart1.Series.Clear();
            Visifire.Charts.DataSeries dataSeries = new Visifire.Charts.DataSeries() { RenderAs = Visifire.Charts.RenderAs.Radar};
            List<ChartDataV> list = DatabaseOPtionHelper.GetInstance().select<ChartDataV>(this.FrequencySql);
            for (int i = 0; i < list.Count; i++)
            {
                ChartDataV data = list[i];
                dataSeries.DataPoints.Add(new Visifire.Charts.DataPoint() { AxisXLabel =data.stationName, YValue =data.id });
            }                    
            this.Chart1.Series.Add(dataSeries);
        }

        /// <summary>
        /// 站点验票吨数对比图
        /// </summary>
        private void BuildChart2()
        {
            this.Chart2.Series.Clear();
            Visifire.Charts.DataSeries dataSeries = new Visifire.Charts.DataSeries() { RenderAs = Visifire.Charts.RenderAs.Pie };
            List<ChartDataV> list = DatabaseOPtionHelper.GetInstance().select<ChartDataV>(this.WeightSql);
            for (int i = 0; i < list.Count; i++)
            {
                ChartDataV data = list[i];
                dataSeries.DataPoints.Add(new Visifire.Charts.DataPoint() { AxisXLabel = data.stationName, YValue = data.netWeight });
            }
            this.Chart2.Series.Add(dataSeries);

        }
        /// <summary>
        /// 站点补收税款对比图
        /// </summary>
        private void BuildChart3()
        {
            this.Chart3.Series.Clear();
            Visifire.Charts.DataSeries dataSeries = new Visifire.Charts.DataSeries() { RenderAs = Visifire.Charts.RenderAs.Doughnut };
            List<ChartDataV> list = DatabaseOPtionHelper.GetInstance().select<ChartDataV>(this.MoneySql);
            for (int i = 0; i < list.Count; i++)
            {
                ChartDataV data = list[i];
                dataSeries.DataPoints.Add(new Visifire.Charts.DataPoint() { AxisXLabel = data.stationName, YValue = data.overtopMoney });
            }
            this.Chart3.Series.Add(dataSeries);

        }

        /// <summary>
        /// 公司发货吨数对比图
        /// </summary>
        private void BuildChart4()
        {
            this.Chart4.Series.Clear();
            Visifire.Charts.DataSeries dataSeries = new Visifire.Charts.DataSeries() { RenderAs = Visifire.Charts.RenderAs.Column };
            List<ChartDataV> list = DatabaseOPtionHelper.GetInstance().select<ChartDataV>(this.CompanySql);
            for (int i = 0; i < list.Count; i++)
            {
                ChartDataV data = list[i];
                dataSeries.DataPoints.Add(new Visifire.Charts.DataPoint() { AxisXLabel = data.sendCompany, YValue = data.sendNetWeight });
            }
            this.Chart4.Series.Add(dataSeries);

        }

        /// <summary>
        /// 站点验票吨数走趋图
        /// </summary>
        private void BuildChart5()
        {
            List<Station> stationList = StationModel.GetList();
            List<string> sqls = new List<string>();
            DateTime startDate =DateTime.Now.AddDays(-31);           
            foreach (var item in stationList)
            {
                sqls.Add(String.Format(LineSqlT,  startDate.ToString("yyyy-MM-dd"),item.id));              
            }
            List<Visifire.Charts.DataSeries> dataSeries = new List<Visifire.Charts.DataSeries>();
            if (sqls.Count > 0) {
                this.Chart5.Series.Clear();
                foreach (var item in sqls)
                {
                    List<ChartDataV> list = DatabaseOPtionHelper.GetInstance().select<ChartDataV>(item);                    
                    if (list.Count <= 0) {
                        continue;
                    }
                    Visifire.Charts.DataSeries series = new Visifire.Charts.DataSeries() { RenderAs = Visifire.Charts.RenderAs.Spline };
              
                    foreach (var data in list)
                    {
                        series.LegendText = data.stationName;
                        series.DataPoints.Add(new Visifire.Charts.DataPoint() { AxisXLabel = data.addTime, YValue = data.netWeight });
                    }
                    dataSeries.Add(series);
                }               
            }
            if (dataSeries.Count > 0) {
                foreach (var item in dataSeries)
                {
                    this.Chart5.Series.Add(item);
                }
            }
        }
        #endregion

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
                case "StationanagerMI":
                    new StationManageWondow().ShowDialog();
                    break;
                case "passwordMI":
                    new PasswordWindow().Show();
                    break;
                case "UserManagerMI":
                    new UserWindow().ShowDialog();
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
                    new MaterialManageWindow().ShowDialog();
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
                        this.Dispatcher.Invoke(new Action(delegate
                        {
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
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshContextBtn_Click(object sender, RoutedEventArgs e)
        {
            BuildChart();
        }
    }
}

using MyCustomControlLibrary;
using MyHelper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ManageCenter
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static Station mStation;
        internal static User currentUser;
        public static System.Windows.Forms.NotifyIcon notifyIcon;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            int initstep = Convert.ToInt32(MyHelper.ConfigurationHelper.GetConfig(ConfigItemName.InitStep.ToString()));

            if (initstep < 3)
            {
                if (initstep == 1)
                {
                    new ConnDbWwindow().ShowDialog();
                }
                if (initstep == 2)
                {
                    new SetStationWindow().ShowDialog();
                }
            }

            CreateNotifyIcon();
            mStation = StationModel.SelectById(MyHelper.ConfigurationHelper.GetConfig(ConfigItemName.CurrStationId.ToString()));
            Window loginWindow = new LoginWindow();
            Current.MainWindow = loginWindow;
            Current.MainWindow.Show();
            //监听站点的请求
            StartListener();
        }
        /// <summary>
        /// 监听站点的请求
        /// </summary>
        private void StartListener() {
            String url = ConfigurationHelper.GetConfig(ConfigItemName.listenerPort.ToString());
            Thread thread = new Thread(new ThreadStart(delegate { HttpListenerHelper.Start(url); })) {IsBackground =true };
            thread.Start();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            String assimblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();

            bool createNew = true;
            EventWaitHandle ProgramStarted = new EventWaitHandle(false, EventResetMode.AutoReset, assimblyName, out createNew);
            if (!createNew)
            {
                MMessageBox.GetInstance().ShowBox("该程序已经在运行中，不能重复创建！", "提示", MMessageBox.ButtonType.Yes, MMessageBox.IconType.warring, System.Windows.Controls.Orientation.Vertical, "好");
                App.Current.Shutdown();
                Environment.Exit(0);
            }

            base.OnStartup(e);
        }


        #region  Notify icon

        /// <summary>
        /// 创建Notification
        /// </summary>
        private void CreateNotifyIcon()
        {
            notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                BalloonTipTitle = "煤炭运煤监管系统",
                BalloonTipText = "正常启动",
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath),
                //Icon = new System.Drawing.Icon("aislogo_48.ico"),
                Visible = true,
                BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
            };
            notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
            notifyIcon.ShowBalloonTip(1000);
            notifyIcon.Text = "煤炭运煤监管系统";
            notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
            notifyIcon.Click += NotifyIcon_Click;
            notifyIcon.ContextMenu = GetNotifyMenu();
        }


        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            MMessageBox.GetInstance().ShowBox("你确定更新程系吗", "提示", MMessageBox.ButtonType.YesNo, MMessageBox.IconType.Info);
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            if (App.Current.MainWindow.WindowState == WindowState.Minimized)
            {
                App.Current.MainWindow.WindowState = WindowState.Normal;
                Current.MainWindow.ShowActivated = true;
            }
            else
            {
                App.Current.MainWindow.WindowState = WindowState.Minimized;
            }

        }

        private void NotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //ShowCurrentWindow();
        }

        /// <summary>
        /// 创建Notify icon Menu
        /// </summary>
        /// <returns></returns>
        public System.Windows.Forms.ContextMenu GetNotifyMenu()
        {
            System.Windows.Forms.MenuItem[] notifyMenu;
            System.Windows.Forms.MenuItem quitMenuItem = new System.Windows.Forms.MenuItem();
            quitMenuItem.Text = "退出";
            quitMenuItem.DefaultItem = true;
            quitMenuItem.Click += QuitMenuItem_Click;
            notifyMenu = new System.Windows.Forms.MenuItem[] { quitMenuItem };
            return new System.Windows.Forms.ContextMenu(notifyMenu);
        }

        private void QuitMenuItem_Click(object sender, EventArgs e)
        {
            //currWindow.Activate();

            MMessageBox.Result result = MMessageBox.GetInstance().ShowBox("你确定退出程系吗", "提示", MMessageBox.ButtonType.YesNo, MMessageBox.IconType.Info);
            if (result == MMessageBox.Result.Yes)
            {
                Current.Shutdown();
            }
            else
            {
                Current.MainWindow.ShowActivated = true;
            }
        }
        public static void ShowBalloonTip(String title, String text)
        {
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = text;
            notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(1000);
        }
        #endregion

        #region quit event

        /// <summary>
        /// save the config file's config Item to database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            // ScannerGunHelper.Close();

            //DateTime start = DateTime.Now;
            //    try
            //    {
            //        insertOrUpdateConnectionStrings();
            //}
            //    catch (Exception exception)
            //    {
            //        ConsoleHelper.writeLine("save app ConnectionStrings to dabase error: " + exception.Message);
            //    }
            //try
            //{
            Thread thread = new Thread(new ThreadStart(InsertOrUpdateAppSettings));
            thread.IsBackground = true;
            thread.Start();
            SaveTempData();        
            Thread.Sleep(1200);
            //}
            //catch (Exception exception)
            //{
            //    ConsoleHelper.writeLine("save AppSettings to dabase error: " + exception.Message);
            //}
            //double time = DateTimeHelper.DateDifflMilliseconds(start, DateTime.Now);

            //ConsoleHelper.writeLine("suer time :" + time + " ms");

        }
        /// <summary>
        /// insert Or Update Connection Strings
        /// </summary>
        private void InsertOrUpdateConnectionStrings()
        {
            ConnectionStringSettingsCollection conns = ConfigurationManager.ConnectionStrings;
            string sql = string.Empty;
            for (int i = 0; i < conns.Count; i++)
            {
                Config config = null;
                if (!conns[i].Name.Contains("Local"))
                {
                    sql = DatabaseOPtionHelper.GetInstance().getSelectSql(TableName.config.ToString(), null, ConfigColumns.station_id+"=' " + ConfigurationHelper.GetConfig(ConfigItemName.CurrStationId.ToString()) + "' and config_name = ' " + conns[i].Name + "'", null, null, null, 1);

                    List<Config> configs = DatabaseOPtionHelper.GetInstance().select<Config>(sql);
                    if (configs != null && configs.Count > 0)
                    {
                        config = JsonHelper.JsonToObject(JsonHelper.ObjectToJson(configs[0]), typeof(Config)) as Config;
                        if (config != null)
                        {
                            if (config.configValue != conns[i].ConnectionString)
                            {
                                config.configValue = conns[i].ConnectionString;
                                config.lastUpdateTime = DateTime.Now;
                                if (App.currentUser != null)
                                {
                                    config.lastUpdateUserId = config.addUserId;
                                    config.lastUpdateUserName = config.addUserName;
                                }
                            }
                            DatabaseOPtionHelper.GetInstance().update(config);
                        }
                        else
                        {
                            //conveter error
                        }
                    }
                    else
                    {
                        config = new Config();
                        config.id = Guid.NewGuid().ToString();
                        config.stationId = ConfigurationHelper.GetConfig(ConfigItemName.CurrStationId.ToString());
                        config.configName = conns[i].Name;
                        config.configValue = conns[i].ConnectionString;
                        config.addTime = DateTime.Now;
                        config.configType = (int)ConfigType.ClientAppConfig;
                        config.stationName = mStation.name;
                        if (App.currentUser != null)
                        {
                            config.addUserId = App.currentUser.id;
                            config.addUserName = App.currentUser.name;
                            config.lastUpdateUserId = config.addUserId;
                            config.lastUpdateUserName = config.addUserName;
                        }
                        DatabaseOPtionHelper.GetInstance().insert(config);
                    }
                }
            }
        }
        /// <summary>
        /// insert Or Update App Settings
        /// </summary>
        private void InsertOrUpdateAppSettings()
        {
            try {
                SqlDao.DbHelper helper = DatabaseOPtionHelper.GetInstance(); ;
                string sql = string.Empty;
                NameValueCollection collection = ConfigurationManager.AppSettings;
                string[] keys = collection.AllKeys;
                foreach (string key in keys)
                {
                    Config config = null;
                    String condition = ConfigColumns.station_id + " ='" + ConfigurationHelper.GetConfig(ConfigItemName.CurrStationId.ToString()) + "' and " + ConfigColumns.config_name.ToString() + " = '" + key + "'";
                    sql = helper.getSelectSql(TableName.config.ToString(), null, condition, null, null, null, 1);
                    List<Config> configs = helper.select<Config>(sql);
                    if (configs != null && configs.Count > 0)
                    {
                        if (configs[0] != null)
                        {
                            config = configs[0];
                            if (config.configValue != collection[key].ToString())
                            {
                                config.configValue = collection[key].ToString();
                                config.lastUpdateTime = DateTime.Now;
                                if (App.currentUser != null)
                                {
                                    config.lastUpdateUserId = App.currentUser.id;
                                    config.lastUpdateUserName = App.currentUser.name;
                                }
                            }
                            helper.update(config);
                        }
                        else
                        {
                            //conveter error
                        }
                    }
                    else
                    {
                        config = new Config
                        {
                            id = Guid.NewGuid().ToString(),
                            addTime = DateTime.Now,
                            configName = key,
                            stationId = ConfigurationHelper.GetConfig(ConfigItemName.CurrStationId.ToString()),
                            stationName = mStation.name,
                            configValue = collection[key].ToString(),
                            configType = (int)ConfigType.ClientAppConfig
                        };
                        config.lastUpdateTime = config.addTime;
                        if (App.currentUser != null)
                        {
                            config.addUserId = App.currentUser.id;
                            config.addUserName = App.currentUser.name;
                            config.lastUpdateUserId = config.addUserId;
                            config.lastUpdateUserName = config.addUserName;
                        }
                        helper.insert(config);
                    }
                }
            } catch (Exception e) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
            }                     
        }

        /// <summary>
        /// 保存本机使用过的基础数据
        /// </summary>
        private void SaveTempData() { }
        #endregion

        public static String getAssemblyName()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        }

        public static String GetVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

    }
}

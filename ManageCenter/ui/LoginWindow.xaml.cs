using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MyCustomControlLibrary;
using MyHelper;
namespace ManageCenter
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public bool IsChangeAccount = false;
        private List<HostoryUser> HostoryUsers;
        private bool IsAutoLogin = false;
        private bool IsRemberPwd = false;
        private HostoryUser hostoryUser;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetConfig();
        }

        private void SetConfig() {

            List<Config> configs = ConfigModel.GetCurrStationConfigs();
            foreach (var item in configs)
            {
                ConfigurationHelper.SetConfig(item.configName, item.configValue);
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            HostoryUsers = GetHostoryUsers();
            if (HostoryUsers != null && HostoryUsers.Count > 0)
            {
                this.mobileTb.ItemsSource = HostoryUsers;
                this.mobileTb.SelectedIndex = 0;
            }
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

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            Size size = new Size(mainborder.ActualWidth, mainborder.ActualHeight);
            Point point = this.mainborder.PointToScreen(new Point());
            MMessageBox.GetInstance().ShowLoading(
                MMessageBox.LoadType.Three,
                "登陆中...",
                point,
                size,
                "&#xe752;",
                Orientation.Vertical,
                "#ffffff",
                5);
            string mobile = this.mobileTb.Text.Trim();
            if (this.mobileTb.SelectedIndex >= 0) {
                mobile = hostoryUser.phone;
            }
            if (!RegexHelper.IsMobilePhoneNumber(mobile))
            {
                MMessageBox.GetInstance().ShowModalAlert("输入的电话号码不正确", point, size, Orientation.Vertical, null, "#ffffff", true, false);
                this.AlertPanel.Visibility = Visibility;
                this.AlertTb.Text = "输入的电话号码不正确;";
                this.mobileTb.BorderBrush = Brushes.Red;
                return;
            }
            string pwdStr = this.pwdPb.Password.Trim();
            if (String.IsNullOrEmpty(pwdStr) || pwdStr.Length < 6)
            {
                MMessageBox.GetInstance().ShowModalAlert("密码长度至少6位", point, size, Orientation.Vertical, null, "#ffffff", true, false);
                this.AlertPanel.Visibility = Visibility;
                this.AlertTb.Text = "密码长度至少6位;";
                this.pwdPb.BorderBrush = Brushes.Red;
                return;
            }

            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                String password = pwdStr;
                if (this.IsRemberPwd == false || hostoryUser ==null || hostoryUser.isRemberPwd == false)
                {
                    password = EncryptHelper.MD5Encrypt(pwdStr, false);
                }
                User user = UserModel.Login(mobile, password);             
                this.Dispatcher.Invoke(new Action(delegate
                {
                    if (user != null)
                    {
                        if (user.roleLevel !=(int) RoleLevelType.XTZZ) {
                            if (user.stationId != App.mStation.id)
                            {
                                MMessageBox.GetInstance().ShowBox("禁止登陆", "提示", MMessageBox.ButtonType.Yes, MMessageBox.IconType.warring, Orientation.Horizontal, "好");
                                this.AlertPanel.Visibility = Visibility;
                                this.AlertTb.Text = "你不属于监管中心的人员 ，禁止登陆";
                                return;
                            }
                        }                       
                        App.currentUser = user;
                        DoubleAnimation animation = new DoubleAnimation
                        {
                            From = 0,
                            To = 1,
                            Duration = new Duration(TimeSpan.FromMilliseconds(800))
                        };
                        animation.Completed += Animation_Completed;
                        this.BeginAnimation(OpacityProperty, animation);
                    }
                    else
                    {
                        MMessageBox.GetInstance().ShowModalAlert("登陆失败账号或者密码错误", point, size, Orientation.Vertical, null, "#ffffff", true, false);
                        this.AlertPanel.Visibility = Visibility;
                        this.AlertTb.Text = "登陆失败账号或者密码错误;";
                        this.mobileTb.BorderBrush = Brushes.Red;
                        return;
                    }
                }), System.Windows.Threading.DispatcherPriority.Normal);
            }));
            thread.Start();
        }

        private void Animation_Completed(object sender, EventArgs e)
        {
            this.Close();
            new MainWindow().Show();
            App.ShowBalloonTip("登陆成功","在使用过程中需要帮助，联系：陈龙飞 18087467482 ");
            
            new System.Threading.Thread(new System.Threading.ThreadStart(saveToFile)).Start();            
        }

        private void RemberPwdCBox_Checked(object sender, RoutedEventArgs e)
        {
            IsRemberPwd = true;
        }

        private void RemberPwdCBox_Unchecked(object sender, RoutedEventArgs e)
        {
            IsRemberPwd = false;
            this.AutoLoginCbox.IsChecked = false;
        }

        private void AutoLoginCbox_Checked(object sender, RoutedEventArgs e)
        {
            IsAutoLogin = true;
            this.RemberPwdCBox.IsChecked = true;
        }

        private void AutoLoginCbox_Unchecked(object sender, RoutedEventArgs e)
        {
            IsAutoLogin = false;
        }

        private void saveToFile()
        {
            User user = App.currentUser;
            if (user == null)
            {
                return;
            }
            HostoryUser hostoryUser = new HostoryUser()
            {
                phone = user.phone,
                pwd = user.pwd,
                isAutoLogin = this.IsAutoLogin,
                isRemberPwd = this.IsRemberPwd
            };
            List<HostoryUser> hUsers;
            string path = Constract.tempPath;
            string filePath = Constract.HUserFilePath;
            if (FileHelper.FolderExistsCreater(path))
            {
                if (!FileHelper.Exists(filePath))
                {
                    try
                    {
                        FileHelper.CreateFile(filePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("保存失败,文件创建失败：" + filePath + " msg:" + ex.Message);
                        return;
                    }
                }
                string xml = FileHelper.Reader(filePath, Encoding.UTF8);
                hUsers = (List<HostoryUser>)XmlHelper.Deserialize(typeof(List<HostoryUser>), xml);
                if (hUsers == null)
                {
                    hUsers = new List<HostoryUser>();
                    hUsers.Add(hostoryUser);
                }
                else
                {
                    for (int i = 0; i < hUsers.Count; i++)
                    {
                        if (hUsers[i].phone.Equals(hostoryUser.phone))
                        {
                            hUsers.RemoveAt(i);
                            break;
                        }
                    }
                    hUsers.Insert(0, hostoryUser);
                }
                string connStrings = XmlHelper.Serialize(typeof(List<HostoryUser>), hUsers);
                try
                {
                    FileHelper.Write(filePath, connStrings);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("保存" + "msg:" + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("保存失败！路径创建失败：" + path);
            }
        }

        private List<HostoryUser> GetHostoryUsers()
        {
            string filePath = Constract.HUserFilePath;
            string xml = FileHelper.Reader(filePath, Encoding.UTF8);
            return (List<HostoryUser>)XmlHelper.Deserialize(typeof(List<HostoryUser>), xml);
        }
        private void mobileTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.IsLoaded) {
                String str = this.mobileTb.Text.Trim();
                if (str.Length <= 0) {
                    hostoryUser = null;
                    this.RemberPwdCBox.IsChecked = false;
                    this.AutoLoginCbox.IsChecked = false;
                    this.pwdPb.Password = String.Empty;
                    hostoryUser = null;
                    this.mobileTb.SelectedIndex = -1;
            }
            }
        }
        private void mobileTb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hostoryUser = this.mobileTb.SelectedItem as HostoryUser;
            if (hostoryUser == null)
            {
                return;
            }
          
            this.RemberPwdCBox.IsChecked = hostoryUser.isRemberPwd;
            this.AutoLoginCbox.IsChecked = hostoryUser.isAutoLogin;
            this.IsAutoLogin = hostoryUser.isAutoLogin;
            if (hostoryUser.isRemberPwd) {
                this.pwdPb.Password = hostoryUser.pwd;
            }
            if (hostoryUser.isRemberPwd == true && hostoryUser.isAutoLogin == true)
            {
                if (IsChangeAccount == false) {
                    LoginBtn_Click(null, null);
                }
            }
        }
        
        private void forgotPwaTb_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

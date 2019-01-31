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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ManageCenter
{
    /// <summary>
    /// PasswordWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public PasswordWindow()
        {
            InitializeComponent();
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


        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            String oldpwd = this.oldTb.Text.Trim();

            if (oldpwd.Length < 6) {
                CommonFunction.ShowAlert("密码不能少于6位");
                return;
            }
            String newpwd = this.nowTb.Text.Trim();
            if (newpwd.Length < 6)
            {
                CommonFunction.ShowAlert("新密码不能少于6位");
                return;
            }

            if (oldpwd.Equals(newpwd)) {
                CommonFunction.ShowAlert("新旧密码不能相同");
                return;
            }

            if (MyHelper.EncryptHelper.MD5Encrypt(oldpwd, false).Equals(App.currentUser.pwd)) { } else {
                CommonFunction.ShowAlert("原始密码不正确");
                return;
            }

            App.currentUser.pwd = MyHelper.EncryptHelper.MD5Encrypt(newpwd, false);
            App.currentUser.lastUpdateUserId = App.currentUser.id;
            App.currentUser.lastUpdateUserName = App.currentUser.name;
            int res =DatabaseOPtionHelper.GetInstance().update(App.currentUser);

            if (res > 0)
            {
                CommonFunction.ShowSuccessAlert("修改成功！");
                App.Current.MainWindow.Close();
                new LoginWindow() { IsChangeAccount =true}.Show();
            }
            else {
                CommonFunction.ShowSuccessAlert("修改失败！");
            }
        }
    }
}

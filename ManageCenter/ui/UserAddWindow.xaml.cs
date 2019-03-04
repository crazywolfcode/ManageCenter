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
    /// UserAddWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserAddWindow : Window
    {

        private List<Roles> roleList;
        private List<Station> stationList;
        private User mUser;
        public UserAddWindow(User u = null)
        {
            InitializeComponent();
            this.mUser = u;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mUser != null)
            {
                this.Title = "修改用户";
                this.BodyTitle.Text = "修改用户";
            }
            else
            {
                this.Title = "添加用户";
                this.BodyTitle.Text = "添加用户";
            }

            roleList = RolesModel.GetList();
            this.RolesCb.ItemsSource = roleList;

            stationList = StationModel.GetList();
            this.StationCb.ItemsSource = stationList;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (mUser != null) {
                this.nameTb.Text = mUser.name;
                this.mobileTb.Text = mUser.phone;
                this.sexcb.SelectedIndex = mUser.sex;
                for (int i = 0; i <roleList.Count; i++)
                {
                    if (roleList[i].id.Equals(mUser.roleId)) {
                        this.RolesCb.SelectedIndex = i;
                        break;
                    }
                }
                for (int i = 0; i < stationList.Count; i++)
                {
                    if (stationList[i].id.Equals(mUser.stationId))
                    {
                        this.StationCb.SelectedIndex = i;
                        break;
                    }
                }
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
        private bool isInsert = true;
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mUser != null)
            {
                isInsert = false;
                mUser.lastUpdateUserId = App.currentUser.id;
                mUser.lastUpdateUserName = App.currentUser.name;
            }
            else
            {
                isInsert = true;
                mUser = new User()
                {
                    id = Guid.NewGuid().ToString(),
                    addUserId = App.currentUser.id,
                    addUserName = App.currentUser.name,
                    status = 1,
                };
            }
            if (MyHelper.RegexHelper.IsMobilePhoneNumber(this.mobileTb.Text.Trim()))
            {
                mUser.phone = this.mobileTb.Text.Trim();
                if (isInsert) {
                    if (UserModel.CheckUserByPhone(mUser.phone))
                    {
                        CommonFunction.ShowErrorAlert("手机号已经存在！");
                        return;
                    }
                }               
            }
            else
            {
                CommonFunction.ShowErrorAlert("输入的手机号，不正确");
                return;
            }

            if (String.IsNullOrEmpty(this.nameTb.Text.Trim()))
            {
                CommonFunction.ShowErrorAlert("真实姓名不能为空！");
                return;
            }
            else
            {
                mUser.name = this.nameTb.Text.Trim();
                mUser.nameFirstCase = MyHelper.StringHelper.GetFirstPinyin(mUser.name);
            }

            if (RolesCb.SelectedIndex < 0)
            {
                CommonFunction.ShowErrorAlert("必须为用户设定角色权限！");
                return;
            }
            else
            {
                Roles roles = RolesCb.SelectedItem as Roles;
                mUser.roleId = roles.id;
                mUser.roleName = roles.name;
                mUser.roleLevel = roles.roleLevel;
            }

            if (StationCb.SelectedIndex < 0)
            {
                CommonFunction.ShowErrorAlert("请选择用户所属的站点");
                return;
            }
            else
            {
                Station station = StationCb.SelectedItem as Station;
                mUser.stationId = station.id;
                mUser.stationName = station.name;
            }

            if (isInsert) {
                if (pwdPb.Password.Length < 6)
                {
                    CommonFunction.ShowErrorAlert("密码的长度到少6位！");
                    return;
                }
                else
                {
                    mUser.pwd = MyHelper.EncryptHelper.MD5Encrypt(this.pwdPb.Password.Trim(), false);
                }
            }        
            mUser.sex = sexcb.SelectedIndex;
            int res = 0;
            if (isInsert == true)
            {
                res = DatabaseOPtionHelper.GetInstance().insert(mUser);
                if (res > 0)
                {
                    CommonFunction.ShowSuccessAlert("保存成功");
                    this.Close();
                }
                else
                {
                    CommonFunction.ShowErrorAlert("保存失败");
                }
            }
            else
            {
                res = DatabaseOPtionHelper.GetInstance().update(mUser);
                if (res > 0)
                {
                    CommonFunction.ShowSuccessAlert("修改成功");
                    this.Close();
                }
                else
                {
                    CommonFunction.ShowErrorAlert("修改失败");
                }
            }
        }






    }
}

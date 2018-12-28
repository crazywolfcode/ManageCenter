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
    /// UserWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserWindow : Window
    {
        private User currUser;

        List<RoleUserV> RUVList;
        public UserWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BuildElement();
        }


        private void GetData()
        {
            List<Roles> roleList = RolesModel.GetList();
            RUVList = new List<RoleUserV>();
            for (int i = 0; i < roleList.Count; i++)
            {
                RUVList.Add(  new RoleUserV() { roles = roleList[i], UserList = UserModel.GetByRoleLevel(roleList[i].roleLevel) });                
            }
        }

        private void BuildElement()
        {
            if (RUVList == null || RUVList.Count <= 0)
            {
                return;
            }
            this.rolePanel.Children.Clear();
            for (int i = 0; i < RUVList.Count; i++)
            {
                Expander expander = new Expander();
                expander.Style = FindResource(ResourceName.BaseDataExpenderStyle.ToString()) as Style;
                expander.Header = RUVList[i].roles.name;             
                ListView listView = new ListView();
                listView.ItemsSource = RUVList[i].UserList;
                listView.Style = FindResource(ResourceName.BaseDataListViewStyle.ToString()) as Style;
                listView.ItemContainerStyle = FindResource(ResourceName.ListViewItemCompanyBaseDataStyle.ToString()) as Style;
                listView.SelectionChanged += ListView_SelectionChanged; ;
                expander.Content = listView;
                this.rolePanel.Children.Add(expander);
            }


        }
      private  ListView currentListView;
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv.SelectedIndex == -1)
            {
                return;
            }
            if (currentListView != lv)
            {
                if (currentListView != null)
                {
                    currentListView.SelectedIndex = -1;
                }
            }
            currentListView = lv;
            currUser = lv.SelectedItem as User;
            this.DetailGrid.DataContext = currUser;
            this.DetailGrid.Visibility = Visibility.Visible;
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

        private void UpdteBtn_Click(object sender, RoutedEventArgs e)
        {
            new UserAddWindow(currUser).ShowDialog();
            refresData();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (App.currentUser.roleLevel >= (int)RoleLevelType.JGY)
            {
                new UserAddWindow().ShowDialog();
                refresData();
            }
            else {
                CommonFunction.ShowAlert("无权限操作！");
            }
        }

        private void refresData() {
            GetData();
            BuildElement();
            this.currUser = null;
            this.DetailGrid.DataContext = null;
            this.DetailGrid.Visibility = Visibility.Hidden;
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            refresData();
        }

        private void DeleteCompanyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currUser.id == App.currentUser.id) {
                CommonFunction.ShowErrorAlert("自己不能删除自己！");
                return;
            }
            currUser.isDelete = 1;
            currUser.deleteTime = DateTime.Now;
            currUser.lastUpdateTime = DateTime.Now;
            currUser.lastUpdateUserId = App.currentUser.id;
            currUser.lastUpdateUserName = App.currentUser.name;
            int res = DatabaseOPtionHelper.GetInstance().delete(currUser);
            if (res > 0)
            {
                CommonFunction.ShowSuccessAlert("删除成功！");
                refresData();
            }
            else {
                CommonFunction.ShowErrorAlert("删除失败！");
            }
        }
    }
}

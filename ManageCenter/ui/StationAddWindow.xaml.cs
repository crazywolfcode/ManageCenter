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
    public partial class StationAddWindow : Window
    {

     
        private Station mStation;
        public StationAddWindow(Station m = null)
        {
            InitializeComponent();
            this.mStation = m;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mStation != null) {
                this.Title = "修改站点";
                this.BodyTitle.Text = "修改站点";
                this.nameTb.Text = mStation.name;
                this.privenceTb.Text = mStation.privence;
                this.cityTb.Text = mStation.city;
                this.countryTb.Text = mStation.country;
                this.addressTb.Text = mStation.address;
            }
            else {
                this.Title = "添加站点";
                this.BodyTitle.Text = "添加站点";
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
            if (mStation != null)
            {
                isInsert = false;
                mStation.lastUpdateTime = DateTime.Now;              
                    mStation.lastUpdateUserId = App.currentUser.id;
                    mStation.lastUpdateUserName = App.currentUser.name;               
            }
            else
            {
                isInsert = true;
                mStation = new Station()
                {
                    id = Guid.NewGuid().ToString(),
                    addTime = DateTime.Now,             
                status = 1,
                };
                if (App.currentUser != null)
                {
                    mStation.addUserId = App.currentUser.id;
                    mStation.addUserName = App.currentUser.name;
                }
                else
                {
                    mStation.addUserId = "初始值 ";
                    mStation.addUserName = "初始值 ";
                }
            }

            if (string.IsNullOrEmpty(this.privenceTb.Text.Trim()))
            {
                CommonFunction.ShowErrorAlert("所属省份不能为空！");
                return;
            }
            else {
                mStation.privence = this.privenceTb.Text.Trim();
            }

            if (string.IsNullOrEmpty(this.cityTb.Text.Trim()))
            {
                CommonFunction.ShowErrorAlert("所属州市不能为空！");
                return;
            }
            else
            {
                mStation.city = this.cityTb.Text.Trim();
            }


            if (string.IsNullOrEmpty(this.countryTb.Text.Trim()))
            {
                CommonFunction.ShowErrorAlert("所属区县不能为空！");
                return;
            }
            else
            {
                mStation.country = this.countryTb.Text.Trim();
            }

            if (string.IsNullOrEmpty(this.addressTb.Text.Trim()))
            {
                CommonFunction.ShowErrorAlert("详细地址不能为空！");
                return;
            }
            else
            {
                mStation.address = this.addressTb.Text.Trim();
            }

            if (string.IsNullOrEmpty(this.nameTb.Text.Trim())) {
                CommonFunction.ShowErrorAlert("站点名称不能为空！");
                return;
            }

            if (isInsert == false)
            {
                if (mStation.name != this.nameTb.Text.Trim())
                {
                    mStation.name = this.nameTb.Text.Trim();
                    if (StationModel.GetByName(mStation.name) != null)
                    {
                        CommonFunction.ShowErrorAlert("站点名称已经存在！");
                        mStation.name = null;
                        return;
                    }
                }
            }
            else {
                mStation.name = this.nameTb.Text.Trim();
                if (MaterialModel.GetByName(mStation.name) != null)
                {
                    CommonFunction.ShowErrorAlert("煤种名称已经存在！");
                    mStation.name = null;
                    return;
                }
            }         
         
            int res = 0;
            if (isInsert == true)
            {
                res = DatabaseOPtionHelper.GetInstance().insert(mStation);
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
                res = DatabaseOPtionHelper.GetInstance().update(mStation);
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

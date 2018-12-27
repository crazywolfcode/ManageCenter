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
    public partial class MaterailAddWindow : Window
    {

     
        private Material mMaterial;
        public MaterailAddWindow(Material m = null)
        {
            InitializeComponent();
            this.mMaterial = m;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mMaterial != null) {
                this.Title = "修改煤种";
                this.BodyTitle.Text = "修改煤种";
                this.nameTb.Text = mMaterial.name;
            }
            else {
                this.Title = "添加煤种";
                this.BodyTitle.Text = "添加煤种";
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (mMaterial != null)
            {
              
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
            if (mMaterial != null)
            {
                isInsert = false;
                mMaterial.lastUpdateTime = DateTime.Now;
                mMaterial.lastUpdateUserId = App.currentUser.id;
                mMaterial.lastUpdateUserName = App.currentUser.name;
            }
            else
            {
                isInsert = true;
                mMaterial = new Material()
                {
                    id = Guid.NewGuid().ToString(),
                    addTime = DateTime.Now,
                    addUserId = App.currentUser.id,
                    addUserName = App.currentUser.name,
                    status = 1,
                };
            }

            if (string.IsNullOrEmpty(this.nameTb.Text.Trim())) {
                CommonFunction.ShowErrorAlert("煤种名称不能为空！");
                return;
            }
            mMaterial.name = this.nameTb.Text.Trim();
            if (MaterialModel.GetByName(mMaterial.name) !=null) {
                CommonFunction.ShowErrorAlert("煤种名称已经存在！");
                mMaterial.name = null;
                return;
            }

            int res = 0;
            if (isInsert == true)
            {
                res = DatabaseOPtionHelper.GetInstance().insert(mMaterial);
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
                res = DatabaseOPtionHelper.GetInstance().update(mMaterial);
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

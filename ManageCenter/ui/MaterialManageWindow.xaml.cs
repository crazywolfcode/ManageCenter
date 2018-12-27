using MyCustomControlLibrary;
using MyHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ManageCenter
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialManageWindow : Window
    {       
    
        public MaterialManageWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {          
            LoadData();
        }
        public void LoadData()
        {            
            List<Material> list =MaterialModel.IndistinctSearchByNameOrNameFirstCase(this.MaterialNameCb.Text);
            this.ReportDataGrid.ItemsSource = list;
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

  
        #region Search Tab Button
        private void SearchTabBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.searchAreaPanel.Height == 0)
            {
                SearAreaAnimationToShow();
            }
            else
            {
                SearAreaAnimationToHidden();
            }
        }
        private void SearAreaAnimationToShow()
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 60;
            animation.Duration = TimeSpan.FromSeconds(0.8);
            this.searchAreaPanel.BeginAnimation(HeightProperty, animation);
        }
        private void SearAreaAnimationToHidden()
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 60;
            animation.To = 0;
            animation.Duration = TimeSpan.FromSeconds(0.5);
            this.searchAreaPanel.BeginAnimation(HeightProperty, animation);
        }
        #endregion

        #region EXport EXCL，
        private void ExportExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            ExclHelper.ExclExprotToExcelWitchStatisticInfo(this.ReportDataGrid, "物料信息"+DateTimeHelper.getCurrentDateTime(DateTimeHelper.DateFormat),"物料信息","","","",null);             
        }

        private List<String> GetListStatisticToListString(ListBox listBox)
        {
            if (listBox == null || listBox.Items.Count <= 0)
            {
                return null;
            }
            List<string> list = new List<string>();

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                TextBlock tb = (TextBlock)listBox.Items[i];
                if (tb != null)
                {
                    list.Add(tb.Text);
                }
                else
                {
                    list.Add(" ");
                }
            }
            return list;
        }
        #endregion


        #region material
        private Material mMaterial;
        private void MaterialNameCb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.MaterialNameCb.SelectedIndex >= 0)
            {
                return;
            }
            String tempStr = this.MaterialNameCb.Text.Trim();
            if (String.IsNullOrEmpty(tempStr))
            {
                this.MaterialNameCb.SelectedIndex = -1;
            }
            //最少要输入一位字符
            if (tempStr.Length < 1)
            {              
                return;
            }
            List<Material> tempList = MaterialModel.IndistinctSearchByNameOrNameFirstCase(tempStr);
            this.MaterialNameCb.ItemsSource = tempList;
            if (this.MaterialNameCb.ItemsSource != null)
            {
                this.MaterialNameCb.IsDropDownOpen = true;
            }
        }

        private void MaterialNameCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.MaterialNameCb.SelectedIndex < 0)
            {
                mMaterial = null;
            }
            else
            {
                mMaterial = this.MaterialNameCb.SelectedItem as Material;
            }
        }
        #endregion


        #region Search btn click
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            this.SearchBtn.Cursor = Cursors.Wait;
            LoadData();
            this.SearchBtn.Cursor = Cursors.Hand;
        }

        #endregion

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
            Material material = (Material)e.Row.DataContext;

            if (material.currTaxation <= 0 )
            {
                e.Row.Foreground = Brushes.Red;
            }
        }


        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //TODO 查看调价记录
        }

        private void ResetTraeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (App.currentUser.roleLevel != (int)RoleLevelType.JGY) {
                MMessageBox.Result result = MMessageBox.GetInstance().ShowBox("非监管人员，无权限操作", "提示", MMessageBox.ButtonType.Yes, MMessageBox.IconType.Info, Orientation.Vertical, "好");
                return;
            }
            IconButton button = sender as IconButton;
            Material material = button.Tag as Material;
            new MaterialTaxationRecordWindow(material).ShowDialog();
            LoadData();
        }

        private void AddTabBtn_Click(object sender, RoutedEventArgs e)
        {
            new MaterailAddWindow().ShowDialog();
            LoadData();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            IconButton button = sender as IconButton;
            Material material = button.Tag as Material;
            new MaterailAddWindow(material).ShowDialog();
            LoadData();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            IconButton button = sender as IconButton;
            Material material = button.Tag as Material;
            material.isDelete = 1;
            material.deleteTime = DateTime.Now;
            material.lastUpdateTime = DateTime.Now;
            material.lastUpdateUserId = App.currentUser.id;
            material.lastUpdateUserName = App.currentUser.name;
            int res = DatabaseOPtionHelper.GetInstance().update(material);
            if (res > 0)
            {
                CommonFunction.ShowSuccessAlert("删除成功！");
                LoadData();
            }
            else {
                CommonFunction.ShowSuccessAlert("删除失败！");
            }
        }
    }
}

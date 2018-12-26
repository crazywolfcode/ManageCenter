using MyCustomControlLibrary;
using MyHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace ManageCenter
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class CarManageWindow : Window
    {       
        public CarManageWindow()
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
            List<CarInfo> list = CarInfoModel.FuzzySearch(this.CarNumberCb.Text);
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
            ExclHelper.ExclExprotToExcelWitchStatisticInfo(this.ReportDataGrid, "车辆信息"+DateTimeHelper.getCurrentDateTime(DateTimeHelper.DateFormat),"车辆信息","","","",null);             
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



        #region Car info
        private CarInfo carInfo;
        private void CarNumberCb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.CarNumberCb.SelectedIndex > -1)
            {
                return;
            }
            String tempStr = this.CarNumberCb.Text.Trim();
            if (String.IsNullOrEmpty(tempStr))
            {
                this.CarNumberCb.SelectedIndex = -1;
            }
            //最少要输入2位字符
            if (tempStr.Length < 2)
            {               
                return;
            }
            List<CarInfo> tempList = CarInfoModel.FuzzySearch(tempStr);
            this.CarNumberCb.ItemsSource = tempList;
            if (this.CarNumberCb.ItemsSource != null)
            {
                this.CarNumberCb.IsDropDownOpen = true;
            }
        }

        private void CarNumberCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.CarNumberCb.SelectedIndex == -1)
            {
                carInfo = null;
       
            }
            else
            {
                carInfo = this.CarNumberCb.SelectedItem as CarInfo;           
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
        }


        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //TODO
        }

        private void ResetTraeBtn_Click(object sender, RoutedEventArgs e)
        {
            MMessageBox.Result result = MMessageBox.GetInstance().ShowBox("车辆重置皮重，请使用原来的发卡软件操作", "提示", MMessageBox.ButtonType.Yes, MMessageBox.IconType.Info, Orientation.Vertical, "好");
            return;
        }
    }
}

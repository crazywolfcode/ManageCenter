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
    /// StationManageWondow.xaml 的交互逻辑
    /// </summary>
    public partial class StationManageWondow : Window
    {
        public StationManageWondow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            List<Station> list = StationModel.GetList() ;
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

        #region EXport EXCL，
        private void ExportExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            ExclHelper.ExclExprotToExcelWitchStatisticInfo(this.ReportDataGrid, "站占信息" + DateTimeHelper.getCurrentDateTime(DateTimeHelper.DateFormat), "站占信息", "", "", "", null);
        }
        #endregion


  

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void AddTabBtn_Click(object sender, RoutedEventArgs e)
        {
            new StationAddWindow().ShowDialog();
            this.LoadData();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            IconButton button = sender as IconButton;
            Station station = button.Tag as Station;
            station.lastUpdateTime = DateTime.Now;
            station.lastUpdateUserId = App.currentUser.id;
            station.lastUpdateUserName = App.currentUser.name;
            station.isDelete = 1;
            station.deleteTime = DateTime.Now;
            int res = DatabaseOPtionHelper.GetInstance().update(station);
            if (res > 0)
            {
                CommonFunction.ShowSuccessAlert("删除成功！");
                LoadData();
            }
            else {
                CommonFunction.ShowErrorAlert("删除失败！");
            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            IconButton button = sender as IconButton;
            Station station = button.Tag as Station;
            new StationAddWindow(station).ShowDialog();
            this.LoadData();
        }
    }
}

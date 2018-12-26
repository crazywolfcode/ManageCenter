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
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            this.StratDatePicker.Text = d1.ToString("yyyy年MM月dd日");
            this.EndDatePicker.Text = now.ToString("yyyy年MM月dd日");
            this.EndDatePicker.DisplayDateEnd = now;
            this.StratDatePicker.DisplayDateEnd = now;
            this.PrintTitleTb.Text = ConfigurationHelper.GetConfig(ConfigItemName.PrintTitle.ToString());
            this.StationNametb.Text = "(" + App.mStation.name + "-验票报表)";          
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            this.StationCb.ItemsSource = new List<Station>() { App.mStation };
            this.StationCb.SelectedIndex = 0;
            LoadData();
        }
        public void LoadData()
        {
            String condition = GetSeachCondition();
            List<WeighingBill> list = WeighingBillModel.searchData(condition);
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
            ExclHelper.ExclExprotToExcelWitchStatisticInfo(
                            this.ReportDataGrid,
                            DateTimeHelper.getCurrentDateTime(DateTimeHelper.DateFormat),
                            this.PrintTitleTb.Text,
                            this.StationNametb.Text,
                            this.StationNametb.Text + "导出时间:" + DateTimeHelper.getCurrentDateTime(),
                            this.InSummaryTextBlock.Text,
                            GetListStatisticToListString(this.InStatisticListBox)
                    );             
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

        #region Supply
        private Company sendCompany;
        private void SupplyCb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.SupplyCb.SelectedIndex >= 0)
            {
                return;
            }
            String tempStr = this.SupplyCb.Text.Trim();
            if (String.IsNullOrEmpty(tempStr))
            {
                this.SupplyCb.SelectedIndex = -1;
            }
            //最少要输入两位字符
            if (tempStr.Length < 2)
            {
                return;
            }
            List<Company> tempList = CompanyModel.IndistinctSearchByNameOrNameFirstCase(tempStr);
            this.SupplyCb.ItemsSource = tempList;
            if (this.SupplyCb.ItemsSource != null)
            {
                this.SupplyCb.IsDropDownOpen = true;
            }
        }

        private void SupplyCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.SupplyCb.SelectedIndex < 0)
            {
                sendCompany = null;
            }
            else
            {
                sendCompany = this.SupplyCb.SelectedItem as Company;
                if (checkSupplyCustomer() == false)
                {
                    MMessageBox.GetInstance().ShowBox("发货公司和收货公司不能是同一个！", "提示", MMessageBox.ButtonType.Yes, MMessageBox.IconType.Info, Orientation.Vertical);
                    //this.SupplyCb.Text = null;
                    this.SupplyCb.SelectedIndex = -1;
                    sendCompany = null;
                    return;
                }
            }
        }

        /// <summary>
        /// 供应商和客户是不能是同一个公司
        /// </summary>
        /// <returns></returns>
        private bool checkSupplyCustomer()
        {
            if (receiverCompany != null && sendCompany != null)
            {
                if (receiverCompany.id == sendCompany.id)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion supply

        #region Receiver Company
        private Company receiverCompany;
        private void ReceiverCompanyCb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.ReceiverCompanyCb.SelectedIndex > -1)
            {
                return;
            }
            String tempStr = this.ReceiverCompanyCb.Text.Trim();
            if (String.IsNullOrEmpty(tempStr))
            {
                this.ReceiverCompanyCb.SelectedIndex = -1;
            }
            //最少要输入两位字符
            if (tempStr.Length < 2)
            {
                return;
            }
            List<Company> tempList = CompanyModel.IndistinctSearchByNameOrNameFirstCase(tempStr);
            this.ReceiverCompanyCb.ItemsSource = tempList;
            if (this.ReceiverCompanyCb.ItemsSource != null)
            {
                this.ReceiverCompanyCb.IsDropDownOpen = true;
            }
        }

        private void ReceiverCompanyCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ReceiverCompanyCb.SelectedIndex == -1)
            {
                receiverCompany = null;
            }
            else
            {
                receiverCompany = this.ReceiverCompanyCb.SelectedItem as Company;
                if (checkSupplyCustomer() == false)
                {
                    MMessageBox.GetInstance().ShowBox("发货公司和收货公司不能是同一个！", "提示", MMessageBox.ButtonType.Yes, MMessageBox.IconType.Info, Orientation.Vertical);
                    //this.SupplyCb.Text = null;
                    this.ReceiverCompanyCb.SelectedIndex = -1;
                    receiverCompany = null;
                    return;
                }
            }
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
        private string GetSeachCondition()
        {
            string condition = string.Empty;
            condition = WeighingBillColumns.add_time.ToString() +">='"+ GetStartDate()+"'";
            condition = condition + " and " + WeighingBillColumns.add_time.ToString() + "<='" + GetEndDate() + "'";
            condition = condition + " and " + WeighingBillColumns.station_id.ToString() + "='" + mStation.id+ "'";
            if (sendCompany != null)
            {
                if (String.IsNullOrEmpty(condition))
                {
                    condition += WeighingBillColumns.send_company.ToString() + " = " + Constract.valueSplit + sendCompany.name + Constract.valueSplit;
                }
                else
                {
                    condition += " and " + WeighingBillColumns.send_company.ToString() + " = " + Constract.valueSplit + sendCompany.name + Constract.valueSplit;
                }
            }
            if (receiverCompany != null)
            {
                if (String.IsNullOrEmpty(condition))
                {
                    condition += WeighingBillColumns.receive_company.ToString() + " = " + Constract.valueSplit + receiverCompany.name + Constract.valueSplit;
                }
                else
                {
                    condition += " and " + WeighingBillColumns.receive_company.ToString() + " = " + Constract.valueSplit + receiverCompany.name + Constract.valueSplit;
                }
            }
            if (carInfo != null)
            {
                if (String.IsNullOrEmpty(condition))
                {
                    condition += WeighingBillColumns.car_number.ToString() + " = " + Constract.valueSplit + carInfo.carNumber + Constract.valueSplit;
                }
                else
                {
                    condition += " and " + WeighingBillColumns.car_number.ToString() + " = " + Constract.valueSplit + carInfo.carNumber + Constract.valueSplit;
                }
            }
            if (mMaterial != null)
            {
                if (String.IsNullOrEmpty(condition))
                {
                    condition += WeighingBillColumns.material_name.ToString() + " = " + Constract.valueSplit + mMaterial.name + Constract.valueSplit;
                }
                else
                {
                    condition += " and " + WeighingBillColumns.material_name.ToString() + " = " + Constract.valueSplit + mMaterial.name + Constract.valueSplit;
                }
            }
            switch (this.StatusCb.SelectedIndex) {
                case 0:
                    condition += " and " + WeighingBillColumns.is_checked.ToString() + " = " +0;
                    break;
                case 1:
                    condition += " and " + WeighingBillColumns.is_checked.ToString() + " = " +1;
                    break;
                case 2:
                    condition += " and " + WeighingBillColumns.is_receive_money.ToString() + " = " +0;
                    break;
                case 3:
                    condition += " and " + WeighingBillColumns.is_receive_money.ToString() + " = " + 1;
                    break;
                case 4:
                    condition += " and " + WeighingBillColumns.is_receive_money.ToString() + " = " + 2;
                    break;                   
            }
            return condition;
        }
        private string GetStartDate()
        {
            if (StratDatePicker.SelectedDate == null)
            {
                return null;
            }
            DateTime tempdate = (DateTime)StratDatePicker.SelectedDate;
            if (tempdate == null)
            {
                return null;
            }
            string res = tempdate.ToString(Constract.defaultDateTimeFormat);
            return res;
        }
        private string GetEndDate()
        {
            if (EndDatePicker.SelectedDate == null)
            {
                return null;
            }
            DateTime tempdate = (DateTime)EndDatePicker.SelectedDate;
            if (tempdate == null)
            {
                return null;
            }
            string endtime = " 23:59:59";
            string res = tempdate.ToString(Constract.DateFormat) + endtime;
            return res;
        }
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
            WeighingBill bill = (WeighingBill)e.Row.DataContext;

            if (bill.isReceiveMoney == 0 && bill.overtopMoney > 0)
            {
                e.Row.Foreground = Brushes.Red;
            }
            else if (bill.isReceiveMoney == 1)
            {
                e.Row.Foreground = Brushes.Green;
            }
            else
            {
                e.Row.Foreground = Brushes.Black;
            }
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //TODO
        }

        private void PrintSelectBtn_Click(object sender, RoutedEventArgs e)
        {
            WeighingBill bill = (WeighingBill)this.ReportDataGrid.SelectedItem;
            if (bill == null)
            {
                return;
            }
            new PrintBillW(bill).ShowDialog();
        }
        private Station mStation;
        private void StationCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mStation =(Station) StationCb.SelectedItem;
        }
    }
}

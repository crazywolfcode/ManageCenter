using MyCustomControlLibrary;
using MyHelper;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ManageCenter
{
    /// <summary>
    /// CameraAddW.xaml 的交互逻辑
    ///  CameraAddW.xaml's interactive logical 
    /// </summary>
    public partial class PrintBillW : Window
    {
        #region Variable        
        public Action RefreshData;
        private WeighingBill mWeighingBill;
        private int OutPrintSecend = 0;
        private bool isOPtionSuccess = false;
        private bool isAutoPrint = false;
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;

        #endregion
        public PrintBillW(WeighingBill bill)
        {
            InitializeComponent();
            mWeighingBill = bill;
            string auto = ConfigurationHelper.GetConfig(ConfigItemName.autoPrint.ToString());
            OutPrintSecend = Convert.ToInt32(ConfigurationHelper.GetConfig(ConfigItemName.autoPrintSecend.ToString()));
            if (auto.Equals("1"))
            {
                isAutoPrint = true;
            }
            if (mWeighingBill == null)
            {
                this.Close();
            }
        }
        private Point mPoit = new Point(0, 0);
        private Size mSize = new Size(0, 0);
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.PrintTitleTb.Text = ConfigurationHelper.GetConfig(ConfigItemName.PrintTitle.ToString());
            this.StationNametb.Text = "(" + App.mStation.name + ")";
            this.InGrid.DataContext = mWeighingBill;
            generaterQrCode();
            mPoit = this.InPanel.PointToScreen(new Point());
            mSize = new Size(this.InPanel.ActualWidth, this.InPanel.ActualHeight);

            if (mWeighingBill.isReceiveMoney == 0 && mWeighingBill.overtopMoney > 0)
            {
                this.SetNomalBtn.Visibility = Visibility.Visible;
            }

        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

        }

        private void generaterQrCode()
        {
            var bitmap = MyHelper.QrCode.QrCodeHelper.GenerateQrCode(mWeighingBill.number, 100, 100);
            this.INQrCodeImage.Source = BitmapHelper.BitmapToBitmapImage(bitmap);
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
            if (dispatcherTimer != null)
            {
                dispatcherTimer.Stop();
            }
            if (isOPtionSuccess == true)
            {
                if (this.RefreshData != null)
                {
                    this.RefreshData();
                }
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
        #endregion

        private void LookPicBtn_Click(object sender, RoutedEventArgs e)
        {
            new PicWindow(mWeighingBill).ShowDialog();
        }

        private void SetNomalBtn_Click(object sender, RoutedEventArgs e)
        {
            if (App.currentUser.roleLevel != (int)RoleLevelType.JGY)
            {
                CommonFunction.ShowAlert("无权操作！");
                return;
            }
            MMessageBox.Result result = MMessageBox.GetInstance().ShowBox($"该车辆需要补交税费 {mWeighingBill.overtopMoney} 元，是否确认设置？", "提示", MMessageBox.ButtonType.YesNo, MMessageBox.IconType.Info, Orientation.Vertical, "是");
            if (result == MMessageBox.Result.Yes)
            {
                mWeighingBill.isReceiveMoney = 1;
                mWeighingBill.overtopMoney = 0;
                mWeighingBill.lastUpdateUserId = App.currentUser.id;
                mWeighingBill.lastUpdateUserName = App.currentUser.name;
                mWeighingBill.remark = mWeighingBill.remark + $"  被 {App.currentUser.name} 置为正常。";
                int res = DatabaseOPtionHelper.GetInstance().update(mWeighingBill);
                if (res > 0)
                {
                    CommonFunction.ShowSuccessAlert("设置成功");
                    this.InGrid.DataContext = null;
                    this.InGrid.DataContext = mWeighingBill;
                    this.SetNomalBtn.Visibility = Visibility.Collapsed;
                }
                else
                {
                    CommonFunction.ShowErrorAlert("设置失败");
                }
            }
        }


    }
}

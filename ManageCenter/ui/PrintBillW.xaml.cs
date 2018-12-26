using MyCustomControlLibrary;
using MyHelper;
using System;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            if (auto.Equals("1")) {
                isAutoPrint = true;
            }           
            if (mWeighingBill == null) {
                this.Close();
            }           
        }
        private Point mPoit = new Point(0,0);
        private Size mSize =new Size(0,0);
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.PrintTitleTb.Text = ConfigurationHelper.GetConfig(ConfigItemName.PrintTitle.ToString());
            this.StationNametb.Text = "("+App.mStation.name+")";
            this.InGrid.DataContext = mWeighingBill;
            generaterQrCode();
             mPoit = this.InPanel.PointToScreen(new Point());
            mSize = new Size(this.InPanel.ActualWidth, this.InPanel.ActualHeight);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (isAutoPrint == true)
            {
                dispatcherTimer = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromSeconds(OutPrintSecend) };
                dispatcherTimer.Tick += DispatcherTimer_Tick;
                dispatcherTimer.Start();
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (isClickPrint)
            {
                dispatcherTimer.Stop();
                return;
            }
            OutPrintSecend -= 1;
            this.PrintBtn.Content = OutPrintSecend + "s 打印";
            if (OutPrintSecend <= 0) {
                this.PrintBtn.Content =  "打印中...";
                MMessageBox.GetInstance().ShowLoading(MMessageBox.LoadType.Three, "打印中...", mPoit, mSize, "&#xe752;", Orientation.Vertical, "#ffffff", 4);
                dispatcherTimer.Stop();
               Print();                
            }
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
            if (dispatcherTimer != null) {
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

        private bool isClickPrint = false;
        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            isClickPrint = true;
            Print();
            this.PrintBtn.IsEnabled = true;
        }

        public void Print()
        {
            this.PrintBtn.Content = "打印...";
            this.PrintBtn.IsEnabled = false;
            this.Cursor = Cursors.Wait;
            if (isAutoPrint == false && isClickPrint == false)
            {
                return;
            }
            try
            {
                LocalPrintServer printServer = new LocalPrintServer();
                PrintQueue printQueue = printServer.DefaultPrintQueue;
                if (!printServer.ConnectToPrintQueue(printQueue)) {
                    CommonFunction.ShowAlert("打印机不可用");
                    this.Close();
                }
                if (printQueue == null || printQueue.IsOffline == true)
                {
                    throw new Exception("打印机不可用！");
                }
                string description = "磅单打印";
                PrintDialog printDialog = new PrintDialog
                {
                    CurrentPageEnabled = true,
                    PrintQueue = printQueue,
                    PageRange = new PageRange(1)
                };
                
                Panel PrintArea = this.InPanel;
                printDialog.PrintVisual(PrintArea, description);
               PrintSystemJobInfo jobInfo = printQueue.GetJob(printQueue.NumberOfJobs);
                if (jobInfo.IsCompleted) {
                    WeighingBill bill = new WeighingBill()
                    {
                        id = mWeighingBill.id,
                        number = mWeighingBill.number,
                        printFrequency = mWeighingBill.printFrequency + 1,
                        printTime = DateTime.Now
                    };
                    DatabaseOPtionHelper.GetInstance().update(bill);
                }                                
            }
            catch (Exception e)
            {
                ConsoleHelper.writeLine($"Pint {mWeighingBill.id} failed:{e.Message}");
                // MMessageBox.GetInstance().ShowBox(e.Message,"打印错错误", MMessageBox.ButtonType.Yes, MMessageBox.IconType.error, Orientation.Vertical);         
                App.ShowBalloonTip("打印错误", e.Message);
                this.Close();
            }
            finally {
                this.Close();
                if (dispatcherTimer != null) {
                    dispatcherTimer.Stop();
                }
            }
        }
    }
}

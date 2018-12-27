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
    /// PicWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PicWindow : Window
    {
        private WeighingBill mWeighingBill;
        List<BillImage> images;
        public PicWindow( WeighingBill bill)
        {
            InitializeComponent();
            if (bill == null) {
                this.Close();
            }
            mWeighingBill = bill;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = mWeighingBill;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            images = BillIamgeModel.GetListByBillNumber(mWeighingBill.number);
            this.imagePanel.Children.Clear();
            if (images.Count <= 0) {
                TextBlock text = new TextBlock() { Text = "该磅单没有截图", Foreground = Brushes.Red ,HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(10) };
                this.imagePanel.Children.Add(text);
                return;
            }
            this.imagePanel.Children.Clear();
            for (int i = 0; i < images.Count; i++)
            {
                Image image = new Image() { Stretch = Stretch.Fill,Source =CommonFunction.getImageSource(images[i].address),Margin = new Thickness(0,2,0,2)};
                this.imagePanel.Children.Add(image);
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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }



        #endregion

  
    }
}

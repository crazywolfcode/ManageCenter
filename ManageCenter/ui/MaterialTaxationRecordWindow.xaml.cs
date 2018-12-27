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
    public partial class MaterialTaxationRecordWindow : Window
    {
        private Material mMaterial;

        public MaterialTaxationRecordWindow(Material material)
        {
            InitializeComponent();
            if (material == null) {
                this.Close();
            }
            mMaterial = material;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MTitle.Text = mMaterial.name + " 调价记录";
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            List<MaterialTaxationRecod> list = MaterialTaxationRecordModel.GetList(mMaterial.id);
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
            ExclHelper.ExclExprotToExcelWitchStatisticInfo(this.ReportDataGrid, "物料调价记录" + DateTimeHelper.getCurrentDateTime(DateTimeHelper.DateFormat), "物料调价记录", "", "", "", null);
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


    
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;              
        }
        
        private void AddTabBtn_Click(object sender, RoutedEventArgs e)
        {
            new MaterialPriceWindow(mMaterial).ShowDialog();
            this.LoadData();
        }
    }
}

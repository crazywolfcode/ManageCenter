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
    public partial class MaterialPriceWindow : Window
    {


        private Material mMaterial;
        public MaterialPriceWindow(Material m )
        {
            InitializeComponent();
            this.mMaterial = m;                   
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mMaterial == null)
            {
                this.Close();
            }
            this.Title = mMaterial.name + " 煤种调价";
            this.BodyTitle.Text = mMaterial.name + " 煤种调价";
            this.priceTB.Text = "当前价格：￥" + mMaterial.currTaxation + " 元/t";
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

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string str = this.targeetPriceTb.Text.Trim();
            double d = 0;
            if (string.IsNullOrEmpty(str))
            {
                CommonFunction.ShowAlert("目标价格不能为空");
                return;
            }

            try
            {
                d = Convert.ToDouble(str);
            }
            catch
            {
                CommonFunction.ShowAlert("输入目标价格不正确");
                return;
            }
            mMaterial.currTaxation = d;
            mMaterial.lastUpdateUserId = App.currentUser.id;
            mMaterial.lastUpdateUserName = App.currentUser.name;

            MaterialTaxationRecod record = new MaterialTaxationRecod()
            {
                id = Guid.NewGuid().ToString(),
                addUserId = App.currentUser.id,
                addUserName = App.currentUser.name,
                status = 1,
                operatorId = App.currentUser.id,
                operatorName = App.currentUser.name,
                materialId = mMaterial.id,
                materialName = mMaterial.name,
                materialTaxation = mMaterial.currTaxation,
                remark = this.reamrkTb.Text.Trim()
            };

            String updateSql = DatabaseOPtionHelper.GetInstance().getUpdateSql(mMaterial);
            String insertSql = DatabaseOPtionHelper.GetInstance().getInsertSql(record);

            int res = DatabaseOPtionHelper.GetInstance().TransactionExecute(new string[] { updateSql,insertSql});
            if (res > 0) {
                CommonFunction.ShowSuccessAlert("调价成功");
                this.Close();
            } else {
                CommonFunction.ShowErrorAlert("调价失败");
            }
        }


    }
}

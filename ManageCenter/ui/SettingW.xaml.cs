using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MyHelper;

namespace ManageCenter
{
    /// <summary>
    /// SettingW.xaml 的交互逻辑
    /// </summary>
    public partial class SettingW : Window
    {

        private int selectType;

        public SettingW(int type = 0)
        {
            InitializeComponent();
            selectType = type;
        }

        public void InitializingEvent()
        {

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (this.IsLoaded)
            {

            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            switch (selectType)
            {
                case 0:
                    BaseSettingPanel.Visibility = Visibility.Visible;
                    this.PrintSetting.IsChecked = true;
                    break;
                case 1:
                    HeightSettingPanel.Visibility = Visibility.Visible;
                    this.limitSettingRB.IsChecked = true;
                    FindCom();
                    break;
                case 2:
                    systemSettingPanel.Visibility = Visibility.Visible;
                    softexpriceRB.IsChecked = true;
                    break;
                default:
                    break;
            }
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {


        }

        private void FindCom()
        {
            int count = System.IO.Ports.SerialPort.GetPortNames().Count();
            if (IcreaderSettingRB.IsChecked == true)
            {
                this.ICComCb.ItemsSource = System.IO.Ports.SerialPort.GetPortNames();
            }
            else
            {
                this.ComCb.ItemsSource = System.IO.Ports.SerialPort.GetPortNames();
            }
            if (count > 0)
            {
                if (IcreaderSettingRB.IsChecked == true)
                {
                    ICComAlertLabel.Content = count + " 个串口可用";
                    this.ICComCb.SelectedIndex = 0;
                }
                else
                {
                    ComAlertLabel.Content = count + " 个串口可用";
                    this.ComCb.SelectedIndex = 0;
                }
            }
            else
            {
                ComAlertLabel.Content = "没有可以的串口";
                ICComAlertLabel.Content = count + " 没有可以的串口";
            }
        }

        private void SwitchSelectSettingItem()
        {


        }

        #region window event
        private void WindowTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        protected void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConfigItemPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        #endregion

        
        #region Print Setting 打印
        private void PrintSetting_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded == false)
            {
                return;
            }
            if ("1" == ConfigurationHelper.GetConfig(ConfigItemName.autoPrint.ToString()))
            {
                this.StartAautoPtint.IsChecked = true;
            }
            else
            {
                this.StartAautoPtint.IsChecked = false;
            }
            this.PrintTimes.Text = ConfigurationHelper.GetConfig(ConfigItemName.defaultPrintFrequency.ToString());
            this.autoPrintSecend.Text = ConfigurationHelper.GetConfig(ConfigItemName.autoPrintSecend.ToString());
        }

        private void StartAautoPtint_Checked(object sender, RoutedEventArgs e)
        {
            String set = ConfigColumns.config_value.ToString() + " = '" + 1 + "'";
            String con = ConfigColumns.config_name.ToString() + " = '" + ConfigItemName.autoPrint.ToString() + "'";
            string Sql = DatabaseOPtionHelper.GetInstance().getUpdateSql(TableName.config.ToString(), set, con);
            int res = DatabaseOPtionHelper.GetInstance().update(Sql);
            if (res > 0)
            {
                //CommonFunction.ShowSuccessAlert("保存成功");
                ConfigurationHelper.SetConfig(ConfigItemName.autoPrint.ToString(), "1");
            }
            else
            {
                CommonFunction.ShowErrorAlert("保存失败");
            }
        }

        private void StartAautoPtint_Unchecked(object sender, RoutedEventArgs e)
        {
            String set = ConfigColumns.config_value.ToString() + " = '" + 0 + "'";
            String con = ConfigColumns.config_name.ToString() + " = '" + ConfigItemName.autoPrint.ToString() + "'";
            string Sql = DatabaseOPtionHelper.GetInstance().getUpdateSql(TableName.config.ToString(), set, con);
            int res = DatabaseOPtionHelper.GetInstance().update(Sql);
            if (res > 0)
            {
                //CommonFunction.ShowSuccessAlert("保存成功");
                ConfigurationHelper.SetConfig(ConfigItemName.autoPrint.ToString(), "0");
            }
            else
            {
                CommonFunction.ShowErrorAlert("保存失败");
            }
        }

        private void PrintTimes_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                if (this.PrintTimes.Text.Length > 0 && this.PrintTimes.Text != ConfigurationHelper.GetConfig(ConfigItemName.defaultPrintFrequency.ToString()))
                {
                    if (RegexHelper.IsNumber(this.PrintTimes.Text))
                    {
                        int times = Convert.ToInt32(this.PrintTimes.Text);
                      
                        String set = ConfigColumns.config_value.ToString() + " = '" + times+"'";
                        String con = ConfigColumns.config_name.ToString() + " = '" + ConfigItemName.defaultPrintFrequency.ToString() + "'";
                        string Sql = DatabaseOPtionHelper.GetInstance().getUpdateSql(TableName.config.ToString(), set, con);
                        int res = DatabaseOPtionHelper.GetInstance().update(Sql);
                        if (res > 0)
                        {
                            CommonFunction.ShowSuccessAlert("保存成功");
                           ConfigurationHelper.SetConfig(ConfigItemName.defaultPrintFrequency.ToString(), times.ToString());
                        }
                        else
                        {
                            CommonFunction.ShowErrorAlert("保存失败");
                        }
                       
                    }
                    else
                    {
                        CommonFunction.ShowErrorAlert("输入的打印次数必须为整数！");
                    }
                }
            }
        }
        private void autoPrintSecend_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                if (this.autoPrintSecend.Text.Length > 0 && this.autoPrintSecend.Text != ConfigurationHelper.GetConfig(ConfigItemName.autoPrintSecend.ToString()))
                {
                    if (RegexHelper.IsNumber(this.PrintTimes.Text))
                    {
                        int times = Convert.ToInt32(this.PrintTimes.Text);

                        String set = ConfigColumns.config_value.ToString() + " = '" + times + "'";
                        String con = ConfigColumns.config_name.ToString() + " = '" + ConfigItemName.autoPrintSecend.ToString() + "'";
                        string Sql = DatabaseOPtionHelper.GetInstance().getUpdateSql(TableName.config.ToString(), set, con);
                        int res = DatabaseOPtionHelper.GetInstance().update(Sql);
                        if (res > 0)
                        {
                            CommonFunction.ShowSuccessAlert("保存成功");
                            ConfigurationHelper.SetConfig(ConfigItemName.autoPrintSecend.ToString(), times.ToString());
                        }
                        else
                        {
                            CommonFunction.ShowErrorAlert("保存失败");
                        }
                    }
                    else
                    {
                        CommonFunction.ShowErrorAlert("输入的内容必须为整数！");
                    }
                }
            }
        }
        private void BillTitle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String str = this.billTitlb.Text.Trim();
                if (str.Length <= 0) {
                    return;
                }
                String set = ConfigColumns.config_value.ToString() + " = '" + str + "'";
                String con = ConfigColumns.config_name.ToString() + " = '" + ConfigItemName.PrintTitle.ToString() + "'";
                string Sql = DatabaseOPtionHelper.GetInstance().getUpdateSql(TableName.config.ToString(), set, con);
                int res = DatabaseOPtionHelper.GetInstance().update(Sql);
                if (res > 0)
                {
                    CommonFunction.ShowSuccessAlert("保存成功");
                    ConfigurationHelper.SetConfig(ConfigItemName.PrintTitle.ToString(), str);
                }
                else
                {
                    CommonFunction.ShowErrorAlert("保存失败");
                }
            
            }
            catch
            {
                CommonFunction.ShowErrorAlert("保存失败！");
            }
        }

        private void BillDes_Click(object sender, RoutedEventArgs e)
        {
            try
            {    
                String str = this.billTitlb.Text.Trim();
                if (str.Length <= 0)
                {
                    return;
                }
                String set = ConfigColumns.config_value.ToString() + " = '" + str + "'";
                String con = ConfigColumns.config_name.ToString() + " = '" + ConfigItemName.BillDescription.ToString() + "'";
                string Sql = DatabaseOPtionHelper.GetInstance().getUpdateSql(TableName.config.ToString(), set, con);
                int res = DatabaseOPtionHelper.GetInstance().update(Sql);
                if (res > 0)
                {
                    CommonFunction.ShowSuccessAlert("保存成功");
                    ConfigurationHelper.SetConfig(ConfigItemName.BillDescription.ToString(), str);
                }
                else
                {
                    CommonFunction.ShowErrorAlert("保存失败");
                }
            }
            catch
            {
                CommonFunction.ShowErrorAlert("保存失败！");
            }
        }
        #endregion

        private void limitWeightSetting_Checked(object sender, RoutedEventArgs e)
        {

        }

        #region 合理磅差

        private void limitSettingRB_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded == false)
            {
                return;
            }
            if ("1" == ConfigurationHelper.GetConfig(ConfigItemName.IsUnifeidLimitTone.ToString()))
            {
                this.UnifeidLimitToneRB.IsChecked = true;
            }
            else
            {
                this.UnifeidLimitToneRB.IsChecked = false;
            }
            this.limitToneTB.Text = ConfigurationHelper.GetConfig(ConfigItemName.limitTone.ToString());
        }

        private void UnifeidLimitToneRB_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded == false) return;
            String set = ConfigColumns.config_value.ToString() + " = '" + 1 + "'";
            String con = ConfigColumns.config_name.ToString() + " = '" + ConfigItemName.IsUnifeidLimitTone.ToString() + "'";
            string Sql = DatabaseOPtionHelper.GetInstance().getUpdateSql(TableName.config.ToString(), set, con);
            int res = DatabaseOPtionHelper.GetInstance().update(Sql);
            if (res > 0)
            {
                //CommonFunction.ShowSuccessAlert("保存成功");
                ConfigurationHelper.SetConfig(ConfigItemName.IsUnifeidLimitTone.ToString(), "1");
            }
            else
            {
                CommonFunction.ShowErrorAlert("保存失败");
            }            
        }

        private void UnifeidLimitToneRB_Unchecked(object sender, RoutedEventArgs e)
        {
            String set = ConfigColumns.config_value.ToString() + " = '" + 0 + "'";
            String con = ConfigColumns.config_name.ToString() + " = '" + ConfigItemName.IsUnifeidLimitTone.ToString() + "'";
            string Sql = DatabaseOPtionHelper.GetInstance().getUpdateSql(TableName.config.ToString(), set, con);
            int res = DatabaseOPtionHelper.GetInstance().update(Sql);
            if (res > 0)
            {
                //CommonFunction.ShowSuccessAlert("保存成功");
                ConfigurationHelper.SetConfig(ConfigItemName.IsUnifeidLimitTone.ToString(), "0");
            }
            else
            {
                CommonFunction.ShowErrorAlert("保存失败");
            }
        }
        #endregion


        #region 磅称显示控制器
        private void scaleSettingRB_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded == false)
            {
                return;
            }
        }
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                CommonFunction.ShowSuccessAlert("保存成功");
            }
            catch
            {
                CommonFunction.ShowErrorAlert("保存失败！");
            }

        }
        #endregion

        #region IC卡
        private void IcreaderSettingRB_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded == false)
            {
                return;
            }
            FindCom();
            String com = ConfigurationHelper.GetConfig(ConfigItemName.IcCom.ToString());
            for (int i = 0; i < ICComCb.Items.Count; i++)
            {
                String item = ComCb.Items[i].ToString();
                if (item.Equals(com))
                {
                    ICComCb.SelectedIndex = i;
                    break;
                }
            }
            this.ICBaudRateCB.Text = ConfigurationHelper.GetConfig(ConfigItemName.IcBaudRate.ToString());
        }
        private void saveICBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String com = this.ICComCb.Text.Trim();
                String buadrate = this.ICBaudRateCB.Text.Trim();
                ConfigurationHelper.SetConfig(ConfigItemName.IcCom.ToString(), com);
                ConfigurationHelper.SetConfig(ConfigItemName.IcBaudRate.ToString(), buadrate);
                CommonFunction.ShowSuccessAlert("保存成功");
            }
            catch
            {
                CommonFunction.ShowErrorAlert("保存失败！");
            }
        }


        #endregion

        private void IconButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.currentUser.roleLevel == (int)RoleLevelType.XTZZ)
            {
                String str = this.softExpriceTB.Text;
                DateTime end;
                try
                {
                    end = DateTime.Parse(str);
                }
                catch
                {
                    end = DateTime.Parse(DateTimeHelper.getAfterAddDayDateTime(365));
                }
                long endlong = DateTimeHelper.GetTimeStamp(end);

                String set = ConfigColumns.config_value.ToString() + " = '" + endlong + "'" ;
                String con = ConfigColumns.config_name.ToString() + " = '" + ConfigItemName.SoftEndDate.ToString() + "'";
                string Sql = DatabaseOPtionHelper.GetInstance().getUpdateSql(TableName.config.ToString(), set, con);
                int res = DatabaseOPtionHelper.GetInstance().update(Sql);
                if (res > 0)
                {
                    CommonFunction.ShowSuccessAlert("保存成功");
                    ConfigurationHelper.SetConfig(ConfigItemName.SoftEndDate.ToString(), endlong.ToString());
                }
                else
                {
                    CommonFunction.ShowErrorAlert("保存失败");
                }
                ConfigurationHelper.SetConfig(ConfigItemName.SoftEndDate.ToString(), endlong.ToString());
            }
        }

        private void limitBtn_Click(object sender, RoutedEventArgs e)
        {
            string str = this.limitToneTB.Text.Trim();
            if (string.IsNullOrEmpty(str))
            {
                CommonFunction.ShowErrorAlert("磅差值不能为空，可以是0");
                return;
            }
            try
            {
                double d = Convert.ToDouble(str);
                String set = ConfigColumns.config_value.ToString() + " = '" + d+"'";
                String con = ConfigColumns.config_name.ToString() + " = '" + ConfigItemName.limitTone.ToString() + "'";
                string Sql = DatabaseOPtionHelper.GetInstance().getUpdateSql(TableName.config.ToString(), set, con);
                int res = DatabaseOPtionHelper.GetInstance().update(Sql);
                if (res > 0)
                {
                    CommonFunction.ShowSuccessAlert("保存成功");
                    ConfigurationHelper.SetConfig(ConfigItemName.limitTone.ToString(), d.ToString());
                }
                else
                {
                    CommonFunction.ShowErrorAlert("保存失败");
                }
            }
            catch
            {
                CommonFunction.ShowErrorAlert("保存失败");
            }
        }

        private void DataAsyncRB_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded == false)
            {
                return;
            }
            if ("1" == ConfigurationHelper.GetConfig(ConfigItemName.isOpenListence.ToString()))
            {
                this.DataSsyncSRB.IsChecked = true;
            }
            else
            {
                this.DataSsyncSRB.IsChecked = false;
            }
        }

        private void DataSsyncSRB_Checked(object sender, RoutedEventArgs e)
        {
            ConfigurationHelper.SetConfig(ConfigItemName.isOpenListence.ToString(), "1");
        }

        private void DataSsyncSRB_Unchecked(object sender, RoutedEventArgs e)
        {
            ConfigurationHelper.SetConfig(ConfigItemName.isOpenListence.ToString(), "0");
        }
    }
}

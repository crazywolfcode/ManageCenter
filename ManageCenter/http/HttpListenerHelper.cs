using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
    public class HttpListenerHelper
    {
        private static HttpListener listener;
        private static string successStr = "成功";
        private static string saveErrStr = "保存错误 ";
        private static string contentErrStr = "上传的内容错误 ";
        private static string Nodata = "没有数据 ";
        private static string NoImageData = "没有图片数据 ";
        private static string ParamErr = "参数错误！";
        private const string ImageTableName = "image";
        /// <summary>
        /// 开启监听 
        /// </summary>
        /// <param name="url"> 本机的url 地址带端口 如：http://+:8080/</param>
        public static void Start(String url)
        {
            StationModel.GetList();
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            while (true)
             {
                
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response; 
                response.StatusCode = 200;
                var qs = request.QueryString;
                String tableName = qs.Get("table");
                String stationId = String.Empty;
                String time = string.Empty;
                string fileName = string.Empty;
                try
                {
                    time = qs.Get("time");
                    stationId = qs.Get("stationid");
                    fileName = qs.Get("filename");
                }
                catch { }
                string data = string.Empty;
                using (StreamReader reader = new StreamReader(request.InputStream, Encoding.GetEncoding("GB2312")))
                {
                    if (reader.Peek() > -1)
                    {
                        data += reader.ReadToEnd();
                    }
                }
                NetResult result;
                if (!string.IsNullOrEmpty(fileName))
                {                 
                    result = ProcessImageUp(tableName, fileName, data);
                }
                else if (String.IsNullOrEmpty(data))
                {
                    result = ProcessPull(tableName, time, stationId);
                }
                else
                {
                    result = ProcessPush(tableName, data);
                }

                String res = MyHelper.JsonHelper.ObjectToJson(result);
                using (System.IO.StreamWriter write = new System.IO.StreamWriter(response.OutputStream, Encoding.GetEncoding("GB2312")))
                {
                    //把处理信息返回到客户端
                    try
                    {
                        write.Write(res);
                    }
                    catch { }
                }

            }
        }
        /// <summary>
        /// 处理推送上来的数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static NetResult ProcessPush(String tableName, String Data)
        {
            NetResult result = new NetResult() { errCode = 1, msg = "服务器未能处的请求", Data = "" };
            if (String.IsNullOrEmpty(tableName))
            {
                result.msg = "Table 参数是必须的";
                return result;
            }
            if (String.IsNullOrEmpty(Data))
            {
                result.msg = "上传的数据为空";
                return result;
            }
            switch (tableName)
            {
                case "bill_image":
                    try
                    {
                        BillImage bill = (BillImage)MyHelper.JsonHelper.JsonToObject(Data, typeof(BillImage));                      
                        bill.address.Replace("\\", "\\\\");
                        int res = 0;
                        res = DatabaseOPtionHelper.GetInstance().insertOrUpdate(bill);
                        if (res > 0)
                        {
                            result.errCode = 0;
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 1;
                            result.msg = saveErrStr;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = contentErrStr;
                    }
                    break;
                case "bill_taxation_money_record":
                    try
                    {
                        BillTaxationMoneyRecord data = (BillTaxationMoneyRecord)MyHelper.JsonHelper.JsonToObject(Data, typeof(BillTaxationMoneyRecord));
                        int res = 0;
                        res = DatabaseOPtionHelper.GetInstance().insertOrUpdate(data);
                        if (res > 0)
                        {
                            result.errCode = 0;
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 1;
                            result.msg = saveErrStr;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = contentErrStr;
                    }
                    break;
                case "car_info":
                    try
                    {
                        CarInfo bill = (CarInfo)MyHelper.JsonHelper.JsonToObject(Data, typeof(CarInfo));
                        int res = 0;
                        res = DatabaseOPtionHelper.GetInstance().insertOrUpdate(bill);
                        if (res > 0)
                        {
                            result.errCode = 0;
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 1;
                            result.msg = saveErrStr;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = contentErrStr;
                    }
                    break;
                case "camera_info":
                    try
                    {
                        CameraInfo bill = (CameraInfo)MyHelper.JsonHelper.JsonToObject(Data, typeof(CameraInfo));
                        int res = 0;
                        res = DatabaseOPtionHelper.GetInstance().insertOrUpdate(bill);
                        if (res > 0)
                        {
                            result.errCode = 0;
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 1;
                            result.msg = saveErrStr;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = contentErrStr;
                    }
                    break;
                case "car_trae_recod":
                    try
                    {
                        CarTraeRecod bill = (CarTraeRecod)MyHelper.JsonHelper.JsonToObject(Data, typeof(CarTraeRecod));
                        int res = 0;
                        res = DatabaseOPtionHelper.GetInstance().insertOrUpdate(bill);
                        if (res > 0)
                        {
                            result.errCode = 0;
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 1;
                            result.msg = saveErrStr;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = contentErrStr;
                    }
                    break;
                case "company":
                    try
                    {
                        Company data = (Company)MyHelper.JsonHelper.JsonToObject(Data, typeof(Company));
                        int res = 0;
                        res = DatabaseOPtionHelper.GetInstance().insertOrUpdate(data);
                        if (res > 0)
                        {
                            result.errCode = 0;
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 1;
                            result.msg = saveErrStr;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = contentErrStr;
                    }
                    break;
                case "config":
                    try
                    {
                        Config data = (Config)MyHelper.JsonHelper.JsonToObject(Data, typeof(Config));
                        int res = 0;
                        res = DatabaseOPtionHelper.GetInstance().insertOrUpdate(data);
                        if (res > 0)
                        {
                            result.errCode = 0;
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 1;
                            result.msg = saveErrStr;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = contentErrStr;
                    }
                    break;
                case "material":
                    try
                    {
                        Material data = (Material)MyHelper.JsonHelper.JsonToObject(Data, typeof(Material));
                        int res = 0;
                        res = DatabaseOPtionHelper.GetInstance().insertOrUpdate(data);
                        if (res > 0)
                        {
                            result.errCode = 0;
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 1;
                            result.msg = saveErrStr;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = contentErrStr;
                    }
                    break;
                case "material_category":
                    break;
                case "material_taxation_recod":
                    break;
                case "roles":
                    break;
                case "station":
                    break;
                case "user":
                    try
                    {
                        User data = (User)MyHelper.JsonHelper.JsonToObject(Data, typeof(User));
                        int res = 0;
                        res = DatabaseOPtionHelper.GetInstance().insertOrUpdate(data);
                        if (res > 0)
                        {
                            result.errCode = 0;
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 1;
                            result.msg = saveErrStr;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = contentErrStr;
                    }
                    break;
                case "weighing_bill":
                    try
                    {
                        WeighingBill bill = (WeighingBill)MyHelper.JsonHelper.JsonToObject(Data, typeof(WeighingBill));
                        int res = DatabaseOPtionHelper.GetInstance().insertOrUpdate(bill);
                        if (res > 0)
                        {
                            result.errCode = 0;
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 1;
                            result.msg = saveErrStr;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = contentErrStr;
                    }
                    break;
                default:
                    break;
            }
            return result;
        }
        /// <summary>
        /// 接受 处理图片上传
        /// </summary>
        private static NetResult ProcessImageUp(string tableName, string filename, String data)
        {
            NetResult result = new NetResult() { errCode = 1, msg = "服务器未能处请求", Data = "" };
            Console.WriteLine("==有图片在上传" );
            if (String.IsNullOrEmpty(tableName) || !tableName.Equals(ImageTableName))
            {
                result.msg = ParamErr;
            }
            else if (string.IsNullOrEmpty(data))
            {
                result.msg = NoImageData;
            }
            else
            {            
                string savePath = MyHelper.ConfigurationHelper.GetConfig(ConfigItemName.imageSavePath.ToString());
                savePath = Path.Combine(savePath, DateTime.Now.ToShortDateString());
                bool b = MyHelper.FileHelper.FolderExistsCreater(savePath);           
                if (!b)
                {
                    result.msg = "服务器保存文件路径错误或拒绝访问！";
                    Console.WriteLine("==服务器保存文件路径错误或拒绝访问");
                }
                else
                {
                    try
                    {
                        String path = Path.Combine(savePath, filename);
                        //String SplitStr = "\r\n";
                        //String str = "application/octet-stream\r\n\r\n";
                        //String boundary = String.Empty;
                        //String endBoundray = String.Empty;
                        //boundary = data.Substring(0, data.IndexOf(SplitStr) + SplitStr.Length).Replace(SplitStr,"");
                        //endBoundray = boundary + "--";
                        //data.Replace(boundary, "");
                        //data.Replace(endBoundray, "");
                        //int prelenth = data.IndexOf(str) + str.Length;
                        //data = data.Substring(prelenth, data.Length - prelenth);
                        //data.Replace(SplitStr, "");
                        MyHelper.FileHelper.Write(path, data);
                        result.errCode = 0;
                        result.msg = "图片上传成功";
                        Console.WriteLine("==图片上传成功");
                        result.Data = path;
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = "服务保存失败！";
                        Console.WriteLine("==服务保存失败");
                    }
                }
            }
            Console.WriteLine("==图片上传结束");
            return result;
        }

        /// <summary>
        /// 拉取数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        private static NetResult ProcessPull(String tableName, String time, string stationId)
        {
            NetResult result = new NetResult() { errCode = 1, msg = "服务器未能处请求", Data = "" };
            if (String.IsNullOrEmpty(tableName))
            {
                result.msg = "Table 参数是必须的";
                return result;
            }
            if (String.IsNullOrEmpty(time))
            {
                time = "2018-01-01 00:00:00";
            }

            switch (tableName)
            {
                case "bill_image":
                    if (ChecledStationid(stationId) == false)
                    {
                        result.errCode = 1;
                        result.msg = ParamErr;
                        return result;
                    }
                    try
                    {
                        List<BillImage> datas = DatabaseOPtionHelper.GetInstance().select<BillImage>(BaseDataModel.GetSql(tableName, time));
                        if (datas.Count > 0)
                        {
                            result.errCode = 0;
                            result.Data = MyHelper.JsonHelper.ObjectToJson(datas);
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 0;
                            result.msg = Nodata;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = "未知错误";
                    }
                    break;
                case "bill_taxation_money_record":
                    if (ChecledStationid(stationId) == false)
                    {
                        result.errCode = 1;
                        result.msg = ParamErr;
                        return result;
                    }
                    try
                    {
                        List<BillTaxationMoneyRecord> datas = DatabaseOPtionHelper.GetInstance().select<BillTaxationMoneyRecord>(BaseDataModel.GetSql(tableName, time));
                        if (datas.Count > 0)
                        {
                            result.errCode = 0;
                            result.Data = MyHelper.JsonHelper.ObjectToJson(datas);
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 0;
                            result.msg = Nodata;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = "未知错误";
                    }
                    break;
                case "car_info":
                    try
                    {
                        List<CarInfo> datas = DatabaseOPtionHelper.GetInstance().select<CarInfo>(BaseDataModel.GetSql(tableName, time));
                        if (datas.Count > 0)
                        {
                            result.errCode = 0;
                            result.Data = MyHelper.JsonHelper.ObjectToJson(datas);
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 0;
                            result.msg = Nodata;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = "未知错误";
                    }
                    break;
                case "camera_info":
                    if (ChecledStationid(stationId) == false)
                    {
                        result.errCode = 1;
                        result.msg = ParamErr;
                        return result;
                    }
                    try
                    {
                        List<CameraInfo> datas = DatabaseOPtionHelper.GetInstance().select<CameraInfo>(BaseDataModel.GetSql(tableName, time));
                        if (datas.Count > 0)
                        {
                            result.errCode = 0;
                            result.Data = MyHelper.JsonHelper.ObjectToJson(datas);
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 0;
                            result.msg = Nodata;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = "未知错误";
                    }
                    break;
                case "car_trae_recod":
                    try
                    {
                        List<CarTraeRecod> datas = DatabaseOPtionHelper.GetInstance().select<CarTraeRecod>(BaseDataModel.GetSql(tableName, time));
                        if (datas.Count > 0)
                        {
                            result.errCode = 0;
                            result.Data = MyHelper.JsonHelper.ObjectToJson(datas);
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 0;
                            result.msg = Nodata;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = "未知错误";
                    }
                    break;
                case "company":
                    try
                    {
                        List<Company> datas = DatabaseOPtionHelper.GetInstance().select<Company>(BaseDataModel.GetSql(tableName, time));
                        if (datas.Count > 0)
                        {
                            result.errCode = 0;
                            result.Data = MyHelper.JsonHelper.ObjectToJson(datas);
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 0;
                            result.msg = Nodata;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = "未知错误";
                    }
                    break;
                case "config":
                    if (ChecledStationid(stationId) == false)
                    {
                        result.errCode = 1;
                        result.msg = ParamErr;
                        return result;
                    }
                    try
                    {
                        List<Config> datas = DatabaseOPtionHelper.GetInstance().select<Config>(BaseDataModel.GetSql(tableName, time));
                        if (datas.Count > 0)
                        {
                            result.errCode = 0;
                            result.Data = MyHelper.JsonHelper.ObjectToJson(datas);
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 0;
                            result.msg = Nodata;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = "未知错误";
                    }
                    break;
                case "material":
                    try
                    {
                        List<Material> datas = DatabaseOPtionHelper.GetInstance().select<Material>(BaseDataModel.GetSql(tableName, time));
                        if (datas.Count > 0)
                        {
                            result.errCode = 0;
                            result.Data = MyHelper.JsonHelper.ObjectToJson(datas);
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 0;
                            result.msg = Nodata;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = "未知错误";
                    }
                    break;
                case "material_category":
                    break;
                case "material_taxation_recod":
                    break;
                case "roles":
                    try
                    {
                        List<Roles> datas = DatabaseOPtionHelper.GetInstance().select<Roles>(BaseDataModel.GetSql(tableName, time));
                        if (datas.Count > 0)
                        {
                            result.errCode = 0;
                            result.Data = MyHelper.JsonHelper.ObjectToJson(datas);
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 0;
                            result.msg = Nodata;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = "未知错误";
                    }
                    break;
                case "station":
                    try
                    {
                        List<Station> datas = DatabaseOPtionHelper.GetInstance().select<Station>(BaseDataModel.GetSql(tableName, time));
                        if (datas.Count > 0)
                        {
                            result.errCode = 0;
                            result.Data = MyHelper.JsonHelper.ObjectToJson(datas);
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 0;
                            result.msg = Nodata;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = "未知错误";
                    }
                    break;
                case "user":
                    try
                    {
                        List<User> datas = DatabaseOPtionHelper.GetInstance().select<User>(BaseDataModel.GetSql(tableName, time));
                        if (datas.Count > 0)
                        {
                            result.errCode = 0;
                            result.Data = MyHelper.JsonHelper.ObjectToJson(datas);
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 0;
                            result.msg = Nodata;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = "未知错误";
                    }
                    break;
                case "weighing_bill":
                    if (ChecledStationid(stationId) == false)
                    {
                        result.errCode = 1;
                        result.msg = ParamErr;
                        return result;
                    }
                    try
                    {
                        List<WeighingBill> datas = DatabaseOPtionHelper.GetInstance().select<WeighingBill>(BaseDataModel.GetSql(tableName, time));
                        if (datas.Count > 0)
                        {
                            result.errCode = 0;
                            result.Data = MyHelper.JsonHelper.ObjectToJson(datas);
                            result.msg = successStr;
                        }
                        else
                        {
                            result.errCode = 0;
                            result.msg = Nodata;
                        }
                    }
                    catch
                    {
                        result.errCode = 1;
                        result.msg = "未知错误";
                    }
                    break;
                default:
                    break;
            }
            return result;
        }

        private static bool ChecledStationid(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return false;
            }
            return true;
        }
    }



    public class NetResult
    {
        public int errCode { get; set; }
        public String msg { get; set; }
        public object Data { get; set; }
    }
}

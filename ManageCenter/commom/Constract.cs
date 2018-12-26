using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
    class Constract
    {
        public static string TempPath = System.IO.Path.Combine(MyHelper.FileHelper.GetRunTimeRootPath(), "temp");
        public static string HUserFilePath = TempPath + "\\" + "horstoryuser.xml";

        public static readonly string defaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public static readonly string DateFormat = "yyyy-MM-dd";

        public static string BasePath = "/ManageCenter;component/";

        public static User currentUser = null;

        public static string valueSplit = "'";

        public static string templatePath = MyHelper.FileHelper.GetRunTimeRootPath() + "\\template\\";

        public static string tempPath = MyHelper.FileHelper.GetRunTimeRootPath() + "temp";
        public static string tempSupplyFileName = "SupplyCompanys.xml";
        public static string tempCustomerFileName = "CustomerCompanys.xml";
        public static string tempMatreialFileName = "MatreialCompanys.xml";
        public static string tempCarFileName = "CarCompanys.xml";

        public static String CaputureSuffix = ".jpg";
    }
}

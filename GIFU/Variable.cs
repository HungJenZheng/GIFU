using System.Web.Configuration;

namespace GIFU
{
    public class Variable
    {
        /// <summary>
        /// 取得連線字串
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString(); }
        }

        public static string GetSaveFilePath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["ImageFilePath"].ToString(); }
        }

        public static HttpRuntimeSection GetMaxRequestLength()
        {
            return (HttpRuntimeSection)System.Configuration.ConfigurationManager.GetSection("system.web/httpRuntime");
        }

        /// <summary>
        /// 取得訊息物件
        /// </summary>
        /// <param name="result"></param>
        /// <param name="messange"></param>
        /// <returns></returns>
        public static Models.ResultVM GetResult(int result, string messange)
        {
            return new Models.ResultVM() { result = result, message = messange };
        }
    }
}
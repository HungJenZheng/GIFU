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

        public static string GetMailAccount
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MailAccount"].ToString(); }
        }

        public static string GetMailPassword
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["MailPassword"].ToString(); }
        }

        public static string GetSmtpHost
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["SmtpHost"].ToString(); }
        }

        public static string GetSmtpPort
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["SmtpPort"].ToString(); }
        }

        public static string GetMailSubject
        {
            get
            {
                return "GIFU物品分享平台通知";
            }
        }

        public static string GetMailTemplate
        {
            get
            {
                return @"<!DOCTYPE html>
                        <html lang='en'>
                        <head>
                            <meta charset='UTF-8'>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                            <meta http-equiv='X-UA-Compatible' content='ie=edge'>
                            <title>GIFU通知</title>
                        </head>
                        <body style='padding: 10px 20px;'>
                            <p>親愛的<!--USERNAME-->您好：</p>
                            <p style='padding-left: 10px;'><!--CONTENT--></p>
                            <p>請盡速至平台查看：<!--URL--></p>
                            <p style='float: right;'>GIFU Administrator</p>
                        </body>
                        </html>";
            }
        }

        public static string GetAuthenciateionTemplate
        {
            get
            {
                return @"<!DOCTYPE html>
                        <html lang='en'>
                        <head>
                            <meta charset='UTF-8'>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                            <meta http-equiv='X-UA-Compatible' content='ie=edge'>
                            <title>GIFU通知</title>
                        </head>
                        <body style='padding: 10px 20px;'>
                            <p>親愛的<!--USERNAME-->您好：</p>
                            <p style='padding-left: 10px;'>恭喜您註冊成功，請在30分鐘內點選以下連結啟用帳號。</p>
                            <p>啟用網址：<!--URL--></p>
                            <p style='float: right;'>GIFU Administrator</p>
                        </body>
                        </html>";
            }
        }

        public static string GetCurrentHost
        {
            get
            {
                //return "http://localhost:4647";
                return "http://localhost:8080";
            }
        }
    }
}
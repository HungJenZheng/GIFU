using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GIFU.Models
{
    public class MailServices
    {
        private DataAccessTool dataAccessTool = new DataAccessTool();
        private Tools.EmailSender emailSender = new Tools.EmailSender();

        private List<MailMessage> GetUnSendMessage()
        {
            DataTable dataTable;
            string sql = @"SELECT A.EMAIL AS Email, 
	                              A.NAME AS UserName,
	                              N.CONTENT AS Content,
	                              N.URL AS Url
                           FROM dbo.NOTIFICATION N
	                           JOIN ACCOUNT A
		                           ON N.RECEIVE_ID = A.USER_ID
                           WHERE N.IS_SEND = 'F'";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModelList<MailMessage>(dataTable);
            else
                return new List<MailMessage>();
        }

        private int SetIsSend()
        {
            string sql = @"UPDATE [GIFU].[dbo].[NOTIFICATION] SET IS_SEND = 'T'";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            int result = dataAccessTool.ExecuteNonQuery(Variable.GetConnectionString, sql, parameters);
            return result;
        }

        public async void AutoSendSystemNotificationAsync()
        {
            List<MailMessage> mailMessages = GetUnSendMessage();
            foreach (var item in mailMessages)
            {
                Tools.MailModel mailModel = new Tools.MailModel()
                {
                    From = Variable.GetMailAccount,
                    To = item.Email,
                    Subject = Variable.GetMailSubject,
                    Body = Variable.GetMailTemplate.Replace("<!--USERNAME-->", item.UserName)
                                                    .Replace("<!--CONTENT-->", item.Content)
                                                    .Replace("<!--URL-->", Variable.GetCurrentHost + item.Url)
                };
                await emailSender.SendAnEmailAsync(mailModel);
            }
            SetIsSend();
        }

        public string CreateAToken(int userId)
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            AccountServices accountServices = new AccountServices();
            accountServices.StoreTokenToUser(userId, token);
            return token;
        }

        public bool VerifyToken(int userId, string token)
        {
            byte[] data = Convert.FromBase64String(token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            if (when < DateTime.UtcNow.AddMinutes(-30))
            {
                return false;
            }
            AccountServices accountServices = new AccountServices();
            string tokenStored = accountServices.GetTokenByUserId(userId);
            if (token != tokenStored) return false;
            return true;
        }
    }
}
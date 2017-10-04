using System.Collections.Generic;
using System.Data;

namespace GIFU.Models
{
    public class NotificationServices
    {
        private DataAccessTool dataAccessTool = new DataAccessTool();

        public List<Notification> GetMessages()
        {
            DataTable dataTable;
            string sql = @"SELECT [NOTIFICATION_ID] AS NotificationId
                                  ,[RECEIVE_ID] AS ReceiveId
                                  ,[SEND_ID] AS SendId
                                  ,[CONTENT] AS Content
                                  ,[GOOD_ID] AS GoodId
                                  ,[URL] AS Url
                                  ,[TIME] AS Time
                              FROM [GIFU].[dbo].[NOTIFICATION]";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            //parameters.Add(new KeyValuePair<string, object>("@UserId", userID.NullToDBNullValue()));
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModelList<Notification>(dataTable);
            else
                return new List<Notification>();
        }
    }
}
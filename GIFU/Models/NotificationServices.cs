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

        public List<Notification> GetMessagesById(int? userId)
        {
            DataTable dataTable;
            string sql = @"SELECT TOP 10 [NOTIFICATION_ID] AS NotificationId
                                  ,[RECEIVE_ID] AS ReceiveId
                                  ,[SEND_ID] AS SendId
                                  ,[CONTENT] AS Content
                                  ,N.[GOOD_ID] AS GoodId
                                  ,[URL] AS Url
                                  ,[TIME] AS Time
								  ,GP.[PATH] AS ImageUrl
                              FROM [GIFU].[dbo].[NOTIFICATION] AS N
								LEFT JOIN [GIFU].[dbo].[GOOD_PICTURE] AS GP
									ON N.GOOD_ID = GP.GOOD_ID
                              WHERE N.[RECEIVE_ID] = 1 AND GP.IS_MAIN = 'T'
                              ORDER BY [TIME] DESC";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@UserId", userId));
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModelList<Notification>(dataTable);
            else
                return new List<Notification>();
        }
    }
}
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

        /// <summary>
        /// 根據UserId取得最新的十筆通知
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
								  ,[IS_READ] AS IsRead
								  ,SUM(CASE IS_READ
									WHEN 'F' THEN 1
									WHEN 'T' THEN 0
									ELSE 0
								  END) OVER (PARTITION BY RECEIVE_ID) AS UnReadCount
                              FROM [GIFU].[dbo].[NOTIFICATION] AS N
								LEFT JOIN [GIFU].[dbo].[GOOD_PICTURE] AS GP
									ON N.GOOD_ID = GP.GOOD_ID
                              WHERE N.[RECEIVE_ID] = @UserId AND GP.IS_MAIN = 'T'
                              ORDER BY [NOTIFICATION_ID] DESC";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@UserId", userId));
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModelList<Notification>(dataTable);
            else
                return new List<Notification>();
        }

        /// <summary>
        /// 標示訊息為已讀
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int SetIsRead(int? userId)
        {
            string sql = @"UPDATE [GIFU].[dbo].[NOTIFICATION] SET IS_READ = 'T' WHERE RECEIVE_ID = @UserId";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@UserId", userId));
            int result = dataAccessTool.ExecuteNonQuery(Variable.GetConnectionString, sql, parameters);
            return result;
        }
    }
}
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GIFU.Hubs
{
    public class MessagesRepository
    {
        private static MessagesRepository instance = null;

        public static MessagesRepository GetInstance()
        {
            if (instance == null)
                instance = new MessagesRepository();
            return instance;
        }

        private MessagesRepository()
        {
            RegisterForSqlDependency();
        }

        private void RegisterForSqlDependency()
        {
            var messages = new List<Models.Notification>();
            using (var connection = new SqlConnection(Variable.GetConnectionString))
            {
                string sql = @"SELECT [NOTIFICATION_ID], [RECEIVE_ID], [SEND_ID], [CONTENT], [GOOD_ID], [URL]
                              FROM [dbo].[NOTIFICATION]";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Notification = null;
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += dependency_OnChange;
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MessagesHub.SendMessages();
                RegisterForSqlDependency();
            }
        }
    }
}
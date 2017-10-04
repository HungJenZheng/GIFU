using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GIFU.Hubs
{
    public class MessagesRepository
    {
        public List<Models.Notification> GetAllMessages()
        {
            var messages = new List<Models.Notification>();
            using (var connection = new SqlConnection(Variable.GetConnectionString))
            {
                string sql = @"SELECT [NOTIFICATION_ID] NotificationId, [RECEIVE_ID] ReceiveId, [SEND_ID] SendId, [CONTENT] Content, [GOOD_ID] GoodId, [URL] Url
                              FROM [dbo].[NOTIFICATION]";
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Notification = null;
                    var dependency = new SqlDependency(command);
                    dependency.OnChange += dependency_OnChange;
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        messages.Add(item: new Models.Notification
                        {
                            NotificationId = (int)reader["NotificationId"],
                            ReceiveId = (int)reader["ReceiveId"],
                            SendId = (int)reader["SendId"],
                            Content = (string)reader["Content"],
                            GoodId = (int)reader["GoodId"],
                            Url = (string)reader["Url"]
                        });
                    }
                    connection.Close();
                    //reader.Close();
                }
            }
            return messages;
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            //SqlDependency dependency = sender as SqlDependency;
            //dependency.OnChange -= dependency_OnChange;
            if (e.Type == SqlNotificationType.Change)
            {
                MessagesHub.SendMessages();
            }
        }
    }
}
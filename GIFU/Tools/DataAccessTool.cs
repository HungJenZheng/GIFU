using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GIFU
{
    public class DataAccessTool
    {
        //取得DataTable
        public DataTable Query(string connectionString, string sql, IList<KeyValuePair<string, object>> parameters)
        {
            return (DataTable)this.ExecuteSQL(connectionString, sql, parameters, cmd =>
            {
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                sqlAdapter.Fill(dataTable);
                return dataTable;
            });
        }

        //執行ExecuteNonQuery()
        public int ExecuteNonQuery(string connectionString, string sql, IList<KeyValuePair<string, object>> parameters)
        {
            object obj = this.ExecuteSQL(connectionString, sql, parameters, cmd => cmd.ExecuteNonQuery());
            return Convert.ToInt32(obj);
        }

        //執行ExecuteScalar()
        public object ExecuteScalar(string connectionString, string sql, IList<KeyValuePair<string, object>> parameters)
        {
            return this.ExecuteSQL(connectionString, sql, parameters, cmd => cmd.ExecuteScalar());
        }

        //包含transaction並且可以自訂要執行的命令
        private object ExecuteSQL(string connectionString, string sql, IList<KeyValuePair<string, object>> parameters, Func<SqlCommand, object> action)
        {
            object result = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlTransaction transcation = connection.BeginTransaction();
                command.Transaction = transcation;
                try
                {
                    foreach (KeyValuePair<string, object> parameter in parameters)
                    {
                        command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                    }
                    result = action(command);
                    transcation.Commit();
                }
                catch (SqlException ex)
                {
                    transcation.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }
    }
}

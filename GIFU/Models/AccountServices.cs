using ExtensionMethods;
using System.Collections.Generic;
using System.Data;

namespace GIFU.Models
{
    public class AccountServices
    {
        private DataAccessTool dataAccessTool = new DataAccessTool();

        /// <summary>
        /// 依照輸入的UserId取得Account資料
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Account GetAccountDetailById(int userID)
        {
            DataTable dataTable;
            string sql = @"SELECT [USER_ID] AS UserId,
								  EMAIL     AS Email,
								  PASSWD    AS Passwd,
								  NAME      AS Name,
								  SEX       AS Sex,
								  BIRTHDAY  AS Birthday,
								  PHONE     AS Phone,
								  [ADDRESS] AS Address,
								  PROVIDE_COUNT AS ProvideCount,
								  PHOTO_TYPE AS PhotoType,
								  IS_VALID  AS IsValid,
								  USER_TYPE AS UserType,
								  CONVERT(VARCHAR, UPDATE_DATE, 120)
						FROM dbo.ACCOUNT
						WHERE [USER_ID] = @UserId";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@UserId", userID.NullToDBNullValue()));
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModel<Account>(dataTable.Rows[0]);
            else
                return new Account();
        }

        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int AddAccount(Account account)
        {
            DataTable dataTable;
            string sql = @"SELECT [USER_ID] AS UserId
						FROM dbo.ACCOUNT
						WHERE EMAIL = @Email";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@Email", account.Email.NullToDBNullValue()));
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return -1;

            sql = @"INSERT INTO dbo.ACCOUNT(EMAIL, PASSWD, NAME, SEX, BIRTHDAY, PHONE, [ADDRESS], PHOTO_TYPE, UPDATE_DATE)
						   VALUES(@Email, @Passwd, @Name, @Sex, @Birthday, @Phone, @Address, @PhotoType, GETDATE())";
            //IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Clear();
            parameters.Add(new KeyValuePair<string, object>("@Email", account.Email.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Passwd", account.Passwd.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Name", account.Name.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Sex", account.Sex.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Birthday", account.Birthday.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Phone", account.Phone.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Address", account.Address.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@PhotoType", account.PhotoType.NullToDBNullValue()));
            int result;
            result = dataAccessTool.ExecuteNonQuery(Variable.GetConnectionString, sql, parameters);
            return result;
        }

        /// <summary>
        ///  更新帳號資訊
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int UpdateAccount(Account account)
        {
            string sql = @"UPDATE dbo.ACCOUNT SET 
							PASSWD = @Passwd, 
							NAME = @Name, 
							PHONE = @Phone, 
							[ADDRESS] = @Address, 
							UPDATE_DATE = GETDATE()
					WHERE [USER_ID] = @UserId";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@UserId", account.UserId.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Passwd", account.Passwd.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Name", account.Name.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Phone", account.Phone.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Address", account.Address.NullToDBNullValue()));
            int result;
            result = dataAccessTool.ExecuteNonQuery(Variable.GetConnectionString, sql, parameters);
            return result;
        }

        public Account Authentication(LoginVM loginVM)
        {
            DataTable dataTable;
            string sql = @"SELECT [USER_ID] AS UserId,
								EMAIL AS Email,
								NAME AS Name
						FROM dbo.ACCOUNT
						WHERE EMAIL = @Email AND PASSWD = @Passwd";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@Email", loginVM.Email.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Passwd", loginVM.Passwd.NullToDBNullValue()));
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModel<Account>(dataTable.Rows[0]);
            else
                return new Account();

        }
    }
}
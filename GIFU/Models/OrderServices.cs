using ExtensionMethods;
using System.Collections.Generic;
using System.Data;

namespace GIFU.Models
{
    public class OrderServices
    {
        private DataAccessTool dataAccessTool = new DataAccessTool();

        /// <summary>
        /// 依條件取得訂單資訊
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public List<Order> GetOrdersByCondition(OrderGetArg arg)
        {
            DataTable dataTable;
            string sql = @"SELECT ORDER_ID  AS OrderId,
                                  O.GOOD_ID   AS GoodId,
                                  A.NAME AS UserName,
                                  PLACE_TIME AS PlaceTime,
                                  AMOUNT    AS Amount,
                                  [STATUS]    AS Status,
								  O.COMMENT AS Comment,
                                  CONVERT(VARCHAR, O.UPDATE_DATE, 120) AS UpdateDate
                        FROM [dbo].[ORDER] O
	                        JOIN dbo.ACCOUNT A
		                        ON O.USER_ID = A.USER_ID
                        WHERE (O.GOOD_ID = @GoodId) OR
                              (O.[USER_ID] = @UserId)
                        ORDER BY ORDER_ID DESC";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@GoodId", arg.GoodId.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@UserId", arg.UserId.NullToDBNullValue()));
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModelList<Order>(dataTable);
            else
                return new List<Order>();
        }

        public List<OrderShow> GetOrdersDetailByUserId(int? userId)
        {
            DataTable dataTable;
            string sql = @"SELECT O.ORDER_ID  AS OrderId,
                                  O.GOOD_ID AS GoodId,
								  G.TITLE AS GoodsTitle,
                                  A.NAME AS GiverName,
                                  O.PLACE_TIME AS PlaceTime,
                                  O.AMOUNT AS Amount,
								  O.COMMENT AS Comment,
								  C.NAME AS StatusName,
								  O.[ADDRESS] AS Address,
                                  CONVERT(VARCHAR, O.UPDATE_DATE, 120) AS UpdateDate,
								  G.NEW_DEGREE AS NewDegree,
								  (SELECT NAME FROM dbo.CODE WHERE CODE_KIND = 'TAG' AND CODE_ID = G.TAG1) AS Tag1Name,
								  (SELECT NAME FROM dbo.CODE WHERE CODE_KIND = G.TAG1 AND CODE_ID = G.TAG2) AS Tag2Name,
								  GP.PATH AS PicPath
                        FROM [dbo].[ORDER] O
							JOIN dbo.GOOD G
								ON O.GOOD_ID = G.GOOD_ID
	                        JOIN dbo.ACCOUNT A
		                        ON G.USER_ID = A.USER_ID
	                        JOIN dbo.GOOD_PICTURE GP
		                        ON O.GOOD_ID = GP.GOOD_ID
							JOIN dbo.CODE C
								ON O.[STATUS] = C.CODE_ID
                        WHERE (O.[USER_ID] = @UserId) AND
	                          GP.IS_MAIN = 'T' AND
							  C.CODE_KIND = 'OST'";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@UserId", userId.NullToDBNullValue()));
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModelList<OrderShow>(dataTable);
            else
                return new List<OrderShow>();
        }

        public Order GetOrderById(int? orderId)
        {
            DataTable dataTable;
            string sql = @"SELECT ORDER_ID  AS OrderId,
                                  GOOD_ID   AS GoodId,
                                  USER_ID   AS UserId,
                                  CONVERT(VARCHAR, PLACE_TIME, 120) AS PlaceTime,
                                  AMOUNT    AS Amount,
                                  COMMENT   AS Comment,
                                  [STATUS]  AS Status,
                                  ADDRESS   AS Address,
                                  TRADE_METHOD AS TradeMethod,
                                  CONVERT(VARCHAR, UPDATE_DATE, 120) AS UpdateDate
                        FROM [dbo].[ORDER]
                        WHERE ORDER_ID = @OrderId";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@OrderId", orderId));
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModel<Order>(dataTable.Rows[0]);
            else
                return new Order();
        }

        public int AddOrder(Order order)
        {
            string sql = @"INSERT INTO dbo.[ORDER](GOOD_ID, USER_ID, PLACE_TIME, AMOUNT, COMMENT, STATUS, ADDRESS, TRADE_METHOD, UPDATE_DATE)
	                       VALUES(@GoodId, @UserId, GETDATE(), @Amount, @Comment, @Status, @Address, @TradeMethod, GETDATE())";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@GoodId", order.GoodId.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@UserId", order.UserId.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Amount", order.Amount.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Comment", order.Comment.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Status", order.Status ?? "1"));
            parameters.Add(new KeyValuePair<string, object>("@Address", order.Address.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@TradeMethod", order.TradeMethod ?? "1"));
            int result;
            result = dataAccessTool.ExecuteNonQuery(Variable.GetConnectionString, sql, parameters);
            return result;
        }

        public int UpdateStatus(int orderId, int status)
        {
            string sql = @"UPDATE dbo.[ORDER] SET STATUS = @Status, UPDATE_DATE = GETDATE() WHERE ORDER_ID = @OrderId";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@OrderId", orderId.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Status", status.NullToDBNullValue()));
            int result;
            result = dataAccessTool.ExecuteNonQuery(Variable.GetConnectionString, sql, parameters);
            return result;
        }
    }
}
using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Data;

namespace GIFU.Models
{
    public class GoodsServices
    {
        private DataAccessTool dataAccessTool = new DataAccessTool();

        /// <summary>
        /// 依照條件取得簡單物品資訊
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public List<Goods> GetGoodsByConditions(GoodsSearchArg arg)
        {
            DataTable dataTable;
            string sql = @"SELECT G.GOOD_ID   AS GoodId,
                                  [USER_ID] AS UserId,
                                  TITLE     AS Title,
                                  AMOUNT AS Amount,
                                  NEW_DEGREE AS NewDegree,
                                  TAG1 AS Tag1,
                                  TAG2 AS Tag2,
                                  (SELECT NAME FROM dbo.CODE WHERE CODE_KIND = 'TAG' AND CODE_ID = TAG1) AS Tag1Name, 
                                  (SELECT NAME FROM dbo.CODE WHERE CODE_KIND = TAG1 AND CODE_ID = TAG2) AS Tag2Name, 
                                  CONVERT(VARCHAR, UPDATE_DATE, 120) AS UpdateDate,
                                  GP.PATH PicPath
                        FROM dbo.GOOD G
                            LEFT JOIN dbo.GOOD_PICTURE GP
                                ON G.GOOD_ID = GP.GOOD_ID AND GP.IS_MAIN = 'T'
                        WHERE (UPPER(TITLE) LIKE @Title) AND
                              (TAG1 = @Tag1 OR @Tag1 = '') AND
                              (TAG2 = @Tag2 OR @Tag2 = '') AND 
                              STATUS = 'Y' AND AMOUNT > 0
                        ORDER BY UPDATE_DATE DESC
                        OFFSET @Offset ROWS FETCH NEXT @NextNo ROWS ONLY";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@Title", arg.Title == null ? "%" : '%' + arg.Title.ToUpper() + '%'));
            parameters.Add(new KeyValuePair<string, object>("@Tag1", arg.Tag1 == null ? string.Empty : arg.Tag1));
            parameters.Add(new KeyValuePair<string, object>("@Tag2", arg.Tag2 == null ? string.Empty : arg.Tag2));
            parameters.Add(new KeyValuePair<string, object>("@Offset", arg.Offset ?? 0));
            parameters.Add(new KeyValuePair<string, object>("@NextNo", arg.NextNo ?? 20));
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModelList<Goods>(dataTable);
            else
                return new List<Goods>();
        }

        /// <summary>
        /// 根據GoodId取得物品詳細資訊
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public Goods GetGoodDetailByGoodsId(int? goodsId)
        {
            DataTable dataTable;
            string sql = @"SELECT GOOD_ID   AS GoodId,
                                  [USER_ID] AS UserId,
                                  TITLE     AS Title,
                                  INTRODUCTION AS Introduction,
                                  AMOUNT AS Amount,
                                  NEW_DEGREE AS NewDegree,
                                  STATUS AS Status,
                                  TAG1 AS Tag1,
                                  TAG2 AS Tag2,
                                  (SELECT NAME FROM dbo.CODE WHERE CODE_KIND = 'TAG' AND CODE_ID = TAG1) AS Tag1Name, 
                                  (SELECT NAME FROM dbo.CODE WHERE CODE_KIND = TAG1 AND CODE_ID = TAG2) AS Tag2Name, 
                                  IS_REASON AS IsReason,
                                  HIT_COUNT AS HitCount, 
                                  CONVERT(VARCHAR, UPDATE_DATE, 120) AS UpdateDate
                        FROM dbo.GOOD
                        WHERE GOOD_ID = @GoodId";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@GoodId", goodsId ?? (object)string.Empty));
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModel<Goods>(dataTable.Rows[0]);
            else
                return new Goods();
        }

        /// <summary>
        /// 根據UserId取得物品詳細資訊
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Goods> GetGoodsDetailByUserId(int? userId)
        {
            DataTable dataTable;
            string sql = @"SELECT G.GOOD_ID   AS GoodId,
                                  [USER_ID] AS UserId,
                                  TITLE     AS Title,
                                  INTRODUCTION AS Introduction,
                                  AMOUNT AS Amount,
                                  NEW_DEGREE AS NewDegree,
                                  TAG1 AS Tag1,
                                  TAG2 AS Tag2,
                                  (SELECT NAME FROM dbo.CODE WHERE CODE_KIND = 'TAG' AND CODE_ID = TAG1) AS Tag1Name, 
                                  (SELECT NAME FROM dbo.CODE WHERE CODE_KIND = TAG1 AND CODE_ID = TAG2) AS Tag2Name,
                                  IS_REASON AS IsReason,
                                  CONVERT(VARCHAR, UPDATE_DATE, 120) AS UpdateDate,
                                  GP.PATH PicPath
                        FROM dbo.GOOD G
                            LEFT JOIN dbo.GOOD_PICTURE GP
                                ON G.GOOD_ID = GP.GOOD_ID
                        WHERE USER_ID = @UserId AND GP.IS_MAIN = 'T'
                        ORDER BY UPDATE_DATE DESC";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@UserId", userId ?? 0));
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModelList<Goods>(dataTable);
            else
                return new List<Goods>();
        }

        /// <summary>
        /// 根據GoodId取得物品留言
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public List<GoodsMessage> GetGoodsMessagesById(int? goodsId)
        {
            DataTable dataTable;
            string sql = @"SELECT GOOD_ID AS GoodId, 
                                  COMMENT_NO AS CommentNo, 
                                  [TYPE] AS Type, 
                                  [USER_ID] AS UserId, 
                                  [MESSAGE] AS Message,
                                  CONVERT(VARCHAR, TIME, 120) AS TIME
                          FROM GIFU.dbo.GOOD_MESSAGE 
                          WHERE GOOD_ID = @GoodId
                          ORDER BY COMMENT_NO ASC, [TYPE] DESC";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@GoodId", goodsId ?? (object)string.Empty));
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModelList<GoodsMessage>(dataTable);
            else
                return new List<GoodsMessage>();
        }

        /// <summary>
        ///新增物品訊息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public int AddGoodsMessage(GoodsMessage message)
        {
            //取得目前最大的CommentNo
            string sql = @"SELECT MAX(COMMENT_NO) FROM GOOD_MESSAGE WHERE GOOD_ID = @GoodId";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@GoodId", message.GoodId.NullToDBNullValue()));
            object obj = dataAccessTool.ExecuteScalar(Variable.GetConnectionString, sql, parameters);
            int maxNo = (obj != DBNull.Value) ? Convert.ToInt32(obj) : 0;

            sql = @"INSERT INTO [GIFU].[dbo].[GOOD_MESSAGE]
                    VALUES(@GoodId, @CommentNo, @Type, @UserId, @Message, GETDATE())";
            parameters.Clear();
            parameters.Add(new KeyValuePair<string, object>("@GoodId", message.GoodId.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@CommentNo", maxNo + 1));
            parameters.Add(new KeyValuePair<string, object>("@Type", message.Type.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@UserId", message.UserId.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Message", message.Message.NullToDBNullValue()));
            int result = dataAccessTool.ExecuteNonQuery(Variable.GetConnectionString, sql, parameters);
            return result;
        }

        /// <summary>
        ///更新物品訊息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public int AnswerGoodsMessage(GoodsMessage message)
        {
            string sql = @"INSERT INTO [GIFU].[dbo].[GOOD_MESSAGE]
                    VALUES(@GoodId, @CommentNo, @Type, @UserId, @Message, GETDATE())";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@GoodId", message.GoodId.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@CommentNo", message.CommentNo.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Type", message.Type.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@UserId", message.UserId.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Message", message.Message.NullToDBNullValue()));
            int result = dataAccessTool.ExecuteNonQuery(Variable.GetConnectionString, sql, parameters);
            return result;
        }

        /// <summary>
        ///更新物品訊息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public int UpdateGoodsMessage(GoodsMessage message)
        {
            string sql = @"UPDATE [GIFU].[dbo].[GOOD_MESSAGE]
                            SET MESSAGE = @Message, TIME = GETDATE()
                            WHERE GOOD_ID = @GoodId AND COMMENT_NO = @CommentNo AND Type = 'A'";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@GoodId", message.GoodId.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@CommentNo", message.CommentNo.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Message", message.Message.NullToDBNullValue()));
            int result = dataAccessTool.ExecuteNonQuery(Variable.GetConnectionString, sql, parameters);
            return result;
        }

        /// <summary>
        /// 新增物品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public int AddGoods(Goods goods)
        {
            string sql = @"INSERT INTO dbo.GOOD([USER_ID], TITLE, INTRODUCTION, AMOUNT, NEW_DEGREE, TAG1, TAG2, IS_REASON, UPDATE_DATE)
                           VALUES(@UserId, @Title, @Introduction, @Amount, @NewDegree, @Tag1, @Tag2, @IsReason, GETDATE())
                           SELECT SCOPE_IDENTITY()";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@UserId", goods.UserId.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Title", goods.Title.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Introduction", goods.Introduction.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Amount", goods.Amount.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@NewDegree", goods.NewDegree.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Tag1", goods.Tag1.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Tag2", goods.Tag2.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@IsReason", goods.IsReason.NullToDBNullValue()));
            object result = dataAccessTool.ExecuteScalar(Variable.GetConnectionString, sql, parameters);
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// 更新書籍
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public int UpdateGoods(Goods goods)
        {
            string sql = @"UPDATE dbo.GOOD SET 
                                TITLE = @Title, 
                                INTRODUCTION = @Introduction, 
                                AMOUNT = @Amount, 
                                NEW_DEGREE = @NewDegree, 
                                TAG1 = @Tag1, 
                                TAG2 = @Tag2, 
                                IS_REASON = @IsReason, 
                                UPDATE_DATE = GETDATE()
                           WHERE GOOD_ID = @GoodId";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@GoodId", goods.GoodId.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Title", goods.Title.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Introduction", goods.Introduction.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Amount", goods.Amount.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@NewDegree", goods.NewDegree.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Tag1", goods.Tag1.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@Tag2", goods.Tag2.NullToDBNullValue()));
            parameters.Add(new KeyValuePair<string, object>("@IsReason", goods.IsReason.NullToDBNullValue()));
            int result;
            result = dataAccessTool.ExecuteNonQuery(Variable.GetConnectionString, sql, parameters);
            return result;
        }

        public int DeleteGoods(int? goodsId)
        {
            string sql = @"DELETE FROM [GIFU].[dbo].[GOOD] WHERE GOOD_ID = @GoodId";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@GoodId", goodsId.NullToDBNullValue()));
            int result;
            result = dataAccessTool.ExecuteNonQuery(Variable.GetConnectionString, sql, parameters);
            return result;
        }

        /// <summary>
        /// 根據GoodId取得該物品的詳細圖片
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public List<GoodsPicture> GetGoodPicturePathById(int? goodsId)
        {
            DataTable dataTable;
            string sql = @"SELECT GOOD_ID   AS GoodId,
                                  PIC_NO    AS PicNo,
                                  PATH      AS Path,
                                  IS_MAIN   AS IsMain
                           FROM GOOD_PICTURE
                           WHERE GOOD_ID = @GoodId";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@GoodId", goodsId ?? (object)string.Empty));
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModelList<GoodsPicture>(dataTable);
            else
                return new List<GoodsPicture>();
        }

        /// <summary>
        /// 新增物品照片
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public int AddPicturePath(int goodsId, string path)
        {
            //取得目前最大的Pic_No
            string sql = @"SELECT MAX(PIC_NO) FROM GOOD_PICTURE WHERE GOOD_ID = @GoodsId";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@GoodsId", goodsId));
            object obj = dataAccessTool.ExecuteScalar(Variable.GetConnectionString, sql, parameters);
            int maxNo = (obj != DBNull.Value) ? Convert.ToInt32(obj) : 0;

            sql = @"INSERT INTO dbo.GOOD_PICTURE
                           VALUES(@GoodsId, @PicNO, @Path, @IsMain)";
            parameters.Clear();
            parameters.Add(new KeyValuePair<string, object>("@GoodsId", goodsId));
            parameters.Add(new KeyValuePair<string, object>("@PicNO", maxNo + 1));
            parameters.Add(new KeyValuePair<string, object>("@Path", path));
            //第一張照片為Main
            if (maxNo == 0) parameters.Add(new KeyValuePair<string, object>("@IsMain", 'T'));
            else parameters.Add(new KeyValuePair<string, object>("@IsMain", 'F'));

            int result = dataAccessTool.ExecuteNonQuery(Variable.GetConnectionString, sql, parameters);
            return result;
        }

        public int DeletePicturePathByGoodsId(int goodsId)
        {
            string sql = @"DELETE FROM [GIFU].[dbo].[GOOD_PICTURE] WHERE GOOD_ID = @GoodId";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@GoodId", goodsId));
            int result = dataAccessTool.ExecuteNonQuery(Variable.GetConnectionString, sql, parameters);
            return result;
        }

        /// <summary>
        /// 增加物品點擊數
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public int AddHitCount(int? goodsId)
        {
            string sql = @"UPDATE [GIFU].[dbo].[GOOD] SET [HIT_COUNT] = [HIT_COUNT] + 1
                           WHERE GOOD_ID = @GoodsId";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@GoodsId", goodsId.NullToDBNullValue()));
            int result = dataAccessTool.ExecuteNonQuery(Variable.GetConnectionString, sql, parameters);
            return result;
        }

        public List<Goods> GetRecommendGoods()
        {
            DataTable dataTable;
            string sql = @"SELECT TOP 12 G.GOOD_ID   AS GoodId,
                                  [USER_ID] AS UserId,
                                  TITLE     AS Title,
                                  AMOUNT AS Amount,
                                  NEW_DEGREE AS NewDegree,
                                  TAG1 AS Tag1,
                                  TAG2 AS Tag2,
                                  (SELECT NAME FROM dbo.CODE WHERE CODE_KIND = 'TAG' AND CODE_ID = TAG1) AS Tag1Name, 
                                  (SELECT NAME FROM dbo.CODE WHERE CODE_KIND = TAG1 AND CODE_ID = TAG2) AS Tag2Name, 
                                  CONVERT(VARCHAR, UPDATE_DATE, 120) AS UpdateDate,
                                  GP.PATH PicPath
                        FROM dbo.GOOD G
                            LEFT JOIN dbo.GOOD_PICTURE GP
                                ON G.GOOD_ID = GP.GOOD_ID AND GP.IS_MAIN = 'T'
                        WHERE STATUS = 'Y' AND AMOUNT > 0
                        ORDER BY HIT_COUNT ASC, UPDATE_DATE ASC";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModelList<Goods>(dataTable);
            else
                return new List<Goods>();
        }


        public Goods GetNewestGoodsByUserId(int userId)
        {
            DataTable dataTable;
            string sql = @"SELECT G.GOOD_ID   AS GoodId,
                                  G.[USER_ID] AS UserId,
                                  TITLE     AS Title,
                                  INTRODUCTION AS Introduction,
                                  AMOUNT AS Amount,
                                  NEW_DEGREE AS NewDegree,
                                  STATUS AS Status,
                                  TAG1 AS Tag1,
                                  TAG2 AS Tag2,
                                  (SELECT NAME FROM dbo.CODE WHERE CODE_KIND = 'TAG' AND CODE_ID = TAG1) AS Tag1Name, 
                                  (SELECT NAME FROM dbo.CODE WHERE CODE_KIND = TAG1 AND CODE_ID = TAG2) AS Tag2Name, 
                                  IS_REASON AS IsReason,
                                  HIT_COUNT AS HitCount,
                                  CONVERT(VARCHAR, G.UPDATE_DATE, 120) AS UpdateDate,
								  GP.[PATH] AS PicPath
                        FROM dbo.GOOD G
							JOIN dbo.ACCOUNT A
								ON G.USER_ID = A.USER_ID
							JOIN dbo.GOOD_PICTURE GP
								ON G.GOOD_ID = GP.GOOD_ID
                        WHERE G.USER_ID = @UserId AND GP.IS_MAIN = 'T'";
            IList<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>("@UserId", userId.NullToDBNullValue()));
            dataTable = dataAccessTool.Query(Variable.GetConnectionString, sql, parameters);
            if (dataTable.Rows.Count > 0)
                return DataMappingTool.GetModel<Goods>(dataTable.Rows[0]);
            else
                return new Goods();
        }
    }
}
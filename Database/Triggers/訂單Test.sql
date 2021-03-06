/****** SSMS 中 SelectTopNRows 命令的指令碼  ******/
SELECT TOP 1000 [ORDER_ID]
      ,[GOOD_ID]
      ,[USER_ID]
      ,[PLACE_TIME]
      ,[AMOUNT]
      ,[COMMENT]
      ,[STATUS]
      ,[ADDRESS]
      ,[TRADE_METHOD]
      ,[UPDATE_DATE]
  FROM [GIFU].[dbo].[ORDER]


insert into dbo.[ORDER](GOOD_ID, USER_ID, PLACE_TIME, AMOUNT, TRADE_METHOD)
values(1062, 2, GETDATE(), 3, 2)

update dbo.[ORDER]  set STATUS = '3' where ORDER_ID = 1

update dbo.[ORDER]  set TRADE_METHOD = '1' where ORDER_ID = 1

DBCC CHECKIDENT ('[ORDER]', RESEED, 1);

truncate table [GIFU].[dbo].[ORDER]

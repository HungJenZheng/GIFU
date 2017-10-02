/****** SSMS 中 SelectTopNRows 命令的指令碼  ******/
SELECT TOP 1000 [GOOD_ID]
      ,[USER_ID]
      ,[TITLE]
      ,[INTRODUCTION]
      ,[AMOUNT]
      ,[NEW_DEGREE]
      ,[STATUS]
      ,[TAG1]
      ,[TAG2]
      ,[IS_REASON]
      ,[HIT_COUNT]
      ,[UPDATE_DATE]
  FROM [GIFU].[dbo].[GOOD]

  insert into dbo.GOOD([USER_ID],[TITLE],[INTRODUCTION],[AMOUNT],[NEW_DEGREE],[TAG1],[TAG2],[UPDATE_DATE])
	  values(1, 'test', 'testtestetsetetsaetaetaete', 1, 5, '3C', '1', GETDATE())


	  delete from GIFU.dbo.GOOD where GOOD_ID > 1106
	  DBCC CHECKIDENT ('[GOOD]', RESEED, 1106);
USE [GIFU]
GO

INSERT INTO [dbo].[CODE]
           ([CODE_KIND]
           ,[CODE_ID]
           ,[NAME])
     VALUES
		   ('TAG', '3C', '3C用品'),
		   ('TAG', 'GC', '生活雜貨'),
		   ('TAG', 'CL', '男女服飾'),
		   ('TAG', 'SN', '文具書籍'),
		   ('TAG', 'GM', '電玩相關'),
		   ('TAG', 'OT', '其他'),
           ('GST', 'Y', '上架'),
		   ('GST', 'N', '下架'),
		   ('OST', '1', '審核中'),
		   ('OST', '2', '已確認'),
		   ('OST', '3', '已拒絕'),
		   ('3C', '1', '電腦'),
		   ('3C', '2', '手機'),
		   ('3C', '3', '相機'),
		   ('3C', '4', '3C其他'),
		   ('GC', '1', '五金工具'),
		   ('GC', '2', '食品'),
		   ('GC', '3', '寵物用品'),
		   ('GC', '4', '生活其他'),
		   ('CL', '1', '男裝'),
		   ('CL', '2', '女裝'),
		   ('CL', '3', '配件'),
		   ('SN', '1', '小說'),
		   ('SN', '2', '漫畫'),
		   ('SN', '3', '雜誌'),
		   ('SN', '4', '文書其他'),
		   ('GM', '1', '遊戲機'),
		   ('GM', '2', '遊戲光碟'),
		   ('GM', '3', '電玩攻略'),
		   ('GM', '4', '電玩其他'),
		   ('OT', '0', '其他'),
		   ('NTI', '1', '需要您提供的XXX。'),
		   ('NTI', '2', '恭喜,您的XXX索取成功。'),
		   ('NTI', '3', '抱歉,您的XXX沒有索取成功。'),
		   ('NTI', '4', '您的XXX有新的留言。'),
		   ('NTI', '5', '你有興趣的XXX已有新的回覆。'),
		   ('TRM', '1', '面交自取'),
		   ('TRM', '2', '貨到付款')
GO

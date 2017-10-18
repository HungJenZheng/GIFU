DECLARE @UserId int
    SET @UserId = 1020
   
    delete from dbo.GOOD_PICTURE where GOOD_ID in (select GOOD_ID from dbo.GOOD where USER_ID = @UserId)
    delete from dbo.GOOD_MESSAGE where GOOD_ID in (select GOOD_ID from dbo.GOOD where USER_ID = @UserId)
    delete from dbo.GOOD_MESSAGE where USER_ID = @UserId
    delete from dbo.NOTIFICATION where RECEIVE_ID = 1021 or SEND_ID = @UserId
    delete from dbo.[ORDER] where USER_ID = @UserId
    delete from dbo.GOOD where USER_ID = @UserId
    delete from dbo.ACCOUNT where USER_ID = @UserId
GO
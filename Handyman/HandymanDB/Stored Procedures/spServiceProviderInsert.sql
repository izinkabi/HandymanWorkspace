/*Inserting a ServiceProvider SP*/
CREATE PROCEDURE [dbo].[spServiceProviderInsert]
	@RequestId int = 0,
	@ProfileId int
AS

	INSERT INTO ServiceProvider (RequestId,ProfileId)
	VALUES (@RequestId,@ProfileId)

RETURN 0

CREATE PROCEDURE [dbo].[spServiceInsert]
	@ServiceProviderId int = 0,
	@type VARCHAR (45)
AS
	INSERT INTO Service (type,ServiceProviderId)
	VALUES (@type,@ServiceProviderId)
RETURN 0

/*Consumer Insert SP*/
CREATE PROCEDURE [dbo].[spConsumerInsert]
	@ProfileId int = 0
AS
Begin

	INSERT INTO Consumer (ProfileId)
	VALUES (@ProfileId)
End

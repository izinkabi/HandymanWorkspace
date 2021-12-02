/*Consumer Insert SP*/
CREATE PROCEDURE [dbo].[spConsumerInsert]

	@ProfileId int 
AS
Begin

	INSERT INTO Consumer (ProfileId)
	VALUES (@ProfileId)
End

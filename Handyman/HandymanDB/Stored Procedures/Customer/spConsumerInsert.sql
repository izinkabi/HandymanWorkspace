/*Consumer Insert SP*/
CREATE PROCEDURE [dbo].[spConsumerInsert]

	@ProfileId int ,
	@Activation int
AS
Begin

	INSERT INTO Consumer (ProfileId,Activation)
	VALUES (@ProfileId,@Activation)
End

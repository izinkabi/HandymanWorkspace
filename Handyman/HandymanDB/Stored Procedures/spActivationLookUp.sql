/*Activation Look Up SP*/
CREATE PROCEDURE [dbo].[spActivationLookUp]
	@Id int = 0
	--**More variable will apply**
AS
Begin

set nocount on;
	SELECT ActivationId,ActivationCode,OTP
	FROM dbo.Activation 
	WHERE  ActivationId = @Id 
End

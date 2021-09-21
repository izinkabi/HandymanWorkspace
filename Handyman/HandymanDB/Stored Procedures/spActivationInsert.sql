/*Activation insert SP*/
CREATE PROCEDURE [dbo].[spActivationInsert]
	@OTP VARCHAR (45) = '',
	@ActivationCode VARCHAR (45) = ''
AS
	
Begin
	 INSERT INTO Activation (OTP,ActivationCode)
	 VALUES (@OTP,@ActivationCode)
End

CREATE TABLE [dbo].[Activation]
(
	[ActivationId] INT NOT NULL PRIMARY KEY, 
    [ActivationCode] VARCHAR(45) NULL, 
    [OTP] VARCHAR(45) NULL
)

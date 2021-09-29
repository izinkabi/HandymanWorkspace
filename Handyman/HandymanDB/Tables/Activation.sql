CREATE TABLE [dbo].[Activation]
(
	[ActivationId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ActivationCode] VARCHAR(45) NOT NULL, 
    [OTP] VARCHAR(45) NOT NULL, 
    [ProfileId] INT NOT NULL,
    [ActivationDate] DateTime NOT NULL,
    CONSTRAINT [FK_Activation_Profile] FOREIGN KEY ([ProfileId]) REFERENCES [Profile]([Id])
)

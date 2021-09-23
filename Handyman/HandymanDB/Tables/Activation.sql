CREATE TABLE [dbo].[Activation]
(
	[ActivationId] INT NOT NULL PRIMARY KEY, 
    [ActivationCode] VARCHAR(45) NULL, 
    [OTP] VARCHAR(45) NULL, 
    [ProfileId] INT NULL, 
    CONSTRAINT [FK_Activation_Profile] FOREIGN KEY ([ProfileId]) REFERENCES [Profile]([Id])
)

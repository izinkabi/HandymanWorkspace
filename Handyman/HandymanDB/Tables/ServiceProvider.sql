CREATE TABLE [dbo].[ServiceProvider]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
   
    [CompanyName] VARCHAR(50) NOT NULL, 
    [ProviderType] VARCHAR(50) NOT NULL, 
    [ProfileId] INT NOT NULL, 
    CONSTRAINT [FK_ServiceProvider_Profile] FOREIGN KEY ([ProfileId]) REFERENCES [Profile]([Id])
)

CREATE TABLE [Service].[customService]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [title] NVARCHAR(50) NOT NULL, 
    [description] NVARCHAR(100) NOT NULL, 
    [imageUrl] NVARCHAR(2000) NOT NULL, 
    [originalServiceId] INT NOT NULL, 
    CONSTRAINT [FK_customService_Service] FOREIGN KEY ([originalServiceId]) REFERENCES [Service].[service]([serv_id])

)

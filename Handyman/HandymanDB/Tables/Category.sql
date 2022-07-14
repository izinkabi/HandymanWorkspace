CREATE TABLE [Service].[Category]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [CategoryName] VARCHAR(50) NOT NULL, 
    [CategoryDescription] VARCHAR(50) NOT NULL, 
    [Type] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Service.Category] PRIMARY KEY CLUSTERED ([Id] ASC)
	

)

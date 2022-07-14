CREATE TABLE [Service].[Services]
(
	[Id] INT NOT NULL PRIMARY KEY  IDENTITY (1,1), 
    [Title] NCHAR(50) NOT NULL, 
    [Desciption] NCHAR(100) NOT NULL, 
    [CategoryId] INT NOT NULL, 
    CONSTRAINT [FK_Services_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Service].[Category]([Category_ID])
)

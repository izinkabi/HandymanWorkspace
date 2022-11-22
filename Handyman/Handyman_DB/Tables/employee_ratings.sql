CREATE TABLE [Delivery].[employee_ratings]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [employee_id] NVARCHAR(450) NOT NULL, 
    [rating_id] INT NOT NULL
)

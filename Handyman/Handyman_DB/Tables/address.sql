CREATE TABLE [Delivery].[address]
(
	[add_Id] INT NOT NULL PRIMARY KEY IDENTITY,   
    [ add_street] NVARCHAR(50) NOT NULL, 
    [add_suburb] NVARCHAR(50) NOT NULL, 
    [add_city] NVARCHAR(50) NOT NULL, 
    [add_zip] NVARCHAR(50) NOT NULL, 
    [add_latitude] FLOAT NOT NULL, 
    [add_longitude] FLOAT NOT NULL, 
    [add_country] NVARCHAR(50) NOT NULL, 
    [add_state] NVARCHAR(50) NOT NULL
)

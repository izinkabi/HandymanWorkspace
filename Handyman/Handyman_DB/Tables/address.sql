CREATE TABLE [Delivery].[address]
(
	[add_Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [add_address] NCHAR(10) NOT NULL, 
    [ add_street] NCHAR(10) NULL, 
    [add_suburb] NCHAR(10) NULL, 
    [add_city] NVARCHAR(50) NOT NULL, 
    [add_zip] NCHAR(10) NULL, 
    [add_latitude] VARBINARY(MAX) NOT NULL, 
    [add_longitude] NVARCHAR(MAX) NOT NULL, 
    [add_country] NCHAR(10) NOT NULL, 
    [add_state] NCHAR(10) NOT NULL
)

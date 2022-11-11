CREATE TABLE [Delivery].[registration]
(
	[reg_Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [reg_name] NVARCHAR(100) NOT NULL, 
    [reg_regnumber] NVARCHAR(50) NOT NULL, 
    [reg_taxnumber] NVARCHAR(50) NOT NULL, 
    [reg_businesstype] INT NOT NULL
)

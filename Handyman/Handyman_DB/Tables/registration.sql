CREATE TABLE  [Delivery].[registration]
(
	[reg_Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [reg_name] NVARCHAR(50) NOT NULL, 
    [reg_regnumber] NVARCHAR(50) NOT NULL, 
    [reg_taxnumber] NVARCHAR(50) NOT NULL, 
    [reg_workType] NVARCHAR(50) NOT NULL, 
    [reg_dateCreated] DATE NULL DEFAULT getdate()
)

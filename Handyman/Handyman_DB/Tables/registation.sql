CREATE TABLE [Delivery].[registation]
(
	[reg_Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [reg_name] NCHAR(10) NOT NULL, 
    [reg_regnumber] NCHAR(10) NOT NULL, 
    [reg_taxnumber] NCHAR(10) NOT NULL, 
    [reg_businesstype] NCHAR(10) NOT NULL
)

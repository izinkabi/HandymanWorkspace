CREATE TABLE [Delivery].[invoice]
(
	[inv_Id] INT NOT NULL PRIMARY KEY, 
    [inv_datecreated] NCHAR(10) NULL, 
    [inv_requestid] NCHAR(10) NULL, 
    [inv_orderid] NCHAR(10) NULL
)

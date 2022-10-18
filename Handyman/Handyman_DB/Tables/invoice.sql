CREATE TABLE [Delivery].[invoice]
(
	[inv_Id] INT NOT NULL PRIMARY KEY, 
    [inv_datecreated] DATETIME2 NOT NULL, 
    [inv_requestid] INT NOT NULL, 
    [inv_total] MONEY NOT NULL, 
    [inv_subtotal] MONEY NOT NULL, 
    [inv_tax] FLOAT NOT NULL, 
    CONSTRAINT [FK_invoice_request] FOREIGN KEY (inv_requestid) REFERENCES [Request].[request]([req_id]), 
)

GO

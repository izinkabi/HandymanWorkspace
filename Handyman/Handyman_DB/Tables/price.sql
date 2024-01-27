CREATE TABLE [Service].[price]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [base_price] FLOAT NOT NULL, 
    [negotiated_price] FLOAT NULL, 
    [paid_price] FLOAT NULL

)

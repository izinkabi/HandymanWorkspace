CREATE TABLE [Request].[order]
(
	[ord_id] [int] IDENTITY(1,1) NOT NULL,
    [ord_consumer_id] NVARCHAR(MAX),
	[ord_datecreated] DATETIME2(7) NOT NULL,
    [ord_duedate] DATETIME2(7) NULL,
	[ord_status] INT NOT NULL,
    [ord_service_id] INT NOT NULL, 
    CONSTRAINT [PK_order] PRIMARY KEY ([ord_id]), 
    CONSTRAINT [FK_order_service] FOREIGN KEY ([ord_service_id]) REFERENCES [Service].[service]([serv_id])
)

CREATE TABLE [Request].[order]
(
	[ord_id] [int] IDENTITY(1,1) NOT NULL,
    [ord_consumer_id] NVARCHAR(MAX),
	[ord_datecreated] [datetime2](7) NOT NULL,
	[ord_status] [nchar](10) NOT NULL,
	[ord_duedate] [datetime2](7) NULL, 
    [ord_service_id] INT NOT NULL, 
    CONSTRAINT [PK_order] PRIMARY KEY ([ord_id]), 
    CONSTRAINT [FK_order_service] FOREIGN KEY ([ord_service_id]) REFERENCES [Service].[service]([serv_id])
)

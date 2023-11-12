CREATE TABLE [Request].[request]
(
    [req_id] [int] IDENTITY(1,1) NOT NULL,
    [req_consumer_id] NVARCHAR(MAX) NOT NULL,
	[req_datecreated] DATETIME2(7) NOT NULL,
    [req_duedate] DATETIME2(7) NOT NULL,
	[req_status] INT NOT NULL,
    [req_service_id] INT NOT NULL, 
    CONSTRAINT [PK_request] PRIMARY KEY ([req_id]), 
    CONSTRAINT [FK_request_service] FOREIGN KEY ([req_service_id]) REFERENCES [Service].[service]([serv_id])
)

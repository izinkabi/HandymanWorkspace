CREATE TABLE [Request].[task]
(
	[task_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [tas_title] VARCHAR(100) NOT NULL, 
    [tas_date_started] DATETIME2 NULL DEFAULT CURRENT_TIMESTAMP, 
    [tas_date_finished] DATETIME2 NULL DEFAULT CURRENT_TIMESTAMP, 
    [tas_duration] NVARCHAR(150) NULL, 
    [tas_status] INT NULL, 
    [tas_description] VARCHAR(MAX) NULL, 
    [tas_service_id] INT NOT NULL, 
    CONSTRAINT [FK_task_service] FOREIGN KEY ([tas_service_id]) REFERENCES [Service].[service]([serv_id]) 
)

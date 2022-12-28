CREATE TABLE [Delivery].[provider]
( 
    [pro_serviceid] INT NOT NULL, 
    [pro_userid] NVARCHAR(450) NOT NULL,
    [Id] INT IDENTITY(1,1) NOT NULL,
    CONSTRAINT PK_Person PRIMARY KEY ([pro_serviceid],[pro_userid]),
    CONSTRAINT [FK_provider_employees] FOREIGN KEY (pro_userid) REFERENCES [Delivery].[employees]([emp_userid]), 
    CONSTRAINT [FK_provider_service] FOREIGN KEY ([pro_serviceid]) REFERENCES [Service].[service](serv_id)
)

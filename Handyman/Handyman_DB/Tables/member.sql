CREATE TABLE [Delivery].[member]
( 
    [member_serviceid] INT NOT NULL, 
    [member_profileId] NVARCHAR(450) NOT NULL,
    CONSTRAINT PK_Person PRIMARY KEY ([member_profileId]),
    CONSTRAINT [FK_provider_employee] FOREIGN KEY ([member_profileId]) REFERENCES [Delivery].[employee]([emp_profile_id]), 
    CONSTRAINT [FK_provider_service] FOREIGN KEY ([member_serviceid]) REFERENCES [Delivery].[workshop_services]([Id])
)

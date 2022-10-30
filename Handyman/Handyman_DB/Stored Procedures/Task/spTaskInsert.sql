CREATE PROCEDURE [Request].[spTaskInsert]
	
    @title VARCHAR(100), 
    @dateStarted DATETIME , 
    @dateFinished DATETIME, 
    @duration NVARCHAR(150), 
    @status NVARCHAR(150) , 
    @description VARCHAR(MAX),
    @serviceId int
AS
BEGIN
SET NOCOUNT ON
	INSERT INTO [Request].[task]
    ( 
    [tas_title] , 
    [tas_date_started] , 
    [tas_date_finished] , 
    [tas_duration] , 
    [tas_status] , 
    [tas_description],
    [tas_service_id]
    )
    VALUES
    (
    @title,
    @dateStarted,
    @dateFinished,
    @duration,
    @status,
    @description,
    @serviceId
    )
    DECLARE @Id INT = SCOPE_IDENTITY();

    SELECT [task_id] FROM [Request].[task] WHERE [task_id] = @Id
END

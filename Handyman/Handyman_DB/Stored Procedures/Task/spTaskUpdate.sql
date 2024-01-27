CREATE PROCEDURE [Request].[spTaskUpdate]
	@task_id int = 0,
    @tas_date_started DATETIME2(7),
    @tas_date_finished DATETIME2(7),
    @tas_duration NVARCHAR(150),
    @tas_status INT
AS
BEGIN
	UPDATE [Request].[task]
    SET   
    [tas_date_finished] = @tas_date_finished , 
    [tas_date_started] = tas_date_started,
    [tas_duration] = @tas_duration, 
    [tas_status] = @tas_status
   

    WHERE [task_id] = @task_id
END

CREATE PROCEDURE [Request].[spTaskUpdate]
	@id int = 0,
	@title VARCHAR,
    @dateStarted DATETIME,
    @dateFinished DATETIME,
    @duration NVARCHAR,
    @status NVARCHAR,
    @description VARCHAR

AS
BEGIN
	UPDATE [Request].[task]
    SET  [tas_title] = @title, 
    [tas_date_started] = @dateStarted , 
    [tas_date_finished] = @dateFinished , 
    [tas_duration] = @duration, 
    [tas_status] = @status, 
    [tas_description] = @description

    WHERE [task_id] = @id
END

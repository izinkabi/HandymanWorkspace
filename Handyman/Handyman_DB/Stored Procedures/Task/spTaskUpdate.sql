CREATE PROCEDURE [Request].[spTaskUpdate]
	@id int = 0,
	@title VARCHAR(150),
    @dateStarted DATETIME2(7),
    @dateFinished DATETIME2(7),
    @duration NVARCHAR(150),
    @status INT,
    @description VARCHAR(MAX),
    @serviceId INT

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

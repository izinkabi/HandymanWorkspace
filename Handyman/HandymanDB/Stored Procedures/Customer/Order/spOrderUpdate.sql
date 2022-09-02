CREATE PROCEDURE [Customer].[spOrderUpdate]

	@Id	INT = 0,
    @Stage       VARCHAR(100),
    @ServiceId       INT,
    @DateAccepted DATETIME2,
    @IsAccepted INT
AS
BEGIN
--Updating the Customer's Order with its ID
	UPDATE [Customer].[Order]
	SET	IsAccepted = @IsAccepted, Stage = @Stage, ServiceId = @ServiceId, DateAccepted = @DateAccepted
	WHERE Id = @Id
END

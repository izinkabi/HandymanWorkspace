﻿CREATE PROCEDURE [ServiceDelivery].[spProviderServiceUpdate]
	@Id                INT,
    @ServiceProviderId NVARCHAR,
    @JobId             INT
AS
BEGIN
Set nocount on
	UPDATE [ServiceDelivery].[ProviderService]
	SET JobId = @JobId
	WHERE Id=@Id AND ServiceProviderId = @ServiceProviderId
END
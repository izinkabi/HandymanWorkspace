﻿/*Consumer Look up SP */
CREATE PROCEDURE [dbo].[spConsumerLookUp]
	@ProfileId int = 0
	
AS
Begin
set nocount on;
	SELECT *
	FROM dbo.Consumer
	WHERE ProfileId = @ProfileId
End
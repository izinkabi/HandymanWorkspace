CREATE PROCEDURE [dbo].[spUserLookUp]
	@Id nvarchar(128)
AS
begin
--stored procedure for getting user information
	set nocount on;

	SELECT [userId], username, [Password],email,create_time
	from [dbo].[User]
	where UserId = @Id;
end


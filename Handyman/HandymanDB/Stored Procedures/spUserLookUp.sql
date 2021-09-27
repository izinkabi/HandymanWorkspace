CREATE PROCEDURE [dbo].[spUserLookUp]
	@Id nvarchar(128)
AS
begin
--stored procedure for getting user information
	set nocount on;

	SELECT [Id], username, [Password],email,create_time
	from [dbo].[User]
	where Id = @Id;
end


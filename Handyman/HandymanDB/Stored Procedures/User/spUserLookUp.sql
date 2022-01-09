CREATE PROCEDURE [dbo].[spUserLookUp]
	@Id nvarchar(128)
AS
begin
--stored procedure for getting user information
	set nocount on;
	SELECT [Id],EmailConfirmed,TwoFactorEnabled,[email]
	from [dbo].[Users]
	where Id = @Id;
end

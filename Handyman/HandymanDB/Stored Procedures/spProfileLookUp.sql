/*Profile Look Up*/
CREATE PROCEDURE [dbo].[spProfileLookUp]
	@Id nvarchar(128)
AS
begin

	set nocount on;
	SELECT *
	from dbo.Profile
	where Id = @Id;
End

/*Looking for a Service of a given ID SP*/
CREATE PROCEDURE [dbo].[spServiceLookUp]
	@Id int 
AS
Begin 
set nocount on;
	SELECT *
	FROM dbo.Service
	WHERE Id = @Id
End

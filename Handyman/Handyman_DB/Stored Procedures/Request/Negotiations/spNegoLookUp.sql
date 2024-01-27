CREATE PROCEDURE [Request].[spNegoLookUp]
--Negotiation Look Up
	@Id int = 0
AS
BEGIN
	SELECT * FROM [Request].[negotiation]
    WHERE [Id] = @Id
END
 